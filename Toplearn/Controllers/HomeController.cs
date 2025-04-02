using Microsoft.AspNetCore.Http;
using System.IO;
using System;
using Microsoft.AspNetCore.Mvc;
using Toplearn.Core.Services.Interfaces;
using Toplearn.Core.DTOs.CourseVM;
using System.Threading.Tasks;
using Microsoft.Extensions.Localization;
using Toplearn.Web;
using TopLearn.Web;
using Microsoft.AspNetCore.Http.Extensions;
using System.Web;
using Toplearn.Core.Services;
using System.Linq;
using System.Collections.Generic;
using Microsoft.AspNetCore.Localization;

namespace Toplearn.Web.Controllers
{
    //[Route("/{language}/")]
    public class HomeController : Controller
    {
        private readonly ICourseService _courseService;
        private readonly IStringLocalizer<Resource> _localizer;
        private readonly IBlogService _blogService;

        public HomeController(ICourseService courseService, IStringLocalizer<Resource> localizer,IBlogService blogService)
        {
            _courseService = courseService;
            _localizer = localizer;
            this._blogService = blogService;
        }

        public async Task<IActionResult> Index(int Id)
        {
            switch (Id)
            {
                case 1:
                    ViewData["Login"] = true;
                    break;
                case 2:
                    ViewData["Logout"] = true;
                    break;
            }
            ViewData["Culture"] = Request.Query["Culture"].ToString();
            ViewData["ProductCount"] = await _courseService.CountCourses();
            ViewData["Category"] = await _courseService.GetParentCourseGroups();

            var blogs = await _blogService.SearchAsync(new Core.DTOs.TeacherVM.BlogFilter
            {
                PageNumber = 1,
                PageSize = 4,
            });
           
            ViewData["Blogs"] = blogs;
            return View();
        }
        public IActionResult AboutUs()
        {
            return View();
        }
        public IActionResult ChangeLanguage(string lang, string returnUrl = "/")
        {
            var supportedCultures = new[] { "fa", "ar", "en" };
            if (!Array.Exists(supportedCultures, c => c == lang))
            {
                lang = "fa"; // مقدار پیش‌فرض
            }

            // تنظیم زبان در کوکی
            Response.Cookies.Append(
                "Culture",
                lang,
                new Microsoft.AspNetCore.Http.CookieOptions
                {
                    Expires = DateTimeOffset.UtcNow.AddYears(1),
                    IsEssential = true // اطمینان از تنظیم کوکی در GDPR
                });

            var culture = new RequestCulture(lang);
            HttpContext.Response.Cookies.Append(
                CookieRequestCultureProvider.DefaultCookieName,
                CookieRequestCultureProvider.MakeCookieValue(culture),
                new Microsoft.AspNetCore.Http.CookieOptions { Expires = DateTimeOffset.UtcNow.AddYears(1) });

            return LocalRedirect(returnUrl); // بازگشت به صفحه قبل
        }

        [HttpPost]
        [Route("file-upload")]
        public IActionResult UploadImage(IFormFile upload, string CKEditorFuncNum, string CKEditor, string langCode)
        {
            if (upload.Length <= 0) return null;

            var fileName = Guid.NewGuid() + Path.GetExtension(upload.FileName).ToLower();



            var path = Path.Combine(
                Directory.GetCurrentDirectory(), "wwwroot", "CourseRoot", "DescriptionImg",
                fileName);

            using (var stream = new FileStream(path, FileMode.Create))
            {
                upload.CopyTo(stream);

            }



            var url = $"{"/CourseRoot/DescriptionImg/"}{fileName}";


            return Json(new { uploaded = true, url });
        }
    }
}
