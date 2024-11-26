using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Toplearn.DataLayer.Entities.Permissions;
using Toplearn.DataLayer.Entities.User;

namespace Toplearn.Core.Services
{
	public interface IPermissionService
	{

        #region Roles
        Role GetRole(int Id);
        List<int> GetUserRoles(Guid UserId);
        List<Role> GetAllRoles();
        bool AddRole(Guid UserId, int Role);
        Task<int> AddRoleAsync(Role role);
        bool RemoveRole(int roleId);
        bool UpdateRole(Role role);
        #endregion
        Task SaveChangesAsync();

        #region Permissions
        List<Permission> GetPermissions();
        List<int> PermissionsRole(int roleId);
        Task<bool> AddPermissionToRoleAsync(int permission,int role);
        bool ResetPermissionsOfRole(int roleId);
        Task<bool> CheckPermission(Enum.Permission permission, Guid userId);
        #endregion
    }
}
