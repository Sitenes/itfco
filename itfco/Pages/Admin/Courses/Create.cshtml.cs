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

namespace itfco.Web.Pages.Admin.Products
{

    public class CreateModel : PageModel
    {
        private readonly IProductService _courseService;
        public CreateModel(IProductService courseService)
        {
            _courseService = courseService;
        }
        [BindProperty]
        public Product Product { get; set; }
        public void OnGet()
        {
            ViewData["Groups"] = _courseService.GetGroups() as List<Group>;
            ViewData["Teachers"] = _courseService.GetTeachersName();
            ViewData["Statuses"] = _courseService.GetStatuses();
            ViewData["Levels"] = _courseService.GetLevels();
        }
        public async Task<IActionResult> OnPostAsync(IFormFile ImageFile, IFormFile DemoFile)
        {
            ViewData["Groups"] = _courseService.GetGroups() as List<Group>;
            ViewData["Teachers"] = _courseService.GetTeachersName();
            ViewData["Statuses"] = _courseService.GetStatuses();
            ViewData["Levels"] = _courseService.GetLevels();
            if (!ModelState.IsValid)
                return Page();
            Product = await _courseService.SetGroup(Product);

            int courseId = await _courseService.AddProduct(Product,ImageFile,DemoFile);
            if(courseId!=0)
                ViewData["IsSucceed"] = true;
            else
                ViewData["IsSucceed"] = false;
            return Page();
        }
    }
}