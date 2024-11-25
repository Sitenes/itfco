using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using itfco.Service.Services;
using itfco.Entity.Enum;
using itfco.Entity.Entities.Permissions;
using Microsoft.AspNetCore.Authorization;

namespace itfco.Service.Security
{

	public class PermissionCheckerAttribute : AuthorizeAttribute, IAuthorizationFilter
	{
		private PermissionEnum _permission;
		public PermissionCheckerAttribute(PermissionEnum permssion)
		{
            _permission = permssion;
		}
		public void OnAuthorization(AuthorizationFilterContext context)
		{
			if (!context.HttpContext.User.Identity.IsAuthenticated)
			{
				context.Result = new RedirectResult("/Login",false);
				return;
			}
			IPermissionService _permissionService = context.HttpContext.RequestServices.GetService(typeof(IPermissionService)) as PermissionService;
			var userId = Guid.Parse(context.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "");

            if (!_permissionService.CheckPermission(_permission, userId).Result)
			{
                context.Result = new RedirectResult("/Login", false);
                return;
            }
			//context.Result = new OkResult();
		}
	}
}
