using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Toplearn.Core.DTOs.CourseVM;
using Toplearn.Core.Security;
using Toplearn.Core.Services;
using Toplearn.Core.Services.Interfaces;
using Toplearn.DataLayer.Context;
using Toplearn.DataLayer.Entities.Courses;
using Toplearn.DataLayer.Entities.User;

namespace Toplearn.Web.Pages.Admin.ManageCategories
{
    [PermissionChecker(Core.AllEnums.PermissionEnum.AddCategory)]
    public class CreateModel : PageModel
    {
        private readonly ICourseService _courseService;
        public CreateModel(ICourseService courseService)
        {
            _courseService = courseService;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var categories = await _courseService.GetAllCourseGroups();
            ViewData["Categories"] = categories;
            return Page();
        }

        [BindProperty]
        public CourseGroup? Category { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPost(int? ParentId)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            await _courseService.AddCategory(new CategoryDto
            {
				Id = Category.GroupId,
				ParentId = ParentId,
                NameArabic = Category.NameArabic,
                NameEnglish = Category.NameEnglish,
                NamePersian = Category.NamePersian,
                
			});

            await _courseService.SaveChanges();

            return RedirectToPage("./Index",true);
        }
    }
}
