using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Toplearn.Core.Services.Interfaces;
using Toplearn.DataLayer.Entities.Blogs;
using TopLearn.Core.Convertors;
using Toplearn.DataLayer.Entities.Courses;
using System.Security.Claims;
using Toplearn.Core.Services;
using Toplearn.DataLayer.Entities;

namespace Toplearn.Web.Pages.Admin.Blogs
{
	public class SettingsModel : PageModel
	{
		private readonly IUserService _userService;

		public SettingsModel(IUserService userService)
		{
			_userService = userService;
		}

		[BindProperty]
		public Setting setting { get; set; }

		public async Task<IActionResult> OnGetAsync(bool? isSucceed = null)
		{
			if (isSucceed != null)
				ViewData["IsSucceed"] = isSucceed;
			setting = await _userService.GetSettingAsync();
			if (setting == null)
				return NotFound();

			return Page();
		}

		public async Task<IActionResult> OnPostAsync()
		{
			try
			{
				if (!ModelState.IsValid)
					return Page();
				_userService.UpdateSetting(setting);
				await _userService.SaveChangesAsync();
			}
			catch (Exception)
			{
				return await OnGetAsync(false);
			}

			return await OnGetAsync(true);
		}
	}
}
