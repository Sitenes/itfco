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

namespace Toplearn.Web.Pages.Admin.Episodes
{

    public class EditModel : PageModel
    {
        private readonly ICourseService _courseService;
        public EditModel(ICourseService courseService)
        {
            _courseService = courseService;
        }
        [BindProperty]
        public Episode Episode { get; set; }
        public void OnGet([FromRoute]int EpisodeId)
        {
            Episode = _courseService.GetEpisode(EpisodeId).Result;
        }
        public async Task<IActionResult> OnPost(IFormFile Video)
        {

            if (!ModelState.IsValid)
                return Page();

            bool updateSeccussfully = await _courseService.EditEpisode(Episode, Video);
            if (updateSeccussfully)
            {
                ViewData["IsSucceed"] = true;
                return Redirect($"/Admin/Episodes/Index/{Episode.CourseId}/true");
            }
            else
                ViewData["IsSucceed"] = false;
            return Page();
        }
    }
}