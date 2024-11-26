using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Toplearn.Core.Security;
using Toplearn.Core.Services;
using Toplearn.DataLayer.Context;
using Toplearn.DataLayer.Entities.User;

namespace Toplearn.Web.Pages.Admin.ManageRoles
{
    [PermissionChecker(Core.Enum.Permission.EditRole)]
    public class EditModel : PageModel
    {
        private readonly IPermissionService _permissionService;

        public EditModel(IPermissionService permissionService)
        {
            _permissionService = permissionService;

        }

        [BindProperty]
        public Role Role { get; set; }

        public IActionResult OnGet(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            ViewData["Permissions"] = _permissionService.GetPermissions();
            ViewData["SelectedPermissions"] = _permissionService.PermissionsRole((int)id);

            Role = _permissionService.GetRole((int)id);

            if (Role == null)
            {
                return NotFound();
            }
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync(List<int> SelectedPermissions)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            _permissionService.UpdateRole(Role);
            _permissionService.ResetPermissionsOfRole(Role.RoleId);
            await _permissionService.SaveChangesAsync();
            SelectedPermissions.ForEach(async n =>await _permissionService.AddPermissionToRoleAsync(n, Role.RoleId));
            await _permissionService.SaveChangesAsync();
            return RedirectToPage("./Index",true);
        }

    }
}
