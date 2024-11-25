using Microsoft.AspNetCore.Mvc;
using System;
using itfco.Service.Security;
using itfco.Service.Services;

namespace itfco.Web.Pages.Admin.ManageRoles
{
    [PermissionChecker(Entity.Enum.PermissionEnum.RemoveRole)]
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
