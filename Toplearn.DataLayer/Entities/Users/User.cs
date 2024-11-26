using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Toplearn.DataLayer.Entities.Courses;

namespace Toplearn.DataLayer.Entities.User
{
    public class User
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

        [Display(Name = "تاریخ ثبت نام")]
        [DataType(DataType.DateTime)]
        [Required(ErrorMessage = "لطفا {0} را وارد نمائید")]
        public DateTime RegisterDate { get; set; }

        [Display(Name = "شماره موبایل")]
        public long Phone { get; set; }


        [DataType(DataType.Password)]
        [MaxLength(100, ErrorMessage = "{0} نمی تواند از {1} کاراکتر بیشتر باشد")]
        [Display(Name = "رمز عبور")]
        [Required(ErrorMessage = "لطفا {0} را وارد نمائید")]
        [RegularExpression(@"^(?=.*[A-Za-z])(?=.*\d)[A-Za-z\d]{6,20}$", ErrorMessage = "کلمه عبور باید شامل حرف و عدد باشد")]
        public string Password { get; set; }

        [Display(Name = "وضعیت")]
        public bool IsActive { get; set; }

        //[Display(Name = "کد فعال سازی")]
        //[MaxLength(6, ErrorMessage = "{0} نمی تواند از {1} کاراکتر بیشتر باشد")]
        //[Column(TypeName = "varchar")]
        //public string ActivateCode { get; set; }

        [DataType(DataType.DateTime)]
        [Display(Name = "تاریخ انقضا کد فعال سازی")]
        public DateTime ExpireEmailLink { get; set; }

        [DataType(DataType.ImageUrl)]
        [Display(Name = "آواتار")]
        [MaxLength(45,ErrorMessage = "{0} نمی تواند از {1} کاراکتر بیشتر باشد")]
        [Column(TypeName = "varchar")]
        public string UserAvatar { get; set; }


        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column(TypeName = "uniqueidentifier")]
        [Display(Name ="لینک فعال سازی ایمیل")]
        public Guid EmailLink { get; set; }

        [Required(ErrorMessage = "لطفا {0} را وارد نمائید")]
        [Display(Name = "کیف پول")]
        public long Wallet { get; set; }
        public bool IsDeleted { get; set; }

        [Display(Name ="استاد است ؟")]
        public bool IsTeacher { get; set; }

        #region Relations
        public virtual List<Wallet.Wallet> Wallets { get; set; }
        public virtual List<UserRole> UserRole { get; set; }
        public List<Course> Courses { get; set; }
        #endregion
    }
}