using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Toplearn.Core.AllEnums;
using Toplearn.Core.DTOs.WalletVM;
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


		public async Task<List<Cart>> GetAllCartsAsync(CartFilterViewModel filter)
		{
			IQueryable<Cart> query = _context.Carts.Include(x=>x.UserCreator).Include(x => x.CourseCarts).ThenInclude(x=>x.Course);

			if (filter.UserId.HasValue)
				query = query.Where(c => c.UserCreatorId == filter.UserId);

			if (filter.IsPaid.HasValue)
				query = query.Where(c => c.IsPaid == filter.IsPaid);

			if (!string.IsNullOrEmpty(filter.FilterEmail))
				query = query.Where(c => c.UserCreator.Email.Contains(filter.FilterEmail));

			if (!string.IsNullOrEmpty(filter.FilterNameId))
				query = query.Where(c => c.UserCreator.UserName.Contains(filter.FilterNameId) || c.UserCreator.UserId.ToString().Contains(filter.FilterNameId) || c.Id.ToString() == filter.FilterNameId);

			if (filter.FilterPhone.HasValue)
				query = query.Where(c => c.Phone.Contains(filter.FilterPhone.ToString()) || c.UserCreator.Phone.ToString().Contains(filter.FilterPhone.ToString()));

			if (filter.OnlyActivate.HasValue && filter.OnlyActivate.Value)
				query = query.Where(c => c.UserCreator.IsActive);

			if (filter.PriceFrom.HasValue)
				query = query.Where(c => c.CourseCarts.Sum(x=>x.Course.Price * x.Count) >= filter.PriceFrom.Value);

			if (filter.PriceTo.HasValue)
				query = query.Where(c => c.CourseCarts.Sum(x => x.Course.Price * x.Count) <= filter.PriceTo.Value);

			if (filter.ItemPerPage > 0 && filter.CurrentPage > 0)
			{
				int skip = (filter.CurrentPage - 1) * filter.ItemPerPage;
				query = query.Skip(skip).Take(filter.ItemPerPage);
			}

			return await query.ToListAsync();
		}

		public async Task<Cart> GetLastNotPaidCartAsync(Guid UserId)
        {
            var cart = await _context.Carts.Include(x=>x.CourseCarts).ThenInclude(x=>x.Course).OrderBy(x=>x.CreateDate).LastOrDefaultAsync(c => c.UserCreatorId == UserId && c.IsPaid == false);
            if(cart == null)
            {
                cart = new Cart() { UserCreatorId = UserId, };
                await AddCartAsync(cart);
            }

            return cart;
        }
        public async Task<Cart> GetCartAsync(int CartId)
        {
            return await _context.Carts.Include(x=>x.UserCreator).Include(x => x.CourseCarts).ThenInclude(x=>x.Course).FirstOrDefaultAsync(n => n.Id == CartId);
        }

        public async Task AddCartAsync(Cart cart)
        {
            cart.CreateDate = DateTime.Now;
            await _context.Carts.AddAsync(cart);
        }
        public async Task AddOrUpdateProductInCartAsync(int cartId, int courseId, int count)
        {
            // بررسی وجود محصول در سبد خرید
            var existingCourseCart = await _context.CourseCarts
                .FirstOrDefaultAsync(cc => cc.CartId == cartId && cc.CourseId == courseId);

            if (existingCourseCart != null)
            {
                // به‌روزرسانی تعداد محصول
                existingCourseCart.Count = count;
                _context.CourseCarts.Update(existingCourseCart);
            }
            else
            {
                // اضافه کردن محصول جدید
                await _context.CourseCarts.AddAsync(new CourseCart
                {
                    Count = count,
                    CartId = cartId,
                    CourseId = courseId
                });
            }
        }

        public void UpdateCart(Cart cart)
        {
            _context.Carts.Update(cart);
        }

        public async Task<bool> DeleteCartAsync(int CartId)
        {
            var cart = await _context.Carts.SingleOrDefaultAsync(n => n.Id == CartId);
            if (cart != null)
            {
                _context.Carts.Remove(cart);
            }
            else
                return false;
            return true;
        }
        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
        public async Task RemoveProductFromCartAsync(int cartId, int courseId)
        {
            var courseCart = await _context.CourseCarts
                .FirstOrDefaultAsync(cc => cc.CartId == cartId && cc.CourseId == courseId);

            if (courseCart != null)
            {
                _context.CourseCarts.Remove(courseCart);
            }
        }
        public async Task<int> CountProductInCartAsync(Guid UserId)
        {
            var courseCount = await _context.CourseCarts.Include(x=>x.Cart)
                .CountAsync(x => x.Cart.UserCreatorId == UserId && !x.Cart.IsPaid);

            return courseCount;
        }
    }
}
