using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Toplearn.DataLayer.Entities.User;

namespace Toplearn.DataLayer.Entities.Permissions
{
	public class RolePermission
	{
		[Key]
		public int RP_Id { get; set; }
		public int RoleId { get; set; }
		public int PermissionId { get; set; }


		#region Relations
		[ForeignKey("PermissionId")]
		public Permission Permission { get; set; }
        
		[ForeignKey("RoleId")]
        public Role Role { get; set; }
		#endregion
	}
}
