using System;
using System.Collections.Generic;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Toplearn.Core.DTOs.UserVM;
using Toplearn.Core.DTOs.WalletVM;
using Toplearn.Core.Services;
using Toplearn.Core.Services.Interfaces;
using Toplearn.DataLayer.Entities.User;
using Toplearn.DataLayer.Entities.Wallet;
using ZarinpalSandbox;

namespace Toplearn.Web.Areas.UserPanel.Controllers
{
    [Authorize]
    [Area("UserPanel")]
    public class WalletController : Controller
    {
        private readonly IWalletService _walletService;
        private readonly IUserService _userService;
        public WalletController(IWalletService walletService, IUserService userService)
        {
            _walletService = walletService;
            _userService = userService;
        }


        //Show All wallets of user and Charge Wallet
        [Route("UserPanel/Wallet")]
        public IActionResult Index()
        {
            WalletViewModel UserWallet = new WalletViewModel()
            {
                Wallets = _walletService.
                GetAllPaiedWallet(Guid.Parse(
                    User.FindFirstValue(ClaimTypes.NameIdentifier))),
                ChargeAmount = 0
            };

            return View("WalletIndex", UserWallet);
        }

        [HttpPost]
        [Route("UserPanel/Wallet")]
        public IActionResult Index(WalletViewModel model)
        {
            if (model.ChargeAmount > 10000)
            {
                int WalletId = _walletService.ChargeWallet(model.ChargeAmount, User.Identity.Name);
                User user = _userService.GetUserByName(User.Identity.Name);

                // zarinpal dargah
                var payment = new Payment(Convert.ToInt32(model.ChargeAmount / 10));
                var result = payment.PaymentRequest($"شارژ کیف پول", $"http://Localhost:52532/payment/{WalletId}", user.Email, '0' + user.Phone.ToString());

                if (result.Result.Status == 100)
                    return Redirect("https://sandbox.zarinpal.com/pg/StartPay/" + result.Result.Authority);

            }

            WalletViewModel UserWallet = new WalletViewModel()
            {
                Wallets = _walletService.
                GetAllPaiedWallet(Guid.Parse(
                User.FindFirstValue(ClaimTypes.NameIdentifier))),
                ChargeAmount = 0
            };
            ModelState.AddModelError("ChargeAmount", "مبلغ وارد شده باید بیشتر از ده هزار ریال باشد");
            return View("WalletIndex", UserWallet);

        }

        [Route("/payment/{WalletId?}")]
        [Authorize]
        public IActionResult Payment(int WalletId)
        {
            if (WalletId == 0)
                return BadRequest();
            if (HttpContext.Request.Query["Status"] != "" &&
                HttpContext.Request.Query["Authority"] != "")
            {
                Wallet wallet = _walletService.GetWallet(WalletId);
                if (wallet == null)
                    return NotFound();
                string Authority = HttpContext.Request.Query["Authority"].ToString();
                var amount = int.Parse(wallet.Amount.ToString());
                var payment = new Payment(amount / 10);
                var result = payment.Verification(Authority);
                ViewBag.PaymentResult = result;
                ViewBag.Amount = amount;
                ViewBag.CartId = WalletId;
                ViewBag.RefId = result.Result.RefId;
                string status = HttpContext.Request.Query["Status"];
                if (
                HttpContext.Request.Query["Status"] == "OK")
                {
                    if (result.Result.Status == 100)
                    {
                        _walletService.TikWalletPaied(WalletId,User.Identity.Name);
                    }
                }

                return View();

            }
            return NotFound();
        }

    }
}
