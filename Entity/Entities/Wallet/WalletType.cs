using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace itfco.Entity.Entities.Permissions.Wallet
{
    public class WalletType
    {
        public WalletType()
        {

        }

        [Key,DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int TypeId { get; set; }
        [Required]
        [MaxLength(100)]
        public string Title { get; set; }


        #region Relations
        public virtual List<Wallet> Wallets { get; set; }
        #endregion
    }
}
