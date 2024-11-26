using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.PeerToPeer;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Toplearn.Core.Convertors;
using Toplearn.Core.DTOs.UserVM;
using Toplearn.DataLayer.Context;
using Toplearn.DataLayer.Entities.User;
using TopLearn.Core.Security;

namespace Toplearn.Core.Services
{
    public class UserService : Controller,IUserService
    {
        private readonly ToplearnContext _context;
        private readonly IMemoryCache _cache;
        private readonly MemoryCacheEntryOptions _cacheOption;
        public UserService(ToplearnContext context, IMemoryCache cache)
        {
            _context = context;
            _cache = cache;
            _cacheOption = new MemoryCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromMinutes(15));
        }

        #region Validating User
        public bool IsActivated(string UserName)
        {

            return GetUser(UserName).IsActive;
        }

        public bool IsActivated(Guid UserId)
        {

            return GetUser(UserId).IsActive;
        }

        public bool IsActivated(User User)
        {
            return GetUser(User).IsActive;
        }

        public bool IsExistEmail(string Email)
        {
            if (_context.Users.Any(n => n.Email == Email))
                return true;
            return false;
        }

        public bool IsExistPhone(long Phone)
        {
            if (_context.Users.Any(n => n.Phone == Phone))
                return true;
            return false;
        }

        public bool IsExistUserName(string UserName)
        {
            if (_context.Users.Any(n => n.UserName == UserName))
                return true;
            return false;
        }
        #endregion

        #region Change User
        public bool DeleteUser(Guid UserId)
        {
            try
            {
                User user = GetUser(UserId);
                user.IsDeleted = true;
                UpdateUser(user);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool DeleteUser(User User)
        {
            try
            {
                User user = GetUser(User);
                user.IsDeleted = true;
                UpdateUser(user);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool UpdateUser(User User)
        {
            try
            {
                _context.Users.Update(User);


                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        #endregion

        #region Get User
        public User GetUser(string Email)
        {
            Email = FixText.FixEmail(Email);
            return _context.Users.FirstOrDefault(n => n.Email == Email);
        }

        public User GetUser(Guid UserId)
        {
            return _context.Users.Find(UserId);
        }

        public User GetUser(User User)
        {
            if (User.UserId == null || User.UserId == default)
                return GetUser(User.UserName);
            else
                return GetUser(User.UserId);

        }

        public UserDetailViewModel GetUserDetails(Guid UserId)
        {
            return _context.Users.Where(n => n.UserId == UserId).Select(n => new UserDetailViewModel()
            {
                RegisterDate = n.RegisterDate,
                Email = n.Email,
                IsActive = n.IsActive,
                IsDeleted = n.IsDeleted,
                UserAvatar = n.UserAvatar,
                Wallet = n.Wallet,
                UserName = n.UserName,
                Phone = n.Phone
            }).SingleOrDefault();
        }
        #endregion

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        public bool IsUserExist(Guid User)
        {
            return _context.Users.Any(n => n.UserId == User);
        }

        public bool AddUser(User User)
        {
            try
            {
                #region check validation and replace with default value
                if (User.UserId == null || User.UserId == Guid.Empty)
                    User.UserId = Guid.NewGuid();
                if (string.IsNullOrWhiteSpace(User.UserName))
                    User.UserName = "Default/" + Guid.NewGuid().ToString().Replace("-", "");
                if (User.RegisterDate == null || User.RegisterDate == DateTime.MinValue)
                    User.RegisterDate = DateTime.Now;
                if (string.IsNullOrWhiteSpace(User.UserAvatar))
                    User.UserAvatar = "Default.png";
                if (string.IsNullOrWhiteSpace(User.Password))
                    User.Password = PasswordHelper.EncodePasswordMd5("user123456");
                if (User.Wallet < 0)
                    User.Wallet = 0;
                if (User.ExpireEmailLink == null || User.ExpireEmailLink == DateTime.MinValue)
                    User.ExpireEmailLink = DateTime.Now;
                if (User.EmailLink == null|| User.EmailLink == Guid.Empty)
                    User.EmailLink = Guid.NewGuid();
                #endregion

                _context.Users.Add(User);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool IsEmailLinkValid(Guid emailLink)
        {
            return _context.Users.Any(n => n.EmailLink == emailLink);
        }


        public User GetUserByName(string UserName)
        {
            return _context.Users.
                SingleOrDefault(n => n.UserName == UserName);
        }

        public string GetAvaterByUserName(string UserName)
        {
            return _context.Users.
                Where(n => n.UserName == UserName).
                Select(n => n.UserAvatar).SingleOrDefault();
        }

        public UsersForAdminViewModel FilterUser(UsersForAdminViewModel filters)
        {
            int count = (filters.UserListCount == 0) ? 10 : filters.UserListCount;
            IQueryable<UserListViewModel> users = _context.Users.Select(n => new UserListViewModel()
            {
                Create = n.RegisterDate,
                Email = n.Email,
                Phone = n.Phone,
                UserName = n.UserName,
                Wallet = n.Wallet,
                UserId = n.UserId,
                IsActive = n.IsActive
            });
            if (filters.filterPhone != 0)
            {
                string searchPhone = filters.filterPhone.ToString();
                users = users.Where(n => n.Phone.ToString().Contains(searchPhone));
            }

            if (!string.IsNullOrWhiteSpace(filters.filterNameId))
            {
                if (filters.filterNameId.Contains('-'))
                    users = users.Where(n => n.UserId.ToString() == filters.filterNameId);
                else
                    users = users.Where(n => n.UserName.Contains(filters.filterNameId));
            }

            if (!string.IsNullOrWhiteSpace(filters.filterEmail))
            {
                users = users.Where(n => n.Email.Contains(filters.filterEmail));
            }

            if (filters.OnlyActivate)
            {
                users = users.Where(n => n.IsActive == true);
            }


            int Take = (filters.UserListCount == 0) ? 10 : filters.UserListCount;
            int Skip = Take * ((filters.CurrentPage == 0) ? 0 : filters.CurrentPage - 1);

            filters.NumAllUser = users.Count();
            users = users.Skip(Skip);
            users = users.Take(Take);
            filters.Users = users.ToList();
            if (count != 0)
            {
                filters.PagesCount = filters.NumAllUser / count;
                if (filters.NumAllUser % count != 0)
                    filters.PagesCount++;
            }
            else
                filters.PagesCount = 1;

            return filters;
        }

        public async Task<User> GetUserAsync(Guid UserId)
        {

            var userCache = await _cache.GetOrCreateAsync<User>(UserId, async options =>
            {
                options.SetOptions(_cacheOption);
                var getUser = await _context.Users.Include(n=>n.UserRole).SingleOrDefaultAsync(n=>n.UserId == UserId);
                options.SetValue(getUser);
                return getUser;
            });
            return userCache;
        }
        public IEnumerable<User> GetUsers()
        {
            List<User> users = _context.Users.ToList();
               
            return users;
        }


        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task<bool> DeleteUserAsync(Guid id)
        {
            try
            {
                User user = await _context.Users.FindAsync(id);
                _context.Entry(user).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                user.IsDeleted = true;
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> AddUserAsync(User User)
        {

            try
            {
                #region check validation and replace with default value
                if (User.UserId == null || User.UserId == Guid.Empty)
                    User.UserId = Guid.NewGuid();
                if (string.IsNullOrWhiteSpace(User.UserName))
                    User.UserName = "Default/" + Guid.NewGuid().ToString().Replace("-", "");
                if (User.RegisterDate == null || User.RegisterDate == DateTime.MinValue)
                    User.RegisterDate = DateTime.Now;
                if (string.IsNullOrWhiteSpace(User.UserAvatar))
                    User.UserAvatar = "Default.png";
                if (string.IsNullOrWhiteSpace(User.Password))
                    User.Password = PasswordHelper.EncodePasswordMd5("user123456");
                if (User.Wallet < 0)
                    User.Wallet = 0;
                if (User.ExpireEmailLink == null || User.ExpireEmailLink == DateTime.MinValue)
                    User.ExpireEmailLink = DateTime.Now;
              
                    User.EmailLink = Guid.NewGuid();
                #endregion

                await _context.Users.AddAsync(User);
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }
    }
}
