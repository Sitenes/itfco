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
                return Redirect($"/login?ReturnUrl=?/UserPanel/Cart/Index?productId={productId}&Count={Count}");
            var carts = await _cartService.GetAllCartsAsync(IsPaid: false);
            var cart = carts.FirstOrDefault();
            var product = await _courseService.GetCourse(productId);

            if (cart == null)
            {
                cart = new Cart()
                {
                    FirstName = user.UserName,
                    Courses = product == null?null: new List<Course> { product }
                };
                await _cartService.AddCartAsync(cart);
            }
            else
            {
                cart.Courses.Add(product);
            }
            await _cartService.SaveChangesAsync();

            return View(cart);
        }

    }
}
