using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Toplearn.Core.Convertors;
using Toplearn.Core.DTOs.UserVM;
using Toplearn.Core.Security;
using Toplearn.Core.Services;
using Toplearn.DataLayer.Entities.User;
using TopLearn.Core.Security;

namespace Toplearn.Web.Pages.Admin.ManageUsers
{
    [PermissionChecker(Core.Enum.Permission.AddUser)]
    public class AddUserModel : PageModel
    {
        private readonly IUserService _userService;
        private readonly IPermissionService _permissionService;

        [BindProperty]
        public AddUserAdminViewModel addUser { get; set; }
        public AddUserModel(IUserService userService, IPermissionService permissionService)
        {
            _userService = userService;
            _permissionService = permissionService;
        }

        public void OnGet()
        {
            ViewData["Roles"] = _permissionService.GetAllRoles();
            addUser = new AddUserAdminViewModel();
        }

        public IActionResult OnPost(List<int> Roles)
        {
            ViewData["Roles"] = _permissionService.GetAllRoles();
            #region Validation for Add user from Admin
            if (!ModelState.IsValid || addUser == null)
                return Page();
            if (string.IsNullOrWhiteSpace(addUser.UserName))
            {

                ModelState.AddModelError("UserName", "نام کاربری وارد شده معتبر نمی باشد لطفا مجددا تلاش نمایید");
                return Page();
            }
            if (_userService.IsExistUserName(addUser.UserName))
            {
                ModelState.AddModelError("UserName", "نام کاربری وارد شده تکراری است لطفا مجددا تلاش نمایید");
                return Page();
            }
            if (_userService.IsExistEmail(FixText.FixEmail(addUser.Email)))
            {
                ModelState.AddModelError("Email", "ایمیل وارد شده تکراری است لطفا مجددا تلاش نمایید");
            }
            if (addUser.Phone != 0 && _userService.IsExistPhone(addUser.Phone))
            {
                ModelState.AddModelError("UserName", "شماره موبایل وارد شده تکراری است لطفا مجددا تلاش نمایید");
            }


            #endregion

            User NewUser = new User()
            {
                Email = addUser.Email,
                Password = PasswordHelper.EncodePasswordMd5(addUser.Password),
                IsActive = true,
                Phone = addUser.Phone,
                UserName = addUser.UserName,
                UserId = System.Guid.NewGuid(),
                Wallet = addUser.Wallet,
                RegisterDate = System.DateTime.Now,
            };

            #region Roles
            Roles.ForEach(n => _permissionService.AddRole(NewUser.UserId, n));
            #endregion

            #region Avatar
            if(addUser.UserAvatar == null)
            {
                NewUser.UserAvatar = "Default.png";
            }
            else
            {
                string newAvatarURL = Guid.NewGuid().ToString() + Path.GetExtension(addUser.UserAvatar.FileName);
                string newPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "UserAvatar", newAvatarURL);
                using (var stream = new FileStream(newPath, FileMode.Create))
                {
                    addUser.UserAvatar.CopyTo(stream);
                }
                NewUser.UserAvatar = newAvatarURL;
            }

            #endregion

            _userService.AddUser(NewUser);
            _userService.SaveChanges();

            ViewData["IsSucceed"] = true;
            return Page();
        }
    }
}
