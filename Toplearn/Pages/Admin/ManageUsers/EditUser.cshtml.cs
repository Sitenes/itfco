using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using System.IO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Toplearn.Core.Convertors;
using Toplearn.Core.DTOs.UserVM;
using Toplearn.Core.Senders;
using Toplearn.Core.Services;
using Toplearn.DataLayer.Entities.User;
using TopLearn.Core.Security;
using Toplearn.Core.Security;

namespace Toplearn.Web.Pages.Admin.ManageUsers
{
    [PermissionChecker(Core.Enum.Permission.EditUser)]
    public class EditUserModel : PageModel
    {
        private readonly IUserService _userService;
        private readonly IViewRenderService _renderService;
        private readonly IPermissionService _permissionService;

        public EditUserModel(IUserService userService,IViewRenderService renderService, IPermissionService permissionService)
        {
            _userService = userService;
            _renderService = renderService;
            _permissionService = permissionService;
        }
        [BindProperty]
        public GetUserForEditViewModel EditUser { get; set; }
        public void OnGet(Guid UserId)
        {
            ViewData["Roles"] = _permissionService.GetAllRoles();
            User user = _userService.GetUser(UserId);
            if (user == null)
                return;
            EditUser = new GetUserForEditViewModel();
            EditUser.RolesId = _permissionService.GetUserRoles(UserId);
            EditUser.Email = user.Email;
            EditUser.IsActive = user.IsActive;
            EditUser.Phone = user.Phone;
            EditUser.UserId = UserId;
            EditUser.RegisterDate = user.RegisterDate;
            EditUser.UserName = user.UserName;
            EditUser.OldAvatar = user.UserAvatar;
            EditUser.Wallet = user.Wallet;
        }
        public IActionResult OnPost(List<int> roles)
        {

            EditUser.RolesId = new List<int>();
            ViewData["Roles"] = _permissionService.GetAllRoles();
            if (!ModelState.IsValid)
                return Page();
            User user = _userService.GetUser(EditUser.UserId);
            user.IsActive = EditUser.IsActive;
            user.Phone = EditUser.Phone;
            roles.ForEach(n => _permissionService.AddRole(user.UserId, n));
            user.RegisterDate = EditUser.RegisterDate;
            user.UserName = EditUser.UserName;
            user.Wallet = EditUser.Wallet;
            if(!string.IsNullOrWhiteSpace(EditUser.Password))
                user.Password = PasswordHelper.EncodePasswordMd5(EditUser.Password);

            if (EditUser.NewAvatar != null)
            {
                if (EditUser.OldAvatar != "Default.png")
                {
                    string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot","UserAvatar", EditUser.OldAvatar);
                    if (System.IO.File.Exists(path))
                    {
                        System.IO.File.Delete(path);
                    }
                }
                string newAvatarURL = Guid.NewGuid().ToString() + Path.GetExtension(EditUser.NewAvatar.FileName);
                string newPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "UserAvatar", newAvatarURL);
                using (var stream = new FileStream(newPath, FileMode.Create))
                {
                    EditUser.NewAvatar.CopyTo(stream);
                }
                user.UserAvatar = newAvatarURL;
            }

            if (EditUser.Email != user.Email)
            {
                if (string.IsNullOrWhiteSpace(EditUser.Email) || _userService.IsExistEmail(FixText.FixEmail(EditUser.Email)))
                {
                    ModelState.AddModelError("Email", "لطفا ایمیل دیگری انتخاب نمائید");
                    return Page();
                }
                user.Email = FixText.FixEmail(EditUser.Email);
                if (EditUser.IsActive == false)
                {
                    user.EmailLink = Guid.NewGuid();
                    user.ExpireEmailLink = DateTime.Now.AddDays(1);

                    string EmailBodyRender = _renderService.RenderToStringAsync("ConfirmEmailBody", new EmailtxtViewModel()
                    {
                        UserId = user.UserId,
                        Email = user.Email,
                        EmailLink = user.EmailLink,
                        UserName = user.UserName
                    });
                    SendEmail.Send(EditUser.Email, "فعالسازی حساب کاربری تاپ لرن", EmailBodyRender);
                }
            }
            if (!_userService.UpdateUser(user))
                throw new Exception("یوزر آپدیت نشد");
            _userService.SaveChanges();
            ViewData["IsSucceed"] = true;
            return Page();

        }



    }
}
