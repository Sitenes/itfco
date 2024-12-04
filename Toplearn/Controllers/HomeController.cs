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

namespace Toplearn.Web.Controllers
{
    //[Route("/{language}/")]
    public class HomeController : Controller
    {
        private readonly ICourseService _courseService;
        private readonly IStringLocalizer<Resource> _localizer;
        public HomeController(ICourseService courseService, IStringLocalizer<Resource> localizer)
        {
            _courseService = courseService;
            _localizer = localizer;
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

            return View();
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
