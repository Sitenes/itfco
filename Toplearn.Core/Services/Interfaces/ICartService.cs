using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Toplearn.Core.DTOs.WalletVM;
using Toplearn.DataLayer.Entities.Courses;

namespace Toplearn.Core.Services.Interfaces
{

    public interface ICartService
    {
        Task<List<Cart>> GetAllCartsAsync(CartFilterViewModel filter);
        Task<Cart> GetCartAsync(int CartId);
        Task AddCartAsync(Cart cart);
        void UpdateCart(Cart cart);
		Task<bool> DeleteCartAsync(int CartId);
        Task SaveChangesAsync();
        Task AddOrUpdateProductInCartAsync(int cartId, int courseId, int count);
        Task<Cart> GetLastNotPaidCartAsync(Guid UserId);
        Task RemoveProductFromCartAsync(int cartId, int courseId);
        Task<int> CountProductInCartAsync(Guid? UserId);
    }
}
