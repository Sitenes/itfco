using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Helpers;
using Grpc.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PagedList;
using Toplearn.Core.DTOs.CourseVM;
using Toplearn.Core.DTOs.TeacherVM;
using Toplearn.DataLayer.Entities.Blogs;
using Toplearn.DataLayer.Entities.Courses;

namespace Toplearn.Core.Services.Interfaces
{
	public interface IBlogService
	{
		Task<Blog> GetByIdAsync(int id);
		Task<IEnumerable<Blog>> GetAllAsync();
		Task AddAsync(Blog blog);
		Task UpdateAsync(Blog blog);
		Task DeleteAsync(int id);
		Task<IEnumerable<Blog>> SearchAsync(BlogFilter filter);
		Task SaveChangesAsync();
	}
}
