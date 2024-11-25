using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using itfco.Service.Security;

namespace itfco.Web.Pages.Admin
{
    [PermissionChecker(Entity.Enum.PermissionEnum.Management)]
    public class IndexModel : PageModel
    {
        public void OnGet()
        {
        }
    }
}
