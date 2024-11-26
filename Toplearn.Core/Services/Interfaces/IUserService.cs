using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Toplearn.Core.DTOs.UserVM;
using Toplearn.DataLayer.Entities.User;

namespace Toplearn.Core.Services
{
    public interface IUserService
    {
        #region Validate User
        bool IsEmailLinkValid(Guid emailLink);
        bool IsUserExist (Guid User);
        bool IsExistEmail(string Email);
        bool IsExistPhone(long Phone);
        bool IsExistUserName(string UserName);
        bool IsActivated(string UserName);
        bool IsActivated(Guid UserId);
        bool IsActivated(User User);
        #endregion

        #region Find User
        IEnumerable<User> GetUsers();
        User GetUser(string Email);
        User GetUser(Guid UserId);
        UserDetailViewModel GetUserDetails(Guid UserId);
        User GetUser(User User);
        string GetAvaterByUserName(string UserName);
        User GetUserByName(string UserName);
        UsersForAdminViewModel FilterUser(UsersForAdminViewModel filters);

        #endregion

        #region Change User

        bool DeleteUser(Guid UserId);
        bool DeleteUser(User User);
        bool UpdateUser(User User);
        void SaveChanges();
        bool AddUser(User User);
        #endregion

        #region Async
        Task<User> GetUserAsync(Guid UserId);
        Task SaveChangesAsync();
        Task<bool> DeleteUserAsync(Guid UserId);
        Task<bool> AddUserAsync(User User);
        #endregion

    }
}
