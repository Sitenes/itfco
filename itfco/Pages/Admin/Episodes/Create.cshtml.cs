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

namespace itfco.Web.Pages.Admin.Episodes
{

    public class CreateModel : PageModel
    {
        private readonly IProductService _courseService;
        public CreateModel(IProductService courseService)
        {
            _courseService = courseService;
        }
        [BindProperty]
        public Episode Episode { get; set; }
        public void OnGet([FromRoute]int ProductId)
        {
            if (ProductId == 0)
                Redirect($"/Admin/Episodes/Index/{ProductId}/false");
            Episode = new Episode();
            Episode.ProductId = ProductId;
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