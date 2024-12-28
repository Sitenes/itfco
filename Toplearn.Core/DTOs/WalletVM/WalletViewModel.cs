using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Toplearn.Core.DTOs.UserVM;

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
    public class CartListAdminViewModel
    {
        public string UserName { get; set; }
        public int Count { get; set; }
        public long TotalPrice { get; set; }
        public int Id { get; set; }
    }
    public class CartFilterViewModel
    {
        public string FilterEmail { get; set; }
        public string FilterNameId { get; set; }
        public long? FilterPhone { get; set; }
        public bool? OnlyActivate { get; set; }
        public int PagesCount { get; set; }
        public int CurrentPage { get; set; }
        public int UserListCount { get; set; }
        public int NumAllUser { get; set; }
        public long? PriceFrom { get; set; }
        public long? PriceTo { get; set; }
        public int ItemPerPage { get; set; }
        public int CoursesCount { get; set; }
        public Guid? UserId { get; set; }
        public bool? IsPaid { get; set; }
    }
}
