using System;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Toplearn.Core.Services;
using Toplearn.DataLayer.Entities.User;
using Toplearn.Core.DTOs.UserVM;
using System.IO;
using System.Security.Principal;
using Toplearn.Core.Convertors;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using System.Collections.Generic;
using Toplearn.Core.Senders;
using TopLearn.Core.Security;

namespace Toplearn.Web.Areas.UserPanel.Controllers
{
    [Authorize]
    [Area("UserPanel")]
    public class HomeController : Controller
	{
		private readonly IUserService _userService;
        private readonly IViewRenderService _renderService;
        public HomeController(IUserService userService , IViewRenderService renderService)
		{
			_userService = userService;
            _renderService = renderService;
        }


        #region Panel User
        [Route("UserPanel/{UserName?}")]
        public ActionResult Index()
        {
            User user = _userService.GetUser(Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)));
            UserPanelViewModel userPanel = new UserPanelViewModel()
            {
                Email = user.Email,
                Phone = user.Phone,
                RegisterDate = user.RegisterDate,
                UserName = user.UserName,
                Wallet = user.Wallet,
                UserAvatar = user.UserAvatar
            };

            return View(userPanel);
        }

        #endregion
       

        #region Edit Profile
        [Route("UserPanel/Edit")]
        public ActionResult EditProfile()
        {
            User user = _userService.GetUser(Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)));

            var editUser = new EditProfileViewModel()
            {
                Email = user.Email,
                Phone = user.Phone,
                UserName = user.UserName,
                UserAvatar = user.UserAvatar
            };
            return View(editUser);
        }

        // POST: HomeController/Edit/5
        [HttpPost]
        [Route("UserPanel/Edit")]
        public ActionResult EditProfile(EditProfileViewModel newProfile)
        {
            User user = _userService.GetUser(Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)));
            if (!ModelState.IsValid)
                return View(newProfile);
            if (newProfile == null || user == null)
                return NotFound();

            if (_userService.IsExistPhone(newProfile.Phone) && user.Phone != newProfile.Phone)
            {
                ModelState.AddModelError("Phone", "این شماره موبایل قبلا ثبت شده است لطفا شماره موبایل دیگری انتخاب نمائید");
                return View(newProfile);
            }
            try
            {
                user.Phone = newProfile.Phone;
                if (newProfile.NewAvatar != null)
                {
                    if (newProfile.UserAvatar != "Default.png")
                    {
                        string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot","UserAvatar", newProfile.UserAvatar);
                        if (System.IO.File.Exists(path))
                        {
                            System.IO.File.Delete(path);
                        }
                    }
                    string newAvatarURL = Guid.NewGuid().ToString() + Path.GetExtension(newProfile.NewAvatar.FileName);
                    string newPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "UserAvatar", newAvatarURL);
                    using (var stream = new FileStream(newPath, FileMode.Create))
                    {
                        newProfile.NewAvatar.CopyTo(stream);
                    }
                    user.UserAvatar = newAvatarURL;
                }
                if (newProfile.UserName != user.UserName)
                {
                    user.UserName = newProfile.UserName;
                    //Logout User then Login again to save changes in User Claims
                    HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
                    UserAuthentication(
                    new AuthenticationViewModel
                    {
                        IsActivated = true,
                        IsRememberMe = false,
                        UserId = user.UserId,
                        UserName = user.UserName
                    });
                }

                if (newProfile.Email != user.Email)
                {
                    if (string.IsNullOrWhiteSpace(newProfile.Email) || _userService.IsExistEmail(FixText.FixEmail(newProfile.UserName)))
                    {
                        ModelState.AddModelError("Email", "لطفا ایمیل دیگری انتخاب نمائید");
                        return View(newProfile);
                    }
                    user.Email = FixText.FixEmail(newProfile.Email);
                    user.EmailLink = Guid.NewGuid();
                    user.IsActive = false;
                    user.ExpireEmailLink = DateTime.Now.AddDays(1);

                    string EmailBodyRender = _renderService.RenderToStringAsync("ConfirmEmailBody", new EmailtxtViewModel()
                    {
                        UserId = user.UserId,
                        Email = user.Email,
                        EmailLink = user.EmailLink,
                        UserName = user.UserName
                    });
                    SendEmail.Send(user.Email, "فعالسازی حساب کاربری تاپ لرن", EmailBodyRender);

                    ViewData["EmailChanged"] = true;
                    _userService.UpdateUser(user);
                    _userService.SaveChanges();
                    return View(newProfile);
                }
                _userService.UpdateUser(user);
                _userService.SaveChanges();
                ViewData["IsSucceed"] = true;
                return View(newProfile);
            }
            catch
            {
                ModelState.AddModelError("Email", "عملیات ناموفق بود لطفا مجددا تلاش فرمایید");
                return View(newProfile);
            }
        }
        #endregion


        #region Change Password
        [Route("UserPanel/ChangePassword")]
        public ActionResult ChangePassword()
        {
            User user = _userService.GetUser(Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)));

            var userPass = new ChangePasswordViewModel()
            {
                OldPassword = user.Password,
                UserId = user.UserId
            };

            return View(userPass);
        }

        [HttpPost]
        [Route("UserPanel/ChangePassword")]
        public ActionResult ChangePassword(ChangePasswordViewModel userPass)
        {
            if (!ModelState.IsValid)
            {
                return View(userPass);
            }
            User user = _userService.GetUser(userPass.UserId);
            if (user == null)
                return NotFound();

            if (user.Password != PasswordHelper.EncodePasswordMd5(userPass.OldPassword))
            {
                ModelState.AddModelError("OldPassword", "رمز وارد شده صحیح نمی باشد");
                return View(userPass);
            }
            user.Password = PasswordHelper.EncodePasswordMd5(userPass.Password);
            _userService.UpdateUser(user);
            _userService.SaveChanges();
            ViewData["ResetPasswordSucceed"] = true;
            return View(userPass);
        }
        #endregion


        private void UserAuthentication(AuthenticationViewModel user)
        {
            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()),
                new Claim(ClaimTypes.Name, user.UserName),
                (user.UserRole == null)?null:new Claim(ClaimTypes.Role, user.UserRole.ToString()),
                new Claim("IsActivated", user.IsActivated.ToString())
            };
            ClaimsIdentity identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var principle = new ClaimsPrincipal(identity);
            var properties = new AuthenticationProperties()
            {
                IsPersistent = user.IsRememberMe
            };
            HttpContext.SignInAsync(principle, properties);
        }
    }
}
