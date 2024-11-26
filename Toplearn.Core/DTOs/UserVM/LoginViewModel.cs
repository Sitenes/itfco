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
	public class LoginViewModel
	{
        [Required(ErrorMessage ="لطفا {0} را وارد نمائید")]
        [MaxLength(100, ErrorMessage = "{0} نمی تواند از {1} کاراکتر بیشتر باشد")]
        [Display(Name = "ایمیل")]
        [EmailAddress(ErrorMessage = "{0} وارد شده معتبر نمی باشد")]
        [Column(TypeName = "varchar")]
        public string Email { get; set; }


        [Display(Name = "شماره موبایل")]
        public long Phone { get; set; }

        [Display(Name = "رمز عبور")]
        [Required(ErrorMessage = "لطفا {0} را وارد نمائید")]

        public string Password { get; set; }

        [Display(Name = "مرا به خاطر بسپار")]
        public bool RememberMe { get; set; }

        public string ReturnUrl { get; set; }
    }

    public class ResetPasswordViewmodel
    {
        public Guid UserId { get; set; }

        [DataType(DataType.Password)]
        [MaxLength(20, ErrorMessage = "{0} نمی تواند از {1} کاراکتر بیشتر باشد")]
        [MinLength(6, ErrorMessage = "{0} نمی تواند از {1} کاراکتر کمتر باشد")]
        [Display(Name = "رمز عبور")]
        [Required(ErrorMessage = "لطفا {0} را وارد نمائید")]
        [RegularExpression(@"^(?=.*[A-Za-z])(?=.*\d)[A-Za-z\d]{1,20}$", ErrorMessage = "کلمه عبور باید شامل حرف و عدد باشد")]
        public string Password { get; set; }

        [Compare("Password", ErrorMessage = "کلمه های عبور مغایرت دارند")]
        [DataType(DataType.Password)]
        [Display(Name = "تکرار رمز عبور")]
        [Required(ErrorMessage = "لطفا {0} را وارد نمائید")]
        public string RePassword { get; set; }
    }
}
