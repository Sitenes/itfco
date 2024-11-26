using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Xml.Linq;

namespace Toplearn.Core.DTOs.UserVM
{
    public class EmailViewModel
    {
        [MaxLength(100, ErrorMessage = "{0} نمی تواند از {1} کاراکتر بیشتر باشد")]
        [Display(Name = "نام کاربری")]
        [Required(ErrorMessage = "لطفا {0} را وارد نمائید")]

        public string UserName { get; set; }

        [MaxLength(100, ErrorMessage = "{0} نمی تواند از {1} کاراکتر بیشتر باشد")]
        [Display(Name = "ایمیل")]
        [EmailAddress(ErrorMessage = "{0} وارد شده معتبر نمی باشد")]
        [Column(TypeName = "varchar")]
        public string Email { get; set; }

        [Display(Name = "وضعیت")]
        public bool IsActive { get; set; }
    }

    public class EmailtxtViewModel
    {

        public string UserName { get; set; }

        public string Email { get; set; }

        public Guid UserId { get; set; }
        public Guid EmailLink { get; set; }
    }

    public class ActivateEmailViewModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column(TypeName = "uniqueidentifier")]
        public Guid UserId { get; set; }
        [MaxLength(100, ErrorMessage = "{0} نمی تواند از {1} کاراکتر بیشتر باشد")]
        [Display(Name = "نام کاربری")]
        [Required(ErrorMessage = "لطفا {0} را وارد نمائید")]

        public string UserName { get; set; }

        [MaxLength(100, ErrorMessage = "{0} نمی تواند از {1} کاراکتر بیشتر باشد")]
        [Display(Name = "ایمیل")]
        [EmailAddress(ErrorMessage = "{0} وارد شده معتبر نمی باشد")]
        [Column(TypeName = "varchar")]
        public string Email { get; set; }

        [Display(Name = "وضعیت")]
        public bool IsActive { get; set; }


        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column(TypeName = "uniqueidentifier")]
        [Display(Name = "لینک فعال سازی ایمیل")]
        public Guid EmailLink { get; set; }

        [DataType(DataType.DateTime)]
        [Display(Name = "تاریخ انقضا کد فعال سازی")]
        public DateTime ExpireActivateCodeDate { get; set; }
    }

    public class EmailOnlyViewModel
    {

        [Required(ErrorMessage ="لطفا {0} را وارد نمائید")]
        [MaxLength(100, ErrorMessage = "{0} نمی تواند از {1} کاراکتر بیشتر باشد")]
        [Display(Name = "ایمیل")]
        [EmailAddress(ErrorMessage = "{0} وارد شده معتبر نمی باشد")]
        [DataType(DataType.EmailAddress, ErrorMessage = "{0} وارد شده معتبر نمی باشد")]
        public string Email { get; set; }
    }

}
