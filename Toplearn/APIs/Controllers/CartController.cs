using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Toplearn.Core.Services.Interfaces;
using Toplearn.DataLayer.Context;

namespace Toplearn.Web.APIs.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly ICartService _cartService;

        public CartController(ICartService cartService)
        {
            _cartService = cartService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Delete([FromRoute]int? id)
        {
            if (id == null)
                return NotFound();
            if(!await _cartService.DeleteCartAsync(id.Value))
                return Redirect($"/Admin/Wallets/Index?IsSucceed=false");
            await _cartService.SaveChangesAsync();
            return Redirect($"/Admin/Wallets/Index?IsSucceed=true");
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> DeleteByUser([FromRoute] int? id)
        {
            if (id == null)
                return NotFound();
            if (!await _cartService.DeleteCartAsync(id.Value))
                return Redirect($"/UserPanel/CartList");
            await _cartService.SaveChangesAsync();
            return Redirect($"/UserPanel/CartList");
        }
        [HttpGet("{courseId}/{cartId}")]
        public async Task<IActionResult> RemoveProduct([FromRoute]int courseId, [FromRoute] int cartId)
        {

            var cart = await _cartService.GetCartAsync(cartId);
            if (cart == null)
                return NotFound();

            await _cartService.RemoveProductFromCartAsync(cart.Id, courseId);
            await _cartService.SaveChangesAsync();
            return Redirect($"/Admin/Wallets/edit/{cart.Id}?IsSucceed=true");
        }
        [HttpGet("{courseId}/{cartId}")]
        public async Task<IActionResult> RemoveProductByUser([FromRoute] int courseId, [FromRoute] int cartId)
        {

            var cart = await _cartService.GetCartAsync(cartId);
            if (cart == null)
                return NotFound();

            await _cartService.RemoveProductFromCartAsync(cart.Id, courseId);
            await _cartService.SaveChangesAsync();
            return Redirect($"/UserPanel/Cart");
        }
    }
}
