using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Toplearn.Core.DTOs.CourseVM;
using Toplearn.Core.DTOs.TeacherVM;
using Toplearn.Core.Services.Interfaces;
using Toplearn.DataLayer.Context;
using Toplearn.DataLayer.Entities.Courses;
using TopLearn.Core.Convertors;

namespace Toplearn.Core.Services
{
    public class CourseService : ICourseService
    {
        private readonly ToplearnContext _context;
        public CourseService(ToplearnContext context)
        {
            _context = context;
        }

        public async Task<int> AddCourse(Course course, IFormFile ImageFile, IFormFile DemoFile)
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
                string newPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "CourseRoot", "Image", newAvatarURL);
                using (var stream = new FileStream(newPath, FileMode.Create))
                {
                    ImageFile.CopyTo(stream);
                }
                course.Image = newAvatarURL;
                ImageConvertor imageResize = new ImageConvertor();
                string thumbPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "CourseRoot", "ThumbImage", newAvatarURL);
                string ImageCurrentPath = newPath;
                imageResize.Image_resize(ImageCurrentPath, thumbPath, 5000);
            }
            if (DemoFile == null)
            {
                course.DemoFileName = "";
            }
            else
            {
                string newAvatarURL = Guid.NewGuid().ToString() + Path.GetExtension(DemoFile.FileName);
                string newPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "CourseRoot", "Demo", newAvatarURL);
                using (var stream = new FileStream(newPath, FileMode.Create))
                {
                    await DemoFile.CopyToAsync(stream);
                }
                course.DemoFileName = newAvatarURL;
            }
            await _context.AddAsync(course);
            await _context.SaveChangesAsync();
            return course.CourseId;

        }

        public async Task<int> AddEpisode(Episode episode, IFormFile video)
        {
            if (video == null || episode == null)
                return 0;

            try
            {
                string newAvatarURL = Guid.NewGuid().ToString() + Path.GetExtension(video.FileName);
                string newPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "CourseRoot", "Episode", newAvatarURL);
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
                        string oldPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "CourseRoot", "Episode", episode.EpisodeFileName);
                        if (System.IO.File.Exists(oldPath))
                            System.IO.File.Delete(oldPath);
                    }
                    string newAvatarURL = Guid.NewGuid().ToString() + Path.GetExtension(video.FileName);
                    string newPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "CourseRoot", "Episode", newAvatarURL);
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

        public async Task<Course> GetCourse(int Id)
        {
            return await _context.Courses.FindAsync(Id);
        }

        public async Task<IEnumerable<CourseListAdminViewModel>> GetCoursesList()
        {
            return await _context.Courses.Include(n => n.Teacher).Select(n => new CourseListAdminViewModel()
            {
                CourseId = n.CourseId,
                CourseTitle = n.Title,
                Image = n.Image,
                NumberOfStudents = n.NumberOfStudents,
                Price = n.Price,
                TeacherName = n.Teacher.UserName,
            }).ToListAsync();
        }

        public async Task<IEnumerable<CourseListAdminViewModel>> GetCoursesList(CourseFilterAdminViewModel Filter)
        {
            IEnumerable<Course> Courses = await _context.Courses.Include(n => n.Teacher).ToListAsync();
            if (Filter.CourseStatusId != 0)
            {
                Courses = Courses.Where(n => n.CourseStatusId == Filter.CourseStatusId);
            }
            if (!string.IsNullOrWhiteSpace(Filter.Description))
            {
                Courses = Courses.Where(n => n.Description.Contains(Filter.Description));
            }

            if (Filter.GroupId != 0)
            {
                Courses = Courses.Where(n => n.GroupId == Filter.GroupId);
            }

            if (Filter.LevelId != 0)
            {
                Courses = Courses.Where(n => n.LevelId == Filter.LevelId);
            }

            if (Filter.PriceFrom != 0)
            {
                Courses = Courses.Where(n => n.Price >= Filter.PriceFrom);
            }

            if (Filter.PriceTo != 0)
            {
                Courses = Courses.Where(n => n.Price <= Filter.PriceTo);
            }

            if (Filter.StudentsCountFrom != 0)
            {
                Courses = Courses.Where(n => n.NumberOfStudents >= Filter.StudentsCountFrom);
            }

            if (Filter.StudentsCountTo != 0)
            {
                Courses = Courses.Where(n => n.NumberOfStudents <= Filter.StudentsCountTo);
            }

            if (!string.IsNullOrWhiteSpace(Filter.Tags))
            {
                Courses = Courses.Where(n => n.Tags.Contains(Filter.Tags));
            }

            if (!string.IsNullOrWhiteSpace(Filter.TeacherOrCourseId))
            {
                Courses = Courses.Where(n => n.TeacherId.ToString() == Filter.TeacherOrCourseId || n.CourseId.ToString() == Filter.TeacherOrCourseId);
            }

            if (!string.IsNullOrWhiteSpace(Filter.Title))
            {
                Courses = Courses.Where(n => n.Title.Contains(Filter.Title));
            }
            Filter.CoursesCount = Courses.Count();
            if (Filter.CurrentPage != 0)
                Courses = Courses.Skip((Filter.CurrentPage - 1) * Filter.ItemPerPage);
            if (Filter.ItemPerPage != 0)
                Courses = Courses.Take(Filter.ItemPerPage);

            return Courses.Select(n => new CourseListAdminViewModel()
            {
                CourseId = n.CourseId,
                CourseTitle = n.Title,
                Image = n.Image,
                NumberOfStudents = n.NumberOfStudents,
                Price = n.Price,
                TeacherName = n.Teacher.UserName
            }).ToList();
        }

        public async Task<IEnumerable<CourseItemListViewModel>> GetCoursesList(CourseFilterListViewModel filter)
        {
            IQueryable<Course> coursesList = _context.Courses.Include(n=>n.CourseEpisode);
            if (filter.Coust == Enum.Coust.Monetary)
                coursesList = coursesList.Where(n => n.Price != 0);
            if (filter.Coust == Enum.Coust.Free)
                coursesList = coursesList.Where(n => n.Price == 0);
            if (filter.EndPrice != 0)
                coursesList = coursesList.Where(n => n.Price <= filter.EndPrice);

            if (filter.StartPrice != 0)
                coursesList = coursesList.Where(n => n.Price >= filter.EndPrice);

            List<Course> courses = await coursesList.ToListAsync();
            if (filter.Groups != null && filter.Groups.Count() != 0)
            {
                List<Course> coursesGroupFilter = new List<Course>();
                foreach (var groupId in filter.Groups)
                {
                    coursesGroupFilter.AddRange(courses.Where(n => n.GroupId == groupId));
                }
                courses = coursesGroupFilter;
            }
            if (!string.IsNullOrWhiteSpace(filter.Title))
            {
                var titles = filter.Title.Split(' ');

                List<Course> coursesTitleFilterd = new List<Course>();
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
                case Enum.OrderBy.None:
                    break;
                case Enum.OrderBy.PriceAsc:
                    courses = courses.OrderBy(n => n.Price).ToList();
                    break;
                case Enum.OrderBy.PriceDesc:
                    courses = courses.OrderByDescending(n => n.Price).ToList();
                    break;
                case Enum.OrderBy.TimeAsc:
                    courses = courses.OrderBy(n => n.CourseEpisode.Sum(m=>m.EpisodeTime.Ticks)).ToList();
                    break;
                case Enum.OrderBy.TimeDesc:
                    courses = courses.OrderByDescending(n => n.CourseEpisode.Sum(m => m.EpisodeTime.Ticks)).ToList();
                    break;
                case Enum.OrderBy.CreateDateAsc:
                    courses = courses.OrderBy(n => n.RegistrationDate).ToList();
                    break;
                case Enum.OrderBy.CreateDateDesc:
                    courses = courses.OrderByDescending(n => n.RegistrationDate).ToList();
                    break;
                default:
                    break;
            }
            return courses.Select(n => new CourseItemListViewModel
            {
                Id = n.CourseId,
                Image = n.Image,
                Price = n.Price,
                Time = new TimeSpan(n.CourseEpisode.Sum(m => m.EpisodeTime.Ticks)),
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
            IQueryable<Episode> episodes = _context.Episode.Where(n => n.CourseId == filters.CourseId);
            if (filters.Title != null)
                episodes = episodes.Where(n => n.EpisodeTitle.Contains(filters.Title));
            episodes = episodes.Where(n => n.EpisodeTime >= filters.EpisodeTimeFrom);
            episodes = episodes.Where(n => n.EpisodeTime <= filters.EpisodeTimeTo);
            if (filters.EpisodeStatus == Enum.Coust.Free)
                episodes = episodes.Where(n => n.IsFree == true);

            if (filters.EpisodeStatus == Enum.Coust.Monetary)
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

        public IEnumerable<CourseGroup> GetGroups()
        {
            return _context.CourseGroups.ToList();
        }

        public IEnumerable<CourseLevel> GetLevels()
        {

            return _context.CourseLevels.ToList();
        }

        public IEnumerable<CourseStatus> GetStatuses()
        {

            return _context.CourseStatuses.ToList();
        }

        public IEnumerable<TeacherForCourseViewModel> GetTeachersName()
        {
            return _context.Users.Where(n => n.IsTeacher == true).Select(n =>

                new TeacherForCourseViewModel()
                {
                    TeacherId = n.UserId,
                    TeacherName = n.UserName
                }).ToList();

        }

        public async Task<bool> IsCourseExsit(int Id)
        {
            return await _context.Courses.AnyAsync(n => n.CourseId == Id);
        }

        public async Task<bool> RemoveCourse(int Id)
        {
            var course = await GetCourse(Id);
            if (course == null)
                return false;
            try
            {
                string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "CourseRoot", "Demo", course.DemoFileName);
                if (System.IO.File.Exists(path))
                {
                    System.IO.File.Delete(path);
                }

                if (course.Image != "course.Image")
                {
                    string imgPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "CourseRoot", "Image", course.Image);
                    if (System.IO.File.Exists(imgPath))
                    {
                        System.IO.File.Delete(imgPath);
                    }
                }

                string thumbImgPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "CourseRoot", "ThumbImage", course.Image);
                if (System.IO.File.Exists(thumbImgPath))
                {
                    System.IO.File.Delete(thumbImgPath);
                }
                if (course.CourseEpisode != null)
                    foreach (var episodeItem in course.CourseEpisode)
                    {
                        string episodePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "CourseRoot", "Episode", episodeItem.EpisodeId.ToString());
                        if (Directory.Exists(episodePath))
                            Directory.Delete(episodePath, true);
                        _context.Remove(episodeItem);
                    }

                _context.Courses.Remove(await GetCourse(Id));

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

        public async Task<Course> SetGroup(Course course)
        {

            if (course.GroupId == 0)
            {
                var group = await _context.CourseGroups.SingleOrDefaultAsync(n => n.GroupId == course.SubGroupId);
                course.GroupId = (int)group.ParentId;
            }


            return course;
        }

        public async Task<bool> Update(Course course, IFormFile ImageFile, IFormFile DemoFile)
        {
            if (!await _context.Courses.AnyAsync(n => n.CourseId == course.CourseId))
                return false;
            try
            {
                if (ImageFile != null)
                {
                    if (course.Image != "Default.png" && !string.IsNullOrEmpty(course.Image))
                    {
                        string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "CourseRoot", "Image", course.Image);
                        if (System.IO.File.Exists(path))
                        {
                            System.IO.File.Delete(path);
                        }

                        string thumbPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "CourseRoot", "ThumbImage", course.Image);
                        if (System.IO.File.Exists(thumbPath))
                        {
                            System.IO.File.Delete(thumbPath);
                        }
                    }
                    string newAvatarURL = Guid.NewGuid().ToString() + Path.GetExtension(ImageFile.FileName);
                    string newPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "CourseRoot", "Image", newAvatarURL);
                    using (var stream = new FileStream(newPath, FileMode.Create))
                    {
                        ImageFile.CopyTo(stream);
                    }
                    course.Image = newAvatarURL;
                    ImageConvertor imageResize = new ImageConvertor();
                    string ThumbPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "CourseRoot", "ThumbImage", newAvatarURL);
                    string ImageCurrentPath = newPath;
                    imageResize.Image_resize(ImageCurrentPath, ThumbPath, 5000);
                }

                if (DemoFile != null)
                {
                    if (course.DemoFileName != null)
                    {
                        string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "CourseRoot", "Demo", course.DemoFileName);
                        if (System.IO.File.Exists(path))
                        {
                            System.IO.File.Delete(path);
                        }
                    }
                    string newAvatarURL = Guid.NewGuid().ToString() + Path.GetExtension(DemoFile.FileName ?? "");
                    string newPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "CourseRoot", "Demo", newAvatarURL);
                    using (var stream = new FileStream(newPath, FileMode.Create))
                    {
                        DemoFile.CopyTo(stream);
                    }
                    course.DemoFileName = newAvatarURL;
                }
                _context.Courses.Update(course);
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }
    }
}
