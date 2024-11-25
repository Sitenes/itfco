using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using itfco.ViewModel.DTOs.UserVM;
using itfco.Service.Security;
using itfco.Service.Services;
using itfco.Entity.Entities.Permissions.User;

namespace itfco.Web.Pages.Admin.ManageUsers
{
    [PermissionChecker(Entity.Enum.PermissionEnum.UserManagement)]
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
