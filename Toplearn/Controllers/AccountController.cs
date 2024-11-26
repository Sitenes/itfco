using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Toplearn.Core.Convertors;
using Toplearn.Core.DTOs.UserVM;
using Toplearn.Core.Security;
using Toplearn.Core.Services;
using Toplearn.DataLayer.Context;
using Toplearn.DataLayer.Entities.User;
using TopLearn.Core.Security;
using Toplearn.Core.Senders;
using System.Net.Mail;
using System.Net.Mime;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.IdentityModel.Tokens;
using System.Text.Unicode;
using System.Text;
using System.IdentityModel.Tokens.Jwt;

namespace Toplearn.Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUserService _user;
        private readonly IViewRenderService _renderService;

        public AccountController(IUserService user, IViewRenderService renderService)
        {
            _user = user;
            _renderService = renderService;
        }


        #region Register
        [Route("/Register")]
        public IActionResult Register() => View();

        // GET: Account/Create
        [Route("/Register")]
        [HttpPost]
        public IActionResult Register(RegisterViewModel account)
        {
            if (!ModelState.IsValid)
            {
                return View(account);
            }
            if (account == null)
                return NotFound();
            if (_user.IsExistUserName(account.UserName))
            {
                ModelState.AddModelError("UserName", "نام کاربری تکراری می باشد لطفا نام کاربری دیگری انتخاب نمائید");
                return View(account);
            }
            if (!string.IsNullOrWhiteSpace(account.Email) && _user.IsExistEmail(FixText.FixEmail(account.UserName)))
            {
                ModelState.AddModelError("Email", "این ایمیل قبلا ثبت شده است لطفا ایمیل دیگری انتخاب نمائید");
                return View(account);
            }
            if (_user.IsExistPhone(account.Phone))
            {
                ModelState.AddModelError("Phone", "این شماره موبایل قبلا ثبت شده است لطفا شماره موبایل دیگری انتخاب نمائید");
                return View(account);
            }
            var NewUser = new User
            {
                UserId = Guid.NewGuid(),
                UserName = account.UserName,
                Email = FixText.FixEmail(account.Email),
                Phone = account.Phone,
                Password = PasswordHelper.EncodePasswordMd5(account.Password),
                EmailLink = Guid.NewGuid()
            };
            bool result = _user.AddUser(NewUser);
            if (result == false)
            {
                return NotFound();
            }

            _user.SaveChanges();
            return RedirectToAction("EmailRegister", new { userId = NewUser.UserId });
        }
        #region Activate User
        public IActionResult EmailRegister(Guid userId)
        {
            if (userId == Guid.Empty)
                return NotFound();
            var user = _user.GetUser(userId);
            if (user == null)
                return RedirectToAction("EmailRegister");
            if (user.IsActive)
            {
                return RedirectToAction("Index", "Home");
            }
            user.ExpireEmailLink = DateTime.Now.AddDays(1);
            user.EmailLink = Guid.NewGuid();
            var accountEmail = new ActivateEmailViewModel()
            {
                EmailLink = user.EmailLink,
                Email = user.Email,
                IsActive = user.IsActive,
                UserName = user.UserName,
                UserId = user.UserId,
                ExpireActivateCodeDate = user.ExpireEmailLink
            };

            string EmailBodyRender = _renderService.RenderToStringAsync("ConfirmEmailBody", new EmailtxtViewModel()
            {
                UserId = user.UserId,
                Email = user.Email,
                EmailLink = user.EmailLink,
                UserName = user.UserName
            });
            SendEmail.Send(user.Email, "فعالسازی حساب کاربری تاپ لرن", EmailBodyRender);

            _user.UpdateUser(user);
            _user.SaveChanges();
            return View(accountEmail);
        }
        [Route("/VerifyLink/{userId}/{link}")]
        public IActionResult VerifyLinkEmail(Guid? userId, Guid? link)
        {
            if (link == null || userId == null)
                return NotFound();
            User currentUser = _user.GetUser((Guid)userId);
            if (currentUser == null || currentUser.IsActive == true)
                return NotFound();
            if (DateTime.Compare(currentUser.ExpireEmailLink, DateTime.Now) > 0)
            {
                View();
            }
            if (_user.IsEmailLinkValid((Guid)link) && link == currentUser.EmailLink)
            {
                currentUser.IsActive = true;
                currentUser.EmailLink = Guid.NewGuid();
                _user.UpdateUser(currentUser);
                _user.SaveChanges();
            }
            else
            {
                return NotFound();
            }

            return View(new EmailViewModel
            {
                Email = currentUser.Email,
                IsActive = currentUser.IsActive,
                UserName = currentUser.UserName
            });
        }


        public IActionResult AfterRegister(RegisterViewModel user) => View("PhoneRegister", new ActivateUserViewModel
        {
            ActivateCode = CodeGenerator.ActivatePhone(),
            ExpireActivateCodeDate = DateTime.Now.AddMinutes(5),
            UserName = user.UserName,
            UserId = user.UserId,
            IsActive = false
        });
        public IActionResult ActivateCodeRegister(ActivateUserViewModel user)
        {

            if (!ModelState.IsValid)
            {
                return View(user);
            }
            if (DateTime.Compare(user.ExpireActivateCodeDate, DateTime.Now) > 0)
            {
                ModelState.AddModelError("ReceivedActivateCode", "کد منقضی شده است لطفا ارسال مجدد را بزنید");
                return View(user);
            }
            var currentUser = _user.GetUser(user.UserId);
            currentUser.IsActive = true;
            currentUser.ExpireEmailLink = DateTime.MinValue;
            _user.UpdateUser(currentUser);
            _user.SaveChanges();
            ViewData["IsAvtivated"] = true;
            return View();
        }
        #endregion
        #endregion

        #region Login
        [Route("/Login")]
        public IActionResult Login() => View(new LoginViewModel() { ReturnUrl = HttpContext.Request.Query["ReturnUrl"] });

        [HttpPost]
        [Route("/Login")]
        public IActionResult Login(LoginViewModel account)
        {
            if (account == null)
                return NotFound();
            if (!ModelState.IsValid)
            {
                return View(account);
            }
            var user = _user.GetUser(FixText.FixEmail(account.Email));
            string pass = PasswordHelper.EncodePasswordMd5(account.Password);
            if (user == null || pass != user.Password)
            {
                ModelState.AddModelError("Email", "ایمیل یا رمز وارد شده معتبر نمی باشد");
                return View(account);
            }

            if (user.IsActive == false)
            {
                ModelState.AddModelError("Email", "لطفا با وارد شدن به ایمیل خود و کلیک روی لینک ایمیل خود را تایید کنید .");
                return View(account);
            }
            List<Claim> claims = UserAuthentication(new AuthenticationViewModel()
            {
                IsActivated = user.IsActive,
                IsRememberMe = account.RememberMe,
                UserId = user.UserId,
                UserName = user.UserName,
                UserRole = user.UserRole
            });

            if (!string.IsNullOrWhiteSpace(account.ReturnUrl))
                return Redirect(account.ReturnUrl);

            return RedirectToAction("Index", "Home", new { Id = 1 });
        }

        private List<Claim> UserAuthentication(AuthenticationViewModel user)
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
            return claims;
        }



        #endregion

        #region Forgot Password

        [Route("/ForgotPassword")]
        public IActionResult ForgotPassword() => View();

        [HttpPost]
        [Route("/ForgotPassword")]
        public IActionResult ForgotPassword(EmailOnlyViewModel userEmail)
        {
            if (!ModelState.IsValid)
            {
                return View(userEmail);
            }
            User user = _user.GetUser(userEmail.Email);
            if (user == null)
            {
                ModelState.AddModelError("Email", "ایمیل وارد شده در سیستم ثبت نشده است لطفا ابتدا ثبت نام کنید");
                return View(userEmail);
            }
            user.EmailLink = Guid.NewGuid();
            user.ExpireEmailLink = DateTime.Now.AddDays(1);


            string EmailBodyRender = _renderService.RenderToStringAsync("ResetPasswordBody", new EmailtxtViewModel()
            {
                UserId = user.UserId,
                Email = user.Email,
                EmailLink = user.EmailLink,
                UserName = user.UserName
            });
            SendEmail.Send(user.Email, "تغییر رمز عبور تاپ لرن", EmailBodyRender);


            _user.UpdateUser(user);
            _user.SaveChanges();
            ViewData["ResetPasswordEmailSend"] = true;
            return View(userEmail);
        }


        [Route("/ConfirmPassword/{UserId}/{EmailLink}")]
        public IActionResult ForgotPasswordConfirmation(Guid UserId, Guid EmailLink)
        {
            var user = _user.GetUser(UserId);
            if (user == null || user.EmailLink != EmailLink || DateTime.Compare(user.ExpireEmailLink, DateTime.Now) < 0)
                return NotFound();

            return View("ResetPassword", new ResetPasswordViewmodel { UserId = UserId });
        }

        [HttpPost]
        [Route("/ConfirmPassword/{UserId}/ForgotPasswordConfirmation")]
        public IActionResult ForgotPasswordConfirmation(ResetPasswordViewmodel account)
        {
            if (!ModelState.IsValid)
                return View("ResetPassword", account);
            var user = _user.GetUser(account.UserId);
            if (user == null)
                return NotFound();
            user.Password = PasswordHelper.EncodePasswordMd5(account.Password);
            user.EmailLink = Guid.NewGuid();
            _user.UpdateUser(user);
            _user.SaveChanges();
            ViewData["ResetPasswordSucceed"] = true;
            return View("ResetPassword");
        }
        #endregion

        #region Logout
        [Route("/Logout")]
        public IActionResult Logout()
        {
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home", new { Id = 2 });
        }
        #endregion

    }
}


