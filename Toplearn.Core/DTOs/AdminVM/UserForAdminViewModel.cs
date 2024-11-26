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
	public class UserListViewModel
	{
		public Guid UserId { get; set; }
		public string UserName { get; set; }
		public string Email { get; set; }
		public long Phone { get; set; }
		public DateTime Create { get; set; }
		public long Wallet { get; set; }
		public bool IsActive { get; set; }
	}
	
	public class UsersForAdminViewModel
	{
        public List<UserListViewModel> Users { get; set; }
		public string filterEmail { get; set; }
		public string filterNameId { get; set; }
		public long filterPhone { get; set; }
		public bool OnlyActivate { get; set; }
		public int PagesCount { get; set; }
		public int CurrentPage { get; set; }
		public int UserListCount { get; set; }
		public int NumAllUser { get; set; }
	}
	public class AddUserAdminViewModel
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

		[Required(ErrorMessage ="لطفا {0} را وارد نمائید")]
        [Display(Name = "شماره موبایل")]
        public long Phone { get; set; }


        [DataType(DataType.Password)]
        [MaxLength(100, ErrorMessage = "{0} نمی تواند از {1} کاراکتر بیشتر باشد")]
        [Display(Name = "رمز عبور")]
        [Required(ErrorMessage = "لطفا {0} را وارد نمائید")]
         public string Password { get; set; }

        [Display(Name = "آواتار")]
        public IFormFile UserAvatar { get; set; }


        [Required(ErrorMessage = "لطفا {0} را وارد نمائید")]
        [Display(Name = "کیف پول")]
        public long Wallet { get; set; }

    }

	public class GetUserForEditViewModel
	{
        [Key]
        public Guid UserId { get; set; }
        
        [MaxLength(100, ErrorMessage = "{0} نمی تواند از {1} کاراکتر بیشتر باشد")]
        [Display(Name = "نام کاربری")]
        [Required(ErrorMessage = "لطفا {0} را وارد نمائید")]

        public string UserName { get; set; }

        [MaxLength(100, ErrorMessage = "{0} نمی تواند از {1} کاراکتر بیشتر باشد")]
        [Display(Name = "ایمیل")]
        [EmailAddress(ErrorMessage = "{0} وارد شده معتبر نمی باشد")]
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
        [RegularExpression(@"^(?=.*[A-Za-z])(?=.*\d)[A-Za-z\d]{6,20}$", ErrorMessage = "کلمه عبور باید شامل حرف و عدد باشد")]
        public string Password { get; set; }

        [Display(Name = "وضعیت")]
        public bool IsActive { get; set; }

        public string OldAvatar { get; set; }

        [Required(ErrorMessage = "لطفا {0} را وارد نمائید")]
        [Display(Name = "کیف پول")]
        public long Wallet { get; set; }

        public IFormFile NewAvatar { get; set; }
        public List<int> RolesId { get; set; }
    }
    public class UserDetailViewModel
    {
        public string UserName { get; set; }
        public long Phone { get; set; }
        public string Email { get; set; }
        public DateTime RegisterDate { get; set; }
        public bool IsActive { get; set; }
        public long Wallet { get; set; }
        public bool IsDeleted { get; set; }
        public string UserAvatar { get; set; }

    }
}
