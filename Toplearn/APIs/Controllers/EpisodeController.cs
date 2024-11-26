using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Toplearn.Core.Services.Interfaces;
using Toplearn.DataLayer.Context;
using Toplearn.DataLayer.Entities.Courses;

namespace Toplearn.Web.APIs.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class EpisodeController : ControllerBase
    {
        private readonly ICourseService _courseService;

        public EpisodeController(ICourseService courseService)
        {
            _courseService = courseService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Delete([FromRoute]int? id)
        {
            if (id == null)
                return NotFound();
            int Id = (int)id;
            var course = await _courseService.GetEpisode(Id);
            if(!await _courseService.RemoveEpisode(Id))
                return Redirect($"/Admin/Episodes/Index/{id}/false");
            await _courseService.SaveChanges();
            return Redirect($"/Admin/Episodes/Index/{id}/true");
        }

    }
}
