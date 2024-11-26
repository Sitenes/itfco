using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Toplearn.Core.DTOs.UserVM;
using Toplearn.Core.DTOs.WalletVM;
using Toplearn.Core.Services.Interfaces;
using Toplearn.DataLayer.Context;
using Toplearn.DataLayer.Entities.User;
using Toplearn.DataLayer.Entities.Wallet;

namespace Toplearn.Core.Services
{
    public class WalletService : IWalletService
    {
        private readonly ToplearnContext _context;
        public WalletService(ToplearnContext context)
        {
            _context = context;
        }

        public List<WalletShowViewModel> GetAllPaiedWallet(Guid UserId)
        {
            List<WalletShowViewModel> wallets = _context.Wallets.
                Where(w => w.UserId == UserId && w.IsPay == true).
                Select(w => new WalletShowViewModel()
                {
                    Amount = w.Amount,
                    Description = w.Description,
                    PayDate = w.PayDate,
                    TypeId = w.TypeId,
                }).ToList();
            return wallets;
        }

        public int ChargeWallet(long Price, string UserName)
        {
            try
            {
                var wallet = new Wallet()
                {
                    Amount = Price,
                    Description = "شارژ کیف پول حساب کاربری",
                    IsPay = false,
                    PayDate = DateTime.Now,
                    TypeId = 1,
                    UserId = _context.Users.Where(n => n.UserName == UserName).Select(n => n.UserId).SingleOrDefault(),

                };
                _context.Wallets.Add(wallet);
                _context.SaveChanges();
                return wallet.WalletId;
            }
            catch (Exception)
            {

                return 0;
            }
        }

        public bool TikWalletPaied(int WalletId , string UserName)
        {
            try
            {
                Wallet wallet = _context.Wallets.SingleOrDefault(n => n.IsPay == false && n.WalletId == WalletId);
                wallet.IsPay = true;
                User user = _context.Users.SingleOrDefault(n => n.UserName == UserName);
                user.Wallet += wallet.Amount;
                //_context.Wallets.Update(wallet);
                _context.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }

        public Wallet GetWallet(int WalletId)
        {
            Wallet wallet = _context.Wallets.SingleOrDefault(n => n.WalletId == WalletId);
            return wallet;
        }
    }
}
