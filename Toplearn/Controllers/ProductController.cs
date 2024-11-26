using Microsoft.AspNetCore.Http;
using System.IO;
using System;
using Microsoft.AspNetCore.Mvc;
using Toplearn.Core.Services.Interfaces;
using Toplearn.Core.DTOs.CourseVM;
using System.Threading.Tasks;
using System.Collections.Generic;
using Toplearn.DataLayer.Entities.Courses;
using Toplearn.Core.DTOs.UserVM;

namespace Toplearn.Web.Controllers
{
    //[Route("/{language}/")]
    public class ProductController : Controller
    {
        private readonly ICourseService _courseService;
        public ProductController(ICourseService courseService)
        {
            _courseService = courseService;
        }
        public async Task<IActionResult> Index(CourseFilterAdminViewModel input)
        {
            var products = await _courseService.GetCoursesList(input);

            return View(products);
        }
        [Route("/product/{id}")]
        public async Task<IActionResult> Index(int id)
        {
            var product = await _courseService.GetCourse(id);

            return View("product",product);
        }
    }
}
