using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Toplearn.DataLayer.Entities.Courses;
using System.Collections.Generic;
using System.Threading.Tasks;
using Toplearn.Core.Services.Interfaces;

namespace Toplearn.Web.Pages.Admin.Courses
{
    public class DiscountsModel : PageModel
    {
        private readonly ICourseService _courseService;

        public DiscountsModel(ICourseService courseService)
        {
            _courseService = courseService;
        }

        public List<Discount> Discounts { get; set; }
        [BindProperty]
        public Discount EditingDiscount { get; set; }
        public bool IsEditing { get; set; } = false;

        public async Task OnGetAsync()
        {
            Discounts = await _courseService.GetAllDiscountsAsync();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            ModelState.Remove("EditingDiscount.Id");
            if (!ModelState.IsValid)
            {
                Discounts = await _courseService.GetAllDiscountsAsync();
                return Page();
            }

            if (EditingDiscount.Id == 0)
            {
                await _courseService.AddDiscountAsync(EditingDiscount);
            }
            else
            {
                await _courseService.UpdateDiscountAsync(EditingDiscount);
            }

            return RedirectToPage();
        }
        
        public async Task<IActionResult> OnGetDeleteAsync(int id)
        {
            await _courseService.DeleteDiscountAsync(id);
            return Redirect("/Admin/Courses/Discount");
        }
    }
}
