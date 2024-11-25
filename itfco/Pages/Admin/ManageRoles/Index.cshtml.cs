using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using itfco.Service.Security;
using itfco.Service.Services;
using itfco.DataLayer.Context;
using itfco.Entity.Entities.Permissions.User;

namespace itfco.Web.Pages.Admin.ManageRoles
{
    [PermissionChecker(Entity.Enum.PermissionEnum.RoleManagement)]
    public class IndexModel : PageModel
    {
        private readonly IPermissionService _permissionService;

        public IndexModel(IPermissionService permissionService)
        {
            _permissionService = permissionService;
        }

        public List<Role> Role { get;set; }

        public void OnGetAsync(bool IsSucceed)
        {
            if (IsSucceed == true)
                ViewData["IsSucceed"] = true;
            Role = _permissionService.GetAllRoles();
        }
    }
}
