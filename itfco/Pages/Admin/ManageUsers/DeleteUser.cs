using Microsoft.AspNetCore.Mvc;
using System;
using itfco.Service.Security;
using itfco.Service.Services;

namespace itfco.Web.Pages.Admin.ManageUsers
{
    [PermissionChecker(Entity.Enum.PermissionEnum.RemoveUser)]
    public class DeleteUser:Controller
	{
        private readonly IUserService _userService;
        public DeleteUser(IUserService userService)
        {
            _userService = userService;
        }


        #region Delete User
        [Route("Admin/ManageUser/Delete")]
        public IActionResult Delete(Guid UserId)
        {
            if(UserId == Guid.Empty)
                return Redirect("/Admin/ManageUser/UserList?currentPage=1");

            if (_userService.DeleteUser(UserId))
            {
                _userService.SaveChanges();
                
            }
            return Redirect("/Admin/ManageUser/UserList?currentPage=1&IsSucceed=true");
        }
        #endregion



    }
}
