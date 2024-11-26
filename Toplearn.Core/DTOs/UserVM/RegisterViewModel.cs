using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Xml.Linq;
using Microsoft.AspNetCore.Http;

namespace Toplearn.Core.DTOs.UserVM
{
    public class RegisterViewModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid UserId { get; set; }
        [MaxLength(100, ErrorMessage = "{0} نمی تواند از {1} کاراکتر بیشتر باشد")]
        [Display(Name = "نام کاربری")]
        [Required(ErrorMessage = "لطفا {0} را وارد نمائید")]
        public string UserName { get; set; }


        [MaxLength(100, ErrorMessage = "{0} نمی تواند از {1} کاراکتر بیشتر باشد")]
        [Display(Name = "ایمیل")]
        [EmailAddress(ErrorMessage = "{0} وارد شده معتبر نمی باشد")]
        [Required(ErrorMessage ="لطفا {0} را وارد نمائید")]
        public string Email { get; set; }

        [Required(ErrorMessage = "لطفا {0} را وارد نمائید")]
        //[MaxLength(10, ErrorMessage = "{0} نمی تواند از {1} کاراکتر بیشتر باشد")]
        //[MinLength(9, ErrorMessage = "{0} نمی تواند از {1} کاراکتر کمتر باشد")]
        [Display(Name = "شماره موبایل")]
        public long Phone { get; set; }

        [DataType(DataType.Password)]
        [MaxLength(100, ErrorMessage = "{0} نمی تواند از {1} کاراکتر بیشتر باشد")]
        [Display(Name = "رمز عبور")]
        [Required(ErrorMessage = "لطفا {0} را وارد نمائید")]
        [RegularExpression(@"^(?=.*[A-Za-z])(?=.*\d)[A-Za-z\d]{6,20}$", ErrorMessage = "کلمه عبور باید شامل حرف و عدد باشد")]
        public string Password { get; set; }

        [Compare("Password",ErrorMessage ="کلمه های عبور مغایرت دارند")]
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [Display(Name = "تکرار رمز عبور")]
        public string RePassword { get; set; }
    }
    public class ActivateUserViewModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid UserId { get; set; }
        [MaxLength(100, ErrorMessage = "{0} نمی تواند از {1} کاراکتر بیشتر باشد")]
        [Display(Name = "نام کاربری")]
        [Required(ErrorMessage = "لطفا {0} را وارد نمائید")]

        public string UserName { get; set; }

        [Display(Name = "شماره موبایل")]
        public long Phone { get; set; }


        [Display(Name = "وضعیت")]
        public bool IsActive { get; set; }

        [Display(Name = "کد فعال سازی")]
        [MaxLength(40, ErrorMessage = "{0} نمی تواند از {1} کاراکتر بیشتر باشد")]
        public string ActivateCode { get; set; }

        [Display(Name = "کد فعال سازی")]
        [MaxLength(40, ErrorMessage = "{0} نمی تواند از {1} کاراکتر بیشتر باشد")]
        public string ReceivedActivateCode { get; set; }

        [DataType(DataType.DateTime)]
        [Display(Name = "تاریخ انقضا کد فعال سازی")]
        public DateTime ExpireActivateCodeDate { get; set; }
    }
}
