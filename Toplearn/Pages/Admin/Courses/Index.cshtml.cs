using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Toplearn.Core.DTOs.CourseVM;
using Toplearn.Core.Services.Interfaces;
using Toplearn.DataLayer.Entities.Courses;

namespace Toplearn.Web.Pages.Admin.Courses
{
    public class IndexModel : PageModel
    {

        private readonly ICourseService _courseService;
        public List<CourseListAdminViewModel> Courses { get; set; }

        [BindProperty]
        public CourseFilterAdminViewModel Filters { get; set; }

        [BindProperty]
        public int ItemPerPage { get; set; }
        public IndexModel(ICourseService courseService)
        {
            _courseService = courseService;
        }
        public async Task OnGetAsync(bool? IsSucceed)
        {
            if(IsSucceed!=null)
                ViewData["IsSucceed"] = IsSucceed;
            Filters = new CourseFilterAdminViewModel();
            Filters.ItemPerPage = 10;
            ItemPerPage = 10;
            Filters.CurrentPage = 1;
            Courses = (await _courseService.GetCoursesList(Filters)).ToList();
            Courses?.ForEach(x => x.Image = string.IsNullOrEmpty(x.Image) ? "Default.jpg" : x.Image);

		}
        public async Task<IActionResult> OnPostAsync(int currentPage)
        {
            Filters.ItemPerPage = ItemPerPage;
            Filters.CurrentPage = currentPage;
            Courses = (await _courseService.GetCoursesList(Filters)).ToList();
            return Page();
        }
    }
}
