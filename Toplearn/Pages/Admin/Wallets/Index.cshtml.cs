using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Toplearn.Core.DTOs.WalletVM;
using Toplearn.Core.Services.Interfaces;
using Toplearn.DataLayer.Entities.Courses;

namespace Toplearn.Web.Pages.Admin.Wallets
{
	public class IndexModel : PageModel
	{
		private readonly ICartService _cartService;
		public List<Cart> Carts { get; set; }

		[BindProperty]
		public CartFilterViewModel Filters { get; set; }

		[BindProperty]
		public int ItemPerPage { get; set; }

		public IndexModel(ICartService cartService)
		{
			_cartService = cartService;
		}

		public async Task OnGetAsync(bool? IsSucceed)
		{
			if (IsSucceed != null)
				ViewData["IsSucceed"] = IsSucceed;

			Filters = new CartFilterViewModel
			{
				ItemPerPage = 10,
				CurrentPage = 1
			};

			ItemPerPage = 10;

			Carts = await _cartService.GetAllCartsAsync(Filters);
		}

		public async Task<IActionResult> OnPostAsync(int currentPage)
		{
			//Filters.ItemPerPage = ItemPerPage;
			Filters.CurrentPage = currentPage;

			Carts = await _cartService.GetAllCartsAsync(Filters);

			return Page();
		}
	}
}
