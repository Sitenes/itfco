using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Toplearn.Core.Security;

namespace Toplearn.Web.Pages.Admin
{
    [PermissionChecker(Core.Enum.Permission.Management)]
    public class IndexModel : PageModel
    {
        public void OnGet()
        {
        }
    }
}
