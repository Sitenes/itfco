using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Toplearn.DataLayer.Entities.Permissions
{
	public class Permission
	{
		[Key]
		public int PermissionId { get; set; }


		[MaxLength(200,ErrorMessage = "{0} نمی تواند از {1} کاراکتر بیشتر باشد")]
		[Display(Name ="عنوان")]
		[Required(ErrorMessage ="لطفا {0} را وارد نمائید")]
		public string PermissionTitle { get; set; }

		public int? ParentID { get; set; }

		#region Relations
		[ForeignKey("ParentID")]
		public Permission ParentPermission { get; set; }

        public virtual List<RolePermission> RolePermissions { get; set; }
        #endregion
    }
}
