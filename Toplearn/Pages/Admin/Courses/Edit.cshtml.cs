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

    public class EditModel : PageModel
    {
        private readonly ICourseService _courseService;
        public EditModel(ICourseService courseService)
        {
            _courseService = courseService;
        }

        [BindProperty]
        public Course Course { get; set; }
        public void OnGet([FromRoute]int id)
        {
            ViewData["Groups"] = _courseService.GetGroups() as List<CourseGroup>;
            ViewData["Teachers"] = _courseService.GetTeachersName();
            ViewData["Statuses"] = _courseService.GetStatuses();
            ViewData["Levels"] = _courseService.GetLevels();
            Course = _courseService.GetCourse(id).Result;
            if (Course == null)
                BadRequest();
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

            bool IsSucceed = await _courseService.Update(Course,ImageFile,DemoFile);
            if (IsSucceed)
            {
                await _courseService.SaveChanges();
                return Redirect("/Admin/Courses/Index?IsSucceed=true");
            }
            else
            return Redirect("/Admin/Courses/Index?IsSucceed=false");
        }
    }
}