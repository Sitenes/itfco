using System.Threading.Tasks;
using System;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Toplearn.DataLayer.Context;
using Toplearn.Core.Services;
using Toplearn.DataLayer.Entities.User;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Authorization;

namespace Toplearn.Web.APIs.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public IActionResult GetAllUser()
        {
            var users = _userService.GetUsers();
            Request.HttpContext.Response.Headers["X-Count"] = ((List<User>)users).Count.ToString();
            return Ok(users.FirstOrDefault()?.UserName);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUser([FromRoute] Guid id)
        {
            if (id == Guid.Empty)
                return BadRequest(ModelState);
            return Ok(await _userService.GetUserAsync(id));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser ([FromRoute] Guid id)
        {
            if (await _userService.DeleteUserAsync(id))
            {
                await _userService.SaveChangesAsync();
                Request.HttpContext.Response.StatusCode = StatusCodes.Status200OK;
                return Ok(true);
            }
            else
                return BadRequest(ModelState);
        }

        [HttpPost]
        public async Task<IActionResult> PostUser([FromBody] User user)
        {
            if (await _userService.AddUserAsync(user))
            {
                await _userService.SaveChangesAsync();
                Request.HttpContext.Response.Headers["UserId"] = user.UserId.ToString();
                return CreatedAtAction("GetAllUser",user.UserId,user);
            }
            else
            {
                HttpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
                return BadRequest(ModelState);
            }
        }
        //[HttpPost]
        //public async Task<IActionResult> PostImages([FromForm] IFormFile file)
        //{
        //    return Ok();
        //}
    }
}