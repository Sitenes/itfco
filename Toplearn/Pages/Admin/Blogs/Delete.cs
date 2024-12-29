using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Toplearn.Core.Security;
using Toplearn.Core.Services;
using Toplearn.Core.Services.Interfaces;

namespace Toplearn.Web.Pages.Admin.Blogs
{
	[PermissionChecker(Core.AllEnums.PermissionEnum.RemoveBlog)]
	public class Delete : Controller
	{
		private readonly IBlogService _blogService;
		public Delete(IBlogService blogService)
		{
			_blogService = blogService;
		}


		#region Delete Blog
		[Route("Admin/Blogs/Delete")]
		public async Task<IActionResult> DeleteAsync(int BlogId)
		{
			if (BlogId == 0)
				return Redirect("/Admin/ManageBlog/BlogList?currentPage=1");
			await _blogService.DeleteAsync(BlogId);
			await _blogService.SaveChangesAsync();
			return Redirect("/Admin/Blogs/BlogList?IsSucceed=true");
		}
		#endregion



	}
}
