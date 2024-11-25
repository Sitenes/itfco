using itfco.Entity.Entities.Permissions.Products;
using itfco.ViewModel.DTOs.ProductVM;
using itfco.ViewModel.DTOs.TeacherVM;
using Microsoft.AspNetCore.Http;

namespace itfco.Service.Services.Interfaces
{
	public interface IProductService
	{
		#region Group
		IEnumerable<Group> GetGroups();
		IEnumerable<TeacherForProductViewModel> GetTeachersName();
		IEnumerable<ProductStatus> GetStatuses();
		IEnumerable<ProductLevel> GetLevels();
		Task<IEnumerable<ProductListAdminViewModel>> GetProductsList();
		Task<IEnumerable<ProductListAdminViewModel>> GetProductsList(ProductFilterAdminViewModel Filter);
		Task<Product> SetGroup(Product course);

		#endregion

		#region Products
		Task<int> AddProduct(Product course, IFormFile ImageFile, IFormFile DemoFile);
		Task<bool> IsProductExsit(int Id);
		Task<Product> GetProduct(int Id);
		Task<bool> RemoveProduct(int Id);
		Task<IEnumerable<ProductItemListViewModel>> GetProductsList(ProductFilterListViewModel filter);
		Task<bool> Update(Product course, IFormFile ImageFile, IFormFile DemoFile);
		#endregion

		#region Episode
		IEnumerable<EpisodeListViewModel> GetEpisodesList(FilterEpisodeListViewModel filters);
		Task<int> AddEpisode(Episode episode, IFormFile video);
		Task<bool> EditEpisode(Episode episode, IFormFile video);
		Task<Episode> GetEpisode(int Id);
		Task<bool> RemoveEpisode(int Id);

		#endregion

		Task SaveChanges();
	}
}
