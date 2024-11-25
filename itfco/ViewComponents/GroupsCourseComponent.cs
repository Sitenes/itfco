using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using itfco.Service.Services.Interfaces;

namespace itfco.Web.ViewComponents
{
	public class GroupsProductComponent:ViewComponent
	{
		private readonly IProductService _courseService;
		public GroupsProductComponent(IProductService courseService)
		{
            _courseService = courseService;

        }
		public async Task<IViewComponentResult> InvokeAsync()
		{
			var groups = _courseService.GetGroups();
			return await Task.FromResult((IViewComponentResult)View("GroupsComponent", groups));
		}
	}
}
