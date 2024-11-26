using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Toplearn.Core.Services.Interfaces;

namespace Toplearn.Web.ViewComponents
{
	public class GroupsCourseComponent:ViewComponent
	{
		private readonly ICourseService _courseService;
		public GroupsCourseComponent(ICourseService courseService)
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
