using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Toplearn.Core.DTOs.TeacherVM;
using Toplearn.Core.Services.Interfaces;
using Toplearn.DataLayer.Entities.Courses;
using static System.Net.Mime.MediaTypeNames;

namespace Toplearn.Web.Pages.Admin.Courses
{

    public class CreateModel : PageModel
    {
        private readonly ICourseService _courseService;
        public CreateModel(ICourseService courseService)
        {
            _courseService = courseService;
        }
        [BindProperty]
        public Course Course { get; set; }
        public void OnGet()
        {
            ViewData["Groups"] = _courseService.GetGroups() as List<CourseGroup>;
            ViewData["Teachers"] = _courseService.GetTeachersName();
            ViewData["Statuses"] = _courseService.GetStatuses();
            ViewData["Levels"] = _courseService.GetLevels();
        }
        public async Task<IActionResult> OnPostAsync(IFormFile ImageFile, IFormFile DemoFile)
        {
            ViewData["Groups"] = _courseService.GetGroups() as List<CourseGroup>;
            ViewData["Teachers"] = _courseService.GetTeachersName();
            ViewData["Statuses"] = _courseService.GetStatuses();
            ViewData["Levels"] = _courseService.GetLevels();
            if (!ModelState.IsValid)
                return Page();
            Course = await _courseService.SetGroup(Course);

            int courseId = await _courseService.AddCourse(Course,ImageFile,DemoFile);
            if(courseId!=0)
                ViewData["IsSucceed"] = true;
            else
                ViewData["IsSucceed"] = false;
            return Page();
        }
    }
}