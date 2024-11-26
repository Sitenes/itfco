using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Toplearn.Core.DTOs.UserVM;
using Toplearn.Core.DTOs.WalletVM;
using Toplearn.DataLayer.Entities.Wallet;

namespace Toplearn.Core.Services.Interfaces
{
    public interface IWalletService
    {
        List<WalletShowViewModel> GetAllPaiedWallet(Guid UserId);
        int ChargeWallet(long Price, string UserName);
        bool TikWalletPaied(int WalletId , string UserName);
        Wallet GetWallet(int WalletId);
    }
}
