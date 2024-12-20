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

namespace Toplearn.Web.Controllers
{
    //[Route("/{language}/")]
    
    public class CartController : Controller
    {
        private readonly ICourseService _courseService;
        private readonly IStringLocalizer<Resource> _localizer;
        public CartController(ICourseService courseService, IStringLocalizer<Resource> localizer)
        {
            _courseService = courseService;
            _localizer = localizer;
        }

        public async Task<IActionResult> Index()
        {
            return View();
        }
    }
}
