using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Toplearn.Core.DTOs.WalletVM
{
    public class WalletShowViewModel
    {
        public string Description { get; set; }
        [Display(Name = "مبلغ")]
        [Required(ErrorMessage = "لطفا {0} را وارد نمائید")]
        public long Amount { get; set; }
        [Display(Name = "تارخ و ساعت پرداخت")]
        [DataType(DataType.DateTime)]
        public DateTime PayDate { get; set; }

        [Display(Name = "نوع تراکنش")]
        public int TypeId { get; set; }

    }

    public class WalletViewModel
    {
        public List<WalletShowViewModel> Wallets { get; set; }

        [Display(Name = "مبلغ")]
        [Required(ErrorMessage = "لطفا {0} را وارد نمائید")]
        [DataType("long", ErrorMessage = "لطفا مبلغ را به درستی وارد نمایید")]
        public long ChargeAmount { get; set; }

    }
}
