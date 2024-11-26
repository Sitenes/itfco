using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Toplearn.DataLayer.Entities;

namespace Toplearn.DataLayer.Entities.Wallet
{
    public class Wallet
    {
        public Wallet()
        {

        }
        [Key]
        public int WalletId { get; set; }
        [Display(Name ="شرح")][MaxLength(500,ErrorMessage = "{0} نمی تواند از {1} کاراکتر بیشتر باشد")]
        public string Description { get; set; }
        [Display(Name ="مبلغ")][Required(ErrorMessage ="لطفا {0} را وارد نمائید")]
        public long Amount { get; set; }
        [Display(Name ="تارخ و ساعت پرداخت")][DataType(DataType.DateTime)]
        public DateTime PayDate { get; set; }

        [Display(Name ="پرداخت شده")]
        public bool IsPay { get; set; }

        #region Relations
        [Display(Name ="کاربر")]
        public Guid UserId { get; set; }
        [ForeignKey("UserId")]
        public virtual User.User User { get; set; }
        [Display(Name ="نوع تراکنش")]
        public int TypeId { get; set; }

        [ForeignKey("TypeId")]
        public virtual WalletType Type { get; set; }
        #endregion
    }
}
