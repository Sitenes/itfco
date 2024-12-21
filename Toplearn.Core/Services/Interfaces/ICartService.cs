using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Toplearn.DataLayer.Entities.Courses;

namespace Toplearn.Core.Services.Interfaces
{

    public interface ICartService
    {
        Task<List<Cart>> GetAllCartsAsync(Guid? UserId = null, bool? IsPaid = null);
        Task<Cart> GetCartAsync(int CartId);
        Task AddCartAsync(Cart cart);
        void UpdateCart(Cart cart);
        Task DeleteCartAsync(int CartId);
        Task SaveChangesAsync();
    }
}
