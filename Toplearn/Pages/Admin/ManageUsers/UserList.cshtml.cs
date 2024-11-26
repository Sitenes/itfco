using System;
using System.Collections.Generic;
using System.Net.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Toplearn.Core.DTOs.UserVM;
using Toplearn.Core.Security;
using Toplearn.Core.Services;

namespace Toplearn.Web.Pages.Admin.ManageUsers
{
    [PermissionChecker(Core.Enum.Permission.UserManagement)]
    public class List : PageModel
    {
        private readonly IUserService _userService;

        public List(IUserService userService)
        {
            _userService = userService;

        }


        public UsersForAdminViewModel Filters { get; set; }
        public void OnGet(UsersForAdminViewModel filters,bool IsSucceed)
        {
            if(IsSucceed)
                ViewData["IsSucceed"] = true;

            if (filters.PagesCount == 0)
                filters.PagesCount = 1;
            if (filters.CurrentPage == 0)
                filters.CurrentPage = 1;
            if(filters.UserListCount == 0)
                filters.UserListCount = 10;   
            Filters = _userService.FilterUser(filters);

        }
        public void OnPost(UsersForAdminViewModel filters)
        {
            
            if (filters.PagesCount == 0)
                filters.PagesCount = 1;
            if (filters.CurrentPage== 0)
                filters.CurrentPage = 1;
            Filters = _userService.FilterUser(filters);

        }
    }
}
