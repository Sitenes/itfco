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

        public async Task<IActionResult> OnGetAsync(int? id, bool? isSucceed = null)
        {
            if (isSucceed != null)
                ViewData["IsSucceed"] = isSucceed;
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
            if (string.IsNullOrEmpty(Blog.Image))
                Blog.Image = "Default.jpg";

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(IFormFile? ImageFile)
        {
            try
            {
                if (!ModelState.IsValid)
                    return Page();
                var userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
                string newImage = "";
                if (ImageFile != null)
                {
                    string imageDir = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "BlogRoot", "Image");
                    string thumbDir = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "BlogRoot", "ThumbImage");

                    if (!Directory.Exists(imageDir))
                        Directory.CreateDirectory(imageDir);

                    if (!Directory.Exists(thumbDir))
                        Directory.CreateDirectory(thumbDir);

                    if (Blog.Image != "Default.jpg" && !string.IsNullOrEmpty(Blog.Image))
                    {
                        string path = Path.Combine(imageDir, Blog.Image);
                        if (System.IO.File.Exists(path))
                        {
                            System.IO.File.Delete(path);
                        }

                        string thumbPath = Path.Combine(thumbDir, Blog.Image);
                        if (System.IO.File.Exists(thumbPath))
                        {
                            System.IO.File.Delete(thumbPath);
                        }
                    }
                    string newAvatarURL = Guid.NewGuid().ToString() + Path.GetExtension(ImageFile.FileName);
                    string newPath = Path.Combine(imageDir, newAvatarURL);
                    using (var stream = new FileStream(newPath, FileMode.Create))
                    {
                        ImageFile.CopyTo(stream);
                    }
                    newImage = newAvatarURL;
                    ImageConvertor imageResize = new ImageConvertor();
                    string ThumbPath = Path.Combine(thumbDir, newAvatarURL);
                    string ImageCurrentPath = newPath;
                    imageResize.Image_resize(ImageCurrentPath, ThumbPath, 5000);
                }
                if (Blog.Id == 0)
                {
                    Blog.CreateDate = DateTime.Now;
                    Blog.UserCreatorId = userId;
                    Blog.Image = newImage;
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
                    if (ImageFile != null)
                        existingBlog.Image = newImage;

                }

                await _blogService.SaveChangesAsync();
            }
            catch (Exception)
            {
                return await OnGetAsync(Blog.Id, false);
            }

            return await OnGetAsync(Blog.Id, true);
        }
    }
}
