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

namespace Toplearn.Web.Pages.Admin.Episodes
{

    public class CreateModel : PageModel
    {
        private readonly ICourseService _courseService;
        public CreateModel(ICourseService courseService)
        {
            _courseService = courseService;
        }
        [BindProperty]
        public Episode Episode { get; set; }
        public void OnGet([FromRoute]int CourseId)
        {
            if (CourseId == 0)
                Redirect($"/Admin/Episodes/Index/{CourseId}/false");
            Episode = new Episode();
            Episode.CourseId = CourseId;
        }
        public async Task<IActionResult> OnPostAsync(IFormFile Video)
        {

            if (!ModelState.IsValid)
                return Page();

            int EpisodeId = await _courseService.AddEpisode(Episode, Video);
            if(EpisodeId != 0)
                ViewData["IsSucceed"] = true;
            else
                ViewData["IsSucceed"] = false;
            return Page();
        }
    }
}