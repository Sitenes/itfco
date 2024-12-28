using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Toplearn.Core.Security;
using Toplearn.Core.Services;
using Toplearn.Core.Services.Interfaces;
using Toplearn.DataLayer.Entities.Blogs;

namespace Toplearn.Web.Pages.Admin.Blogs
{
    [PermissionChecker(Core.AllEnums.PermissionEnum.BlogManagement)]
    public class List : PageModel
    {
        private readonly IBlogService _blogService;

        public List(IBlogService blogService)
        {
            _blogService = blogService;
        }


        public IEnumerable<Blog> Blogs { get; set; }
        public async Task OnGetAsync(bool IsSucceed, [FromQuery] string blogId)
        {
            if(IsSucceed)
                ViewData["IsSucceed"] = true;
            if (!string.IsNullOrEmpty(blogId))
            Blogs = await _blogService.GetAllAsync();

        }
    }
}
