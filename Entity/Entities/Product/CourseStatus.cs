using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace itfco.Entity.Entities.Permissions.Products
{
    public class ProductStatus
    {
        [Key]
        public int StatusId { get; set; }
        
        [Required(ErrorMessage ="لطفا {0} را وارد نمائید")][MaxLength(200,ErrorMessage = "{0} نمی تواند از {1} کاراکتر بیشتر باشد")]
        public string StatusTitle { get; set; }


        #region Relations
        /// <summary>
        /// Products That Has This ProductStatus
        /// </summary>
        public List<Product> Products { get; set; }

        #endregion
    }
}
