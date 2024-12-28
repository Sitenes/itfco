using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Toplearn.Core.AllEnums;
using Toplearn.Core.DTOs.TeacherVM;
using Toplearn.Core.DTOs.WalletVM;
using Toplearn.Core.Services.Interfaces;
using Toplearn.DataLayer.Context;
using Toplearn.DataLayer.Entities.Blogs;
using Toplearn.DataLayer.Entities.Courses;

namespace Toplearn.Core.Services
{
	public class ServiceBlog : IBlogService
	{
		private readonly DbContext _context;

		public ServiceBlog(DbContext context)
		{
			_context = context;
		}

		public async Task<Blog> GetByIdAsync(int id)
		{
			return await _context.Set<Blog>().FindAsync(id);
		}

		public async Task<IEnumerable<Blog>> GetAllAsync()
		{
			return await _context.Set<Blog>().ToListAsync();
		}

		public async Task AddAsync(Blog blog)
		{
			await _context.Set<Blog>().AddAsync(blog);
			await _context.SaveChangesAsync();
		}

		public async Task UpdateAsync(Blog blog)
		{
			_context.Set<Blog>().Update(blog);
			await _context.SaveChangesAsync();
		}

		public async Task DeleteAsync(int id)
		{
			var blog = await GetByIdAsync(id);
			if (blog != null)
			{
				_context.Set<Blog>().Remove(blog);
				await _context.SaveChangesAsync();
			}
		}

		public async Task<IEnumerable<Blog>> SearchAsync(BlogFilter filter)
		{
			IQueryable<Blog> query = _context.Set<Blog>();

			if (!string.IsNullOrEmpty(filter.Title))
			{
				query = query.Where(b => b.Title.Contains(filter.Title));
			}

			if (filter.StartDate.HasValue)
			{
				query = query.Where(b => b.CreateDate >= filter.StartDate.Value);
			}

			if (filter.EndDate.HasValue)
			{
				query = query.Where(b => b.CreateDate <= filter.EndDate.Value);
			}

			if (!string.IsNullOrEmpty(filter.Tags))
			{
				query = query.Where(b => b.Tags.Contains(filter.Tags));
			}

			if (filter.UserCreatorId.HasValue)
			{
				query = query.Where(b => b.UserCreatorId == filter.UserCreatorId.Value);
			}

			return await query.ToListAsync();
		}
	}

}
