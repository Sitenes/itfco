using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using itfco.ViewModel.DTOs.TeacherVM;
using itfco.Service.Services.Interfaces;
using itfco.Entity.Entities.Permissions.Products;
using static System.Net.Mime.MediaTypeNames;

namespace itfco.Web.Pages.Admin.Episodes
{

    public class EditModel : PageModel
    {
        private readonly IProductService _courseService;
        public EditModel(IProductService courseService)
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
                return Redirect($"/Admin/Episodes/Index/{Episode.ProductId}/true");
            }
            else
                ViewData["IsSucceed"] = false;
            return Page();
        }
    }
}