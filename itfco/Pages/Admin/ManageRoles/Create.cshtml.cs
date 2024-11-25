using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using itfco.Service.Security;
using itfco.Service.Services;
using itfco.DataLayer.Context;
using itfco.Entity.Entities.Permissions.User;

namespace itfco.Web.Pages.Admin.ManageRoles
{
    [PermissionChecker(Entity.Enum.PermissionEnum.AddRole)]
    public class CreateModel : PageModel
    {
        private readonly IPermissionService _permissionService;
        public CreateModel(IPermissionService permissionService)
        {
            _permissionService = permissionService;
        }

        public IActionResult OnGet()
        {
            ViewData["Permissions"] = _permissionService.GetPermissions();
            return Page();
        }

        [BindProperty]
        public Role Role { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPost(List<int> SelectedPermissions)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            
            var roleId = await _permissionService.AddRoleAsync(Role);

            SelectedPermissions.ForEach(async per => await _permissionService.AddPermissionToRoleAsync(per,roleId));
            
            await _permissionService.SaveChangesAsync();

            return RedirectToPage("./Index",true);
        }
    }
}
