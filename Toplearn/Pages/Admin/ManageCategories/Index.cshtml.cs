using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
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
    [PermissionChecker(Core.AllEnums.PermissionEnum.CategoryManagement)]
    public class IndexModel : PageModel
    {
        private readonly ICourseService _CourseService;

        public IndexModel(ICourseService CourseService)
        {
            _CourseService = CourseService;
        }

        public List<CategoryDto>? Category { get;set; }

        public async Task OnGetAsync(bool IsSucceed)
        {
            if (IsSucceed == true)
                ViewData["IsSucceed"] = true;
			Category = await _CourseService.GetAllCourseGroups();
        }
    }
}
