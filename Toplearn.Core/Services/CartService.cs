using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Toplearn.Core.Services.Interfaces;
using Toplearn.DataLayer.Context;
using Toplearn.DataLayer.Entities.Courses;

namespace Toplearn.Core.Services
{
    public class CartService : ICartService
    {
        private readonly ToplearnContext _context;
        public CartService(ToplearnContext context)
        {
            _context = context;
        }


        public async Task<List<Cart>> GetAllCartsAsync(Guid? UserId = null, bool? IsPaid = null)
        {
            IQueryable<Cart> query = _context.Carts;

            if (UserId.HasValue)
            {
                query = query.Where(c => c.UserCreatorId == UserId);
            }

            if (IsPaid.HasValue)
            {
                query = query.Where(c => c.IsPaid == IsPaid);
            }

            return await query.ToListAsync();
        }

        public async Task<Cart> GetCartAsync(int CartId)
        {
            return await _context.Carts.SingleOrDefaultAsync(n => n.Id == CartId);
        }

        public async Task AddCartAsync(Cart cart)
        {
            await _context.Carts.AddAsync(cart);
        }

        public void UpdateCart(Cart cart)
        {
            _context.Carts.Update(cart);
        }

        public async Task DeleteCartAsync(int CartId)
        {
            var cart = await _context.Carts.SingleOrDefaultAsync(n => n.Id == CartId);
            if (cart != null)
            {
                _context.Carts.Remove(cart);
            }
        }
        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
