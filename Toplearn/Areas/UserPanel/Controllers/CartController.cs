using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Toplearn.Core.Services;
using Toplearn.Core.Services.Interfaces;
using Toplearn.DataLayer.Entities.Courses;
using Toplearn.DataLayer.Entities.User;

namespace Toplearn.Web.Areas.UserPanel.Controllers
{
    //[Route("/{language}/")]
    [Authorize]
    [Area("UserPanel")]
    public class CartController : Controller
    {
        private readonly ICartService _cartService;
        private readonly IUserService _userService;
        private readonly IStringLocalizer<Resource> _localizer;
        private readonly ICourseService _courseService;
        public CartController(ICartService cartService, IUserService userService, IStringLocalizer<Resource> localizer, ICourseService courseService)
        {
            _courseService = courseService;
            _cartService = cartService;
            _userService = userService;
            _localizer = localizer;
        }
        [Route("UserPanel/Cart")]
        public async Task<IActionResult> Index(int productId = 0, int Count = 1)
        {
            var user = _userService.GetUser(Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)));
            if (user == null)
                return Redirect($"/login?ReturnUrl=/UserPanel/Cart/Index?productId={productId}&Count={Count}");
            var cart = await _cartService.GetLastNotPaidCartAsync(user.UserId);
            var product = await _courseService.GetCourse(productId);
            if(product != null)
                await _cartService.AddOrUpdateProductInCartAsync(cart.Id,productId,Count);
            await _cartService.SaveChangesAsync();
			cart = await _cartService.GetLastNotPaidCartAsync(user.UserId);
			return View(cart);
        }
        [Route("UserPanel/Cart/Checkout")]
        public async Task<IActionResult> Checkout()
        {
            var user = _userService.GetUser(Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)));
            if (user == null)
                return Redirect($"/login?ReturnUrl=/UserPanel/Cart/Index");
            var cart = await _cartService.GetLastNotPaidCartAsync(user.UserId);

            return View(cart);
        }
        [Route("UserPanel/Cart/Payment")]
        public async Task<IActionResult> Payment(Cart cart)
        {
            var cartOld = await _cartService.GetCartAsync(cart.Id);
            var user = _userService.GetUser(Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)));
            if (user == null)
                return Redirect($"/login?ReturnUrl=/UserPanel/Cart/Index");
            cartOld.Address = cart.Address;
            cartOld.AddressAdditional = cart.AddressAdditional;
            cartOld.City = cart.City;
            cartOld.CompanyName = cart.CompanyName;
            cartOld.Country = cart.Country;
            cartOld.Email = cart.Email;
            cartOld.FirstName = cart.FirstName;
            cartOld.LastName = cart.LastName;
            cartOld.Notes = cart.Notes;
            cartOld.Phone = cart.Phone;
            cartOld.Postcode = cart.Postcode;
            cartOld.State = cart.State;

            _cartService.UpdateCart(cartOld);
            await _cartService.SaveChangesAsync();
            return View(cart);
        }
        [Route("UserPanel/Cart/RemoveProduct/{courseId}")]
        public async Task<IActionResult> RemoveProduct(int courseId)
        {
            var user = _userService.GetUser(Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)));
            if (user == null)
                return Redirect("/login");

            var cart = await _cartService.GetLastNotPaidCartAsync(user.UserId);
            if (cart == null)
                return NotFound();

            await _cartService.RemoveProductFromCartAsync(cart.Id, courseId);
            await _cartService.SaveChangesAsync();
            return RedirectToAction("Index");
        }

    }
}
