using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using itfco.ViewModel.DTOs.ProductVM;
using itfco.Service.Services.Interfaces;
using itfco.Entity.Entities.Permissions.Products;

namespace itfco.Web.Pages.Admin.Products
{
    public class IndexModel : PageModel
    {

        private readonly IProductService _courseService;
        public List<ProductListAdminViewModel> Products { get; set; }

        [BindProperty]
        public ProductFilterAdminViewModel Filters { get; set; }

        [BindProperty]
        public int ItemPerPage { get; set; }
        public IndexModel(IProductService courseService)
        {
            _courseService = courseService;
        }
        public void OnGetAsync(bool? IsSucceed)
        {
            if(IsSucceed!=null)
                ViewData["IsSucceed"] = IsSucceed;
            Filters = new ProductFilterAdminViewModel();
            Filters.ItemPerPage = 10;
            ItemPerPage = 10;
            Filters.CurrentPage = 1;
            Products = _courseService.GetProductsList(Filters).Result as List<ProductListAdminViewModel>;
        }
        public async Task<IActionResult> OnPostAsync(int currentPage)
        {
            Filters.ItemPerPage = ItemPerPage;
            Filters.CurrentPage = currentPage;
            Products = await _courseService.GetProductsList(Filters) as List<ProductListAdminViewModel>;
            return Page();
        }
    }
}
