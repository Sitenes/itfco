using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Toplearn.Core.DTOs.UserVM;
using Toplearn.Core.Security;
using Toplearn.Core.Services;
using Toplearn.DataLayer.Entities.User;

namespace Toplearn.Web.Pages.Admin.ManageUsers
{
    [PermissionChecker(Core.Enum.Permission.UserManagement)]
    public class DetailsModel : PageModel
    {
        private readonly IUserService _userService;
        private readonly IPermissionService _permissionService;
        public DetailsModel(IUserService userService, IPermissionService permissionService)
        {
            _userService = userService;
            _permissionService = permissionService;
        }


        [BindProperty]
        public UserDetailViewModel UserDetails { get; set; }
        public void OnGet(Guid userId)
        {
            if (userId == Guid.Empty)
                return ;
            UserDetails = _userService.GetUserDetails(userId);
            
        }
    }
}
