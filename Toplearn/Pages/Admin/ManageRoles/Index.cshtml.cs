using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Toplearn.Core.Security;
using Toplearn.Core.Services;
using Toplearn.DataLayer.Context;
using Toplearn.DataLayer.Entities.User;

namespace Toplearn.Web.Pages.Admin.ManageRoles
{
    [PermissionChecker(Core.Enum.Permission.RoleManagement)]
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
