using itfco.Service.Services.Interfaces;
using itfco.DataLayer.Context;
using itfco.Entity.Entities.Permissions.Products;
using itfco.ViewModel.DTOs.ProductVM;
using itfco.ViewModel.DTOs.TeacherVM;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using itfco.Service.Convertors;

namespace itfco.Service.Services
{
	public class ProductService : IProductService
	{
		private readonly Context _context;
		public ProductService(Context context)
		{
			_context = context;
		}

		public async Task<int> AddProduct(Product course, IFormFile ImageFile, IFormFile DemoFile)
		{
			if (course.RegistrationDate == DateTime.MinValue)
				course.RegistrationDate = DateTime.Now;

			if (course.Description == null)
				course.Description = "";

			if (course.GroupId == 0 && course.SubGroupId != 0)
				await SetGroup(course);

			if (ImageFile == null)
			{
				course.Image = "Default.png";
			}
			else
			{
				string newAvatarURL = Guid.NewGuid().ToString() + Path.GetExtension(ImageFile.FileName);
				string newPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "ProductRoot", "Image", newAvatarURL);
				using (var stream = new FileStream(newPath, FileMode.Create))
				{
					ImageFile.CopyTo(stream);
				}
				course.Image = newAvatarURL;

				string thumbPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "ProductRoot", "ThumbImage", newAvatarURL);
				string ImageCurrentPath = newPath;
			}
			if (DemoFile == null)
			{
				course.DemoFileName = "";
			}
			else
			{
				string newAvatarURL = Guid.NewGuid().ToString() + Path.GetExtension(DemoFile.FileName);
				string newPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "ProductRoot", "Demo", newAvatarURL);
				using (var stream = new FileStream(newPath, FileMode.Create))
				{
					await DemoFile.CopyToAsync(stream);
				}
				course.DemoFileName = newAvatarURL;
			}
			await _context.AddAsync(course);
			await _context.SaveChangesAsync();
			return course.ProductId;

		}

		public async Task<int> AddEpisode(Episode episode, IFormFile video)
		{
			if (video == null || episode == null)
				return 0;

			try
			{
				string newAvatarURL = Guid.NewGuid().ToString() + Path.GetExtension(video.FileName);
				string newPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "ProductRoot", "Episode", newAvatarURL);
				using (var stream = new FileStream(newPath, FileMode.Create))
				{
					await video.CopyToAsync(stream);
				}
				episode.EpisodeFileName = newAvatarURL;


				await _context.AddAsync(episode);
				await _context.SaveChangesAsync();
				return episode.EpisodeId;
			}
			catch (Exception)
			{
				return 0;
			}

		}

		public async Task<bool> EditEpisode(Episode episode, IFormFile video)
		{
			if (episode == null || episode.EpisodeId == 0)
				return false;
			try
			{
				#region Edit Video

				if (video != null)
				{
					if (string.IsNullOrEmpty(episode.EpisodeFileName))
					{
						string oldPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "ProductRoot", "Episode", episode.EpisodeFileName);
						if (System.IO.File.Exists(oldPath))
							System.IO.File.Delete(oldPath);
					}
					string newAvatarURL = Guid.NewGuid().ToString() + Path.GetExtension(video.FileName);
					string newPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "ProductRoot", "Episode", newAvatarURL);
					using (var stream = new FileStream(newPath, FileMode.Create))
					{
						await video.CopyToAsync(stream);
					}
					episode.EpisodeFileName = newAvatarURL;

				}

				#endregion



				_context.Update(episode);
				await _context.SaveChangesAsync();
			}
			catch (Exception)
			{

				return false;
			}

			return true;
		}

		public async Task<Product> GetProduct(int Id)
		{
			return await _context.Products.FindAsync(Id);
		}

		public async Task<IEnumerable<ProductListAdminViewModel>> GetProductsList()
		{
			return await _context.Products.Include(n => n.Teacher).Select(n => new ProductListAdminViewModel()
			{
				ProductId = n.ProductId,
				ProductTitle = n.Title,
				Image = n.Image,
				NumberOfStudents = n.NumberOfStudents,
				Price = n.Price,
				TeacherName = n.Teacher.UserName,
			}).ToListAsync();
		}

		public async Task<IEnumerable<ProductListAdminViewModel>> GetProductsList(ProductFilterAdminViewModel Filter)
		{
			IEnumerable<Product> Products = await _context.Products.Include(n => n.Teacher).ToListAsync();
			if (Filter.ProductStatusId != 0)
			{
				Products = Products.Where(n => n.ProductStatusId == Filter.ProductStatusId);
			}
			if (!string.IsNullOrWhiteSpace(Filter.Description))
			{
				Products = Products.Where(n => n.Description.Contains(Filter.Description));
			}

			if (Filter.GroupId != 0)
			{
				Products = Products.Where(n => n.GroupId == Filter.GroupId);
			}

			if (Filter.LevelId != 0)
			{
				Products = Products.Where(n => n.LevelId == Filter.LevelId);
			}

			if (Filter.PriceFrom != 0)
			{
				Products = Products.Where(n => n.Price >= Filter.PriceFrom);
			}

			if (Filter.PriceTo != 0)
			{
				Products = Products.Where(n => n.Price <= Filter.PriceTo);
			}

			if (Filter.StudentsCountFrom != 0)
			{
				Products = Products.Where(n => n.NumberOfStudents >= Filter.StudentsCountFrom);
			}

			if (Filter.StudentsCountTo != 0)
			{
				Products = Products.Where(n => n.NumberOfStudents <= Filter.StudentsCountTo);
			}

			if (!string.IsNullOrWhiteSpace(Filter.Tags))
			{
				Products = Products.Where(n => n.Tags.Contains(Filter.Tags));
			}

			if (!string.IsNullOrWhiteSpace(Filter.TeacherOrProductId))
			{
				Products = Products.Where(n => n.TeacherId.ToString() == Filter.TeacherOrProductId || n.ProductId.ToString() == Filter.TeacherOrProductId);
			}

			if (!string.IsNullOrWhiteSpace(Filter.Title))
			{
				Products = Products.Where(n => n.Title.Contains(Filter.Title));
			}
			Filter.ProductsCount = Products.Count();
			if (Filter.CurrentPage != 0)
				Products = Products.Skip((Filter.CurrentPage - 1) * Filter.ItemPerPage);
			if (Filter.ItemPerPage != 0)
				Products = Products.Take(Filter.ItemPerPage);

			return Products.Select(n => new ProductListAdminViewModel()
			{
				ProductId = n.ProductId,
				ProductTitle = n.Title,
				Image = n.Image,
				NumberOfStudents = n.NumberOfStudents,
				Price = n.Price,
				TeacherName = n.Teacher.UserName
			}).ToList();
		}

		public async Task<IEnumerable<ProductItemListViewModel>> GetProductsList(ProductFilterListViewModel filter)
		{
			IQueryable<Product> coursesList = _context.Products.Include(n => n.ProductEpisode);
			if (filter.Coust == Entity.Enum.Coust.Monetary)
				coursesList = coursesList.Where(n => n.Price != 0);
			if (filter.Coust == Entity.Enum.Coust.Free)
				coursesList = coursesList.Where(n => n.Price == 0);
			if (filter.EndPrice != 0)
				coursesList = coursesList.Where(n => n.Price <= filter.EndPrice);

			if (filter.StartPrice != 0)
				coursesList = coursesList.Where(n => n.Price >= filter.EndPrice);

			List<Product> courses = await coursesList.ToListAsync();
			if (filter.Groups != null && filter.Groups.Count() != 0)
			{
				List<Product> coursesGroupFilter = new List<Product>();
				foreach (var groupId in filter.Groups)
				{
					coursesGroupFilter.AddRange(courses.Where(n => n.GroupId == groupId));
				}
				courses = coursesGroupFilter;
			}
			if (!string.IsNullOrWhiteSpace(filter.Title))
			{
				var titles = filter.Title.Split(' ');

				List<Product> coursesTitleFilterd = new List<Product>();
				foreach (var titleWord in titles)
				{
					var titleWordTrim = titleWord.Trim();
					if (titleWordTrim == "و")
						titleWordTrim.Replace("و", "");
					if (!string.IsNullOrWhiteSpace(titleWordTrim))
					{
						coursesTitleFilterd.AddRange(coursesList.Where(n => n.Title.Contains(titleWord)));
					}
				}
				courses = coursesTitleFilterd;
			}
			switch (filter.OrderBy)
			{
				case Entity.Enum.OrderBy.None:
					break;
				case Entity.Enum.OrderBy.PriceAsc:
					courses = courses.OrderBy(n => n.Price).ToList();
					break;
				case Entity.Enum.OrderBy.PriceDesc:
					courses = courses.OrderByDescending(n => n.Price).ToList();
					break;
				case Entity.Enum.OrderBy.TimeAsc:
					courses = courses.OrderBy(n => n.ProductEpisode.Sum(m => m.EpisodeTime.Ticks)).ToList();
					break;
				case Entity.Enum.OrderBy.TimeDesc:
					courses = courses.OrderByDescending(n => n.ProductEpisode.Sum(m => m.EpisodeTime.Ticks)).ToList();
					break;
				case Entity.Enum.OrderBy.CreateDateAsc:
					courses = courses.OrderBy(n => n.RegistrationDate).ToList();
					break;
				case Entity.Enum.OrderBy.CreateDateDesc:
					courses = courses.OrderByDescending(n => n.RegistrationDate).ToList();
					break;
				default:
					break;
			}
			return courses.Select(n => new ProductItemListViewModel
			{
				Id = n.ProductId,
				Image = n.Image,
				Price = n.Price,
				Time = new TimeSpan(n.ProductEpisode.Sum(m => m.EpisodeTime.Ticks)),
				Title = n.Title
			});

		}

		public async Task<Episode> GetEpisode(int Id)
		{
			var episode = await _context.Episode.FindAsync(Id);
			return episode;
		}

		public IEnumerable<EpisodeListViewModel> GetEpisodesList(FilterEpisodeListViewModel filters)
		{
			if (filters.EpisodeTimeTo == TimeSpan.Zero)
				filters.EpisodeTimeTo = new TimeSpan(0, 23, 59, 59);
			IQueryable<Episode> episodes = _context.Episode.Where(n => n.ProductId == filters.ProductId);
			if (filters.Title != null)
				episodes = episodes.Where(n => n.EpisodeTitle.Contains(filters.Title));
			episodes = episodes.Where(n => n.EpisodeTime >= filters.EpisodeTimeFrom);
			episodes = episodes.Where(n => n.EpisodeTime <= filters.EpisodeTimeTo);
			if (filters.EpisodeStatus == Entity.Enum.Coust.Free)
				episodes = episodes.Where(n => n.IsFree == true);

			if (filters.EpisodeStatus == Entity.Enum.Coust.Monetary)
				episodes = episodes.Where(n => n.IsFree == false);

			IEnumerable<EpisodeListViewModel> episodesResult = episodes.Select(n => new EpisodeListViewModel()
			{
				EpisodeId = n.EpisodeId,
				EpisodeTime = n.EpisodeTime,
				EpisodeTitle = n.EpisodeTitle,
				IsFree = n.IsFree
			});
			return episodesResult;
		}

		public IEnumerable<Group> GetGroups()
		{
			return _context.Groups.ToList();
		}

		public IEnumerable<ProductLevel> GetLevels()
		{

			return _context.ProductLevels.ToList();
		}

		public IEnumerable<ProductStatus> GetStatuses()
		{

			return _context.ProductStatuses.ToList();
		}

		public IEnumerable<TeacherForProductViewModel> GetTeachersName()
		{
			return _context.Users.Where(n => n.IsTeacher == true).Select(n =>

				new TeacherForProductViewModel()
				{
					TeacherId = n.UserId,
					TeacherName = n.UserName
				}).ToList();

		}

		public async Task<bool> IsProductExsit(int Id)
		{
			return await _context.Products.AnyAsync(n => n.ProductId == Id);
		}

		public async Task<bool> RemoveProduct(int Id)
		{
			var course = await GetProduct(Id);
			if (course == null)
				return false;
			try
			{
				string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "ProductRoot", "Demo", course.DemoFileName);
				if (System.IO.File.Exists(path))
				{
					System.IO.File.Delete(path);
				}

				if (course.Image != "course.Image")
				{
					string imgPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "ProductRoot", "Image", course.Image);
					if (System.IO.File.Exists(imgPath))
					{
						System.IO.File.Delete(imgPath);
					}
				}

				string thumbImgPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "ProductRoot", "ThumbImage", course.Image);
				if (System.IO.File.Exists(thumbImgPath))
				{
					System.IO.File.Delete(thumbImgPath);
				}
				if (course.ProductEpisode != null)
					foreach (var episodeItem in course.ProductEpisode)
					{
						string episodePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "ProductRoot", "Episode", episodeItem.EpisodeId.ToString());
						if (Directory.Exists(episodePath))
							Directory.Delete(episodePath, true);
						_context.Remove(episodeItem);
					}

				_context.Products.Remove(await GetProduct(Id));

			}
			catch (Exception)
			{
				return false;
			}
			return true;
		}

		public async Task<bool> RemoveEpisode(int Id)
		{
			try
			{
				var episode = await GetEpisode(Id);
				if (episode == null)
					return false;
				_context.Episode.Remove(episode);
				await SaveChanges();
			}
			catch (Exception)
			{
				return false;
			}
			return true;
		}

		public async Task SaveChanges()
		{
			await _context.SaveChangesAsync();
		}

		public async Task<Product> SetGroup(Product course)
		{

			if (course.GroupId == 0)
			{
				var group = await _context.Groups.SingleOrDefaultAsync(n => n.GroupId == course.SubGroupId);
				course.GroupId = (int)group.ParentId;
			}


			return course;
		}

		public async Task<bool> Update(Product course, IFormFile ImageFile, IFormFile DemoFile)
		{
			if (!await _context.Products.AnyAsync(n => n.ProductId == course.ProductId))
				return false;
			try
			{
				if (ImageFile != null)
				{
					if (course.Image != "Default.png" && !string.IsNullOrEmpty(course.Image))
					{
						string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "ProductRoot", "Image", course.Image);
						if (System.IO.File.Exists(path))
						{
							System.IO.File.Delete(path);
						}

						string thumbPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "ProductRoot", "ThumbImage", course.Image);
						if (System.IO.File.Exists(thumbPath))
						{
							System.IO.File.Delete(thumbPath);
						}
					}
					string newAvatarURL = Guid.NewGuid().ToString() + Path.GetExtension(ImageFile.FileName);
					string newPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "ProductRoot", "Image", newAvatarURL);
					using (var stream = new FileStream(newPath, FileMode.Create))
					{
						ImageFile.CopyTo(stream);
					}
					course.Image = newAvatarURL;
					string ThumbPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "ProductRoot", "ThumbImage", newAvatarURL);
				}

				if (DemoFile != null)
				{
					if (course.DemoFileName != null)
					{
						string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "ProductRoot", "Demo", course.DemoFileName);
						if (System.IO.File.Exists(path))
						{
							System.IO.File.Delete(path);
						}
					}
					string newAvatarURL = Guid.NewGuid().ToString() + Path.GetExtension(DemoFile.FileName ?? "");
					string newPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "ProductRoot", "Demo", newAvatarURL);
					using (var stream = new FileStream(newPath, FileMode.Create))
					{
						DemoFile.CopyTo(stream);
					}
					course.DemoFileName = newAvatarURL;
				}
				_context.Products.Update(course);
			}
			catch (Exception)
			{
				return false;
			}
			return true;
		}
	}
}
