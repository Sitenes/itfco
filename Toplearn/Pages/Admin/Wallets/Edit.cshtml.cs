using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Toplearn.Core.DTOs.TeacherVM;
using Toplearn.Core.Services.Interfaces;
using Toplearn.DataLayer.Entities.Courses;
using static System.Net.Mime.MediaTypeNames;

namespace Toplearn.Web.Pages.Admin.Wallets
{

    public class EditModel : PageModel
    {
        private readonly ICartService _cartService;
        public EditModel(ICartService cartService)
        {
            _cartService = cartService;
        }

        [BindProperty]
        public Cart Cart { get; set; }
        public async Task OnGetAsync([FromRoute] int id,bool? IsSucceed = null)
        {
            if (IsSucceed != null)
                ViewData["IsSucceed"] = IsSucceed;
         
            Cart = await _cartService.GetCartAsync(id);
            if (Cart == null)
                BadRequest();

        }
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
                return Page();

            try
            {
                // دریافت مدل قبلی از دیتابیس
                var existingCart = await _cartService.GetCartAsync(Cart.Id);
                if (existingCart == null)
                    return NotFound();

                // به‌روزرسانی مقادیر ویرایش‌شده
                existingCart.FirstName = Cart.FirstName;
                existingCart.LastName = Cart.LastName;
                existingCart.City = Cart.City;
                existingCart.Email = Cart.Email;
                existingCart.Phone = Cart.Phone;
                existingCart.Notes = Cart.Notes;
                existingCart.IsPaid = Cart.IsPaid;
                existingCart.State = Cart.State;
                existingCart.Postcode = Cart.Postcode;
                existingCart.CompanyName = Cart.CompanyName;
                existingCart.Country = Cart.Country;
                existingCart.Address = Cart.Address;
                existingCart.AddressAdditional = Cart.AddressAdditional;
                existingCart.DiscountId = Cart.DiscountId;
                // ذخیره تغییرات
                _cartService.UpdateCart(existingCart);
                await _cartService.SaveChangesAsync();
            }
            catch (Exception)
            {
                return Redirect("/Admin/Wallets/Index?IsSucceed=false");
            }

            return Redirect("/Admin/Wallets/Index?IsSucceed=true");
        }

    }
}