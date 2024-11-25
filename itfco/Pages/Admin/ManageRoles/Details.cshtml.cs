using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using itfco.Service.Security;
using itfco.DataLayer.Context;
using itfco.Entity.Entities.Permissions.User;

namespace itfco.Web.Pages.Admin.ManageRoles
{
    public class DetailsModel : PageModel
    {
        private readonly Context _context;

        public DetailsModel(Context context)
        {
            _context = context;
        }

        public Role Role { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Role = await _context.Roles.FirstOrDefaultAsync(m => m.RoleId == id);

            if (Role == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
