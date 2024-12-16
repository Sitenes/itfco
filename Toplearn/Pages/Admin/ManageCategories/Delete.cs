using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Toplearn.Core.Security;
using Toplearn.Core.Services;
using Toplearn.Core.Services.Interfaces;

namespace Toplearn.Web.Pages.Admin.ManageCategories
{
    [PermissionChecker(Core.AllEnums.PermissionEnum.RemoveCategory)]
    public class DeleteCategory : Controller
    {
        private readonly ICourseService _courseService;
        public DeleteCategory(ICourseService courseService)
        {
            _courseService = courseService;
        }


        #region Delete
        [Route("Admin/ManageCategories/Delete")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            if (id == 0)
                return Redirect("/Admin/ManageCategories/Index");

            await _courseService.DeleteCategory(id);
            await _courseService.SaveChanges();
            return Redirect("/Admin/ManageCategories/Index?IsSucceed=true");
        }
        #endregion


    }
}
