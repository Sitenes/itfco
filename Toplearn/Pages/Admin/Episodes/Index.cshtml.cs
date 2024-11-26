using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PagedList;
using Toplearn.Core.DTOs.CourseVM;
using Toplearn.Core.Services.Interfaces;

namespace Toplearn.Web.Pages.Admin.Episodes
{
    public class IndexModel : PageModel
    {
        private readonly ICourseService _courseService;
        public IPagedList<EpisodeListViewModel> Episodes { get; set; }

        [BindProperty]
        public FilterEpisodeListViewModel Filters { get; set; }

        [BindProperty]
        public int ItemPerPage { get; set; }
        public IndexModel(ICourseService courseService)
        {
            _courseService = courseService;
        }
        public void OnGetAsync([FromRoute] int id, [FromRoute] bool? IsSucceed)
        {
            if (IsSucceed != null)
                ViewData["IsSucceed"] = IsSucceed;
            Filters = new FilterEpisodeListViewModel();
            Filters.CourseId = id;
            ItemPerPage = 10;
            Episodes = _courseService.GetEpisodesList(Filters).ToPagedList(1, ItemPerPage) as PagedList<EpisodeListViewModel>;
        }
        public IActionResult OnPost([FromRoute]int id ,int currentPage)
        {
            if (currentPage == 0)
                ++currentPage ;
            Filters.CourseId = id;
            Episodes = _courseService.GetEpisodesList(Filters).ToPagedList(currentPage, ItemPerPage) as PagedList<EpisodeListViewModel>;
            return Page();
        }
    }
}
