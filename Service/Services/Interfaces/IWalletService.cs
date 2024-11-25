using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using itfco.ViewModel.DTOs.UserVM;
using itfco.ViewModel.DTOs.WalletVM;
using itfco.Entity.Entities.Permissions.Wallet;

namespace itfco.Service.Services.Interfaces
{
    public interface IWalletService
    {
        List<WalletShowViewModel> GetAllPaiedWallet(Guid UserId);
        int ChargeWallet(long Price, string UserName);
        bool TikWalletPaied(int WalletId , string UserName);
        Wallet GetWallet(int WalletId);
    }
}
