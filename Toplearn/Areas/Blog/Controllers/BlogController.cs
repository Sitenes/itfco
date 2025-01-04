using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Toplearn.Core.Services.Interfaces;

namespace Toplearn.Web.Areas.Blog.Controllers
{
    [Area("Blog")]
    [Route("[controller]/[action]")]
    public class BlogController : Controller
    {
        private readonly IBlogService _blogService;
        private readonly IStringLocalizer<Resource> _localizer;

        public BlogController(IBlogService blogService, IStringLocalizer<Resource> localizer)
        {
            _blogService = blogService;
            _localizer = localizer;
        }

        [Route("")]
        public async Task<IActionResult> BlogList(string search = "")
        {
            var blogs = await _blogService.SearchAsync(new Core.DTOs.TeacherVM.BlogFilter
            {
                Title = search,
            });
            ViewBag.Search = search;
            //ViewBag.TotalPages = (int)Math.Ceiling((double)blogs.TotalCount / pageSize);

            
            return View(blogs);
        }

        [Route("{id}")]
        public async Task<IActionResult> BlogDetail(int id)
        {
            var blog = await _blogService.GetByIdAsync(id);
            if (blog == null)
            {
                return NotFound();
            }
            var baseUrl = $"{Request.Scheme}://{Request.Host}";
            ViewData["BaseUrl"] = baseUrl;

            return View(nameof(BlogDetail),blog);
        }
    }
}
