using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Toplearn.Core.DTOs.CourseVM;
using Toplearn.Core.Security;
using Toplearn.Core.Services;
using Toplearn.Core.Services.Interfaces;
using Toplearn.DataLayer.Context;
using Toplearn.DataLayer.Entities.Courses;
using Toplearn.DataLayer.Entities.User;

namespace Toplearn.Web.Pages.Admin.ManageCategories
{
    [PermissionChecker(Core.AllEnums.PermissionEnum.EditCategory)]
    public class EditModel : PageModel
    {
        private readonly ICourseService _courseService;

        public EditModel(ICourseService _courseService)
        {
			this._courseService = _courseService;

        }

        [BindProperty]
        public CategoryDto Category { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if(id == null)
                return NotFound();
			Category = await _courseService.GetCategoryById(id.GetValueOrDefault());
			if (Category == null || Category.Id == 0) return NotFound();

			var categories = await _courseService.GetAllCourseGroups();
			ViewData["Categories"] = categories;
			
            return Page();
		}

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
			await _courseService.UpdateCategory(Category);
			await _courseService.SaveChanges();
            return RedirectToPage("./Index",true);
        }

    }
}
