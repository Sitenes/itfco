using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Toplearn.Core.DTOs.UserVM
{
    public class UserPanelViewModel
    {

        [Required(ErrorMessage = "لطفا {0} را وارد نمائید")]
        [Display(Name = "کیف پول")]
        public long Wallet { get; set; }

        [DataType(DataType.ImageUrl)]
        [Display(Name = "آواتار")]
        [MaxLength(40, ErrorMessage = "{0} نمی تواند از {1} کاراکتر بیشتر باشد")]
        [Column(TypeName = "varchar")]
        public string UserAvatar { get; set; }

        [MaxLength(100, ErrorMessage = "{0} نمی تواند از {1} کاراکتر بیشتر باشد")]
        [Display(Name = "نام کاربری")]
        [Required(ErrorMessage = "لطفا {0} را وارد نمائید")]

        public string UserName { get; set; }

        [MaxLength(100, ErrorMessage = "{0} نمی تواند از {1} کاراکتر بیشتر باشد")]
        [Display(Name = "ایمیل")]
        [EmailAddress(ErrorMessage = "{0} وارد شده معتبر نمی باشد")]
        [Column(TypeName = "varchar")]
        public string Email { get; set; }

        [Display(Name = "تاریخ ثبت نام")]
        [DataType(DataType.DateTime)]
        [Required(ErrorMessage = "لطفا {0} را وارد نمائید")]
        public DateTime RegisterDate { get; set; }

        [Display(Name = "شماره موبایل")]
        public long Phone { get; set; }

    }

    public class SideBarAvatarViewModel
    {
        public string UserName { get; set; }
        public string UserAvatar { get; set; }
    }

    public class EditProfileViewModel
    {
        [MaxLength(100, ErrorMessage = "{0} نمی تواند از {1} کاراکتر بیشتر باشد")]
        [Display(Name = "نام کاربری")]
        [Required(ErrorMessage = "لطفا {0} را وارد نمائید")]

        public string UserName { get; set; }

        [MaxLength(100, ErrorMessage = "{0} نمی تواند از {1} کاراکتر بیشتر باشد")]
        [Display(Name = "ایمیل")]
        [EmailAddress(ErrorMessage = "{0} وارد شده معتبر نمی باشد")]
        [Required(ErrorMessage = "لطفا {0} را وارد نمائید")]
        public string Email { get; set; }

        [Display(Name = "شماره موبایل")]
        public long Phone { get; set; }

        [DataType(DataType.ImageUrl)]
        [Display(Name = "آواتار")]
        [MaxLength(40, ErrorMessage = "{0} نمی تواند از {1} کاراکتر بیشتر باشد")]
        public string UserAvatar { get; set; }

        [Display(Name = "آواتار")]
        public IFormFile NewAvatar { get; set; }
    }

    public class ChangePasswordViewModel
    {
        public Guid UserId { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "تکرار رمز عبور")]
        [Required(ErrorMessage = "لطفا {0} را وارد نمائید")]
        public string OldPassword { get; set; }


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