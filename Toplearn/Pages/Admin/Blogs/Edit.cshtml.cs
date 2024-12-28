using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Toplearn.Core.Services.Interfaces;
using Toplearn.DataLayer.Entities.Blogs;

namespace Toplearn.Web.Pages.Admin.Blogs
{
    public class EditModel : PageModel
    {
        private readonly IBlogService _blogService;

        public EditModel(IBlogService blogService)
        {
            _blogService = blogService;
        }

        [BindProperty]
        public Blog Blog { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id.HasValue)
            {
                Blog = await _blogService.GetByIdAsync(id.Value);
                if (Blog == null)
                    return NotFound();
            }
            else
            {
                Blog = new Blog();
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(IFormFile? file)
        {
            if (!ModelState.IsValid)
                return Page();

            if (Blog.Id == 0)
            {
                Blog.CreateDate = DateTime.Now;
                await _blogService.AddAsync(Blog);
            }
            else
            {
                var existingBlog = await _blogService.GetByIdAsync(Blog.Id);
                if (existingBlog == null)
                    return NotFound();

                existingBlog.Title = Blog.Title;
                existingBlog.ShortDescription = Blog.ShortDescription;
                existingBlog.Tags = Blog.Tags;
                existingBlog.Content = Blog.Content;

                if (file != null)
                {

                    string imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "BlogImages", existingBlog.Image);
                    if (System.IO.File.Exists(imagePath))
                    {
                        System.IO.File.Delete(imagePath);
                    }

                    string newImageName = Guid.NewGuid() + Path.GetExtension(Blog.Image.FileName);
                    string newImagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "BlogImages", newImageName);
                    using (var stream = new FileStream(newImagePath, FileMode.Create))
                    {
                        await Blog.Image.CopyToAsync(stream);
                    }

                    existingBlog.Image = newImageName;
                }
            }

            await _blogService.SaveChangesAsync();
            return RedirectToPage("Index");
        }
    }
}
