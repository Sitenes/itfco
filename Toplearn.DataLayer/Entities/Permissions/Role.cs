using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Toplearn.DataLayer.Entities.Permissions;

namespace Toplearn.DataLayer.Entities.User
{
    public class Role
    {
        public Role()
        {

        }


        [Key]
        public int RoleId { get; set; }
        [Required(ErrorMessage ="لطفا {0} را وارد نمائید")]
        [Display(Name ="نام نقش")]
        [MaxLength(100,ErrorMessage = "{0} نمی تواند از {1} کاراکتر بیشتر باشد")]
        public string Title { get; set; }
        public bool IsDeleted { get; set; }

        #region Relations
        public virtual List<UserRole> UserRoles { get; set; }
        public virtual List<RolePermission> RolePermissions { get; set; }
        #endregion
    }
}
