using Microsoft.AspNetCore.Http;
using System.IO;
using System;
using Microsoft.AspNetCore.Mvc;
using Toplearn.Core.Services.Interfaces;
using Toplearn.Core.DTOs.CourseVM;
using System.Threading.Tasks;
using System.Collections.Generic;
using Toplearn.DataLayer.Entities.Courses;
using Toplearn.Core.DTOs.UserVM;
using Microsoft.AspNetCore.Authorization;
using System.Linq;
using ZarinpalSandbox;

namespace Toplearn.Web.Controllers
{
    public class ProductController : Controller
    {
        private readonly ICourseService _courseService;
        public ProductController(ICourseService courseService)
        {
            _courseService = courseService;
        }
        public async Task<IActionResult> Index(CourseFilterAdminViewModel input)
        {
            var lang = Request.Cookies["Culture"] ?? "fa";
            input.Language = lang;
            var products = await _courseService.GetCoursesList(input);
            var parents = await _courseService.GetParentCourseGroups();

            var categoryList = new List<CategoryListDto>();
            foreach (var parent in parents)
            {
                var subCategories = new List<CategoryListDto>();
                foreach (var child in parent.Childs)
                {
                    var title = child.NamePersian;
                    if (lang.StartsWith("en"))
                    {
                        title = child.NameEnglish;
                    }
                    else if (lang.StartsWith("ar"))
                    {
                        title = child.NameArabic;
                    }

                    var subCategory = new CategoryListDto
                    {
                        Id = child.GroupId,
                        Count = await _courseService.CountCategoryProducts(child.GroupId),
                        Name = title
                    };

                    subCategories.Add(subCategory);
                }

                var parentTitle = parent.NamePersian;
                if (lang.StartsWith("en"))
                {
                    parentTitle = parent.NameEnglish;
                }
                else if (lang.StartsWith("ar"))
                {
                    parentTitle = parent.NameArabic;
                }

                var category = new CategoryListDto
                {
                    Id = parent.Id,
                    Count = await _courseService.CountCategoryProducts(parent.Id),
                    Name = parentTitle,
                    SubCategories = subCategories
                };

                categoryList.Add(category);
            }

            ViewData["Categories"] = categoryList;
            return View(products);
        }

        [Route("/product/{id}")]
        public async Task<IActionResult> Index(int id)
        {
            var product = await _courseService.GetCourse(id);
            
            var lang = Request.Cookies["Culture"] ?? "fa";
            var parents = (await _courseService.GetParentCourseGroups());

            var categoryList = new List<CategoryListDto>();
            foreach (var parent in parents)
            {
                var subCategories = new List<CategoryListDto>();
                foreach (var child in parent.Childs)
                {
                    var subCategory = new CategoryListDto
                    {
                        Id = child.GroupId,
                        Count = await _courseService.CountCategoryProducts(child.GroupId),
                        Name = lang == "en" ? child.NameEnglish : lang == "ar" ? child.NameArabic : child.NamePersian,
                    };

                    subCategories.Add(subCategory);
                }

                var category = new CategoryListDto
                {
                    Id = parent.Id,
                    Count = await _courseService.CountCategoryProducts(parent.Id),
                    Name = lang == "en" ? parent.NameEnglish : lang == "ar" ? parent.NameArabic : parent.NamePersian,
                    SubCategories = subCategories
                };
                categoryList.Add(category);
            }
            ViewData["Categories"] = categoryList;
            return View("product", product);
        }
    }
}
