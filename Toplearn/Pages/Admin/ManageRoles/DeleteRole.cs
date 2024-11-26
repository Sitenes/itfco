using Microsoft.AspNetCore.Mvc;
using System;
using Toplearn.Core.Security;
using Toplearn.Core.Services;

namespace Toplearn.Web.Pages.Admin.ManageRoles
{
    [PermissionChecker(Core.Enum.Permission.RemoveRole)]
    public class DeleteRole : Controller
    {
        private readonly IPermissionService _permissionService;
        public DeleteRole(IPermissionService permissionService)
        {
            _permissionService = permissionService;
        }


        #region Delete User
        [Route("Admin/ManageRoles/Delete")]
        public IActionResult Delete(int roleId)
        {
            if (roleId == 0)
                return Redirect("/Admin/ManageRoles/Index");

            if (_permissionService.RemoveRole(roleId))
            {
                _permissionService.ResetPermissionsOfRole(roleId);
                _permissionService.SaveChangesAsync();
                return Redirect("/Admin/ManageRoles/Index?IsSucceed=true");
            }
            return Redirect("/Admin/ManageRoles/Index");
        }
        #endregion


    }
}
