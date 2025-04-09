using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Toplearn.DataLayer.Entities.Courses;

namespace Toplearn.Core.DTOs.CourseVM
{

    public class CourseListAdminViewModel
    {
        public int CourseId { get; set; }

        public string CourseTitle { get; set; }

        public string Image { get; set; }//عکس دوره

        public int NumberOfStudents { get; set; }//تعداد دانجو هایی که در این دوره ثبت نام کرده اند

        public double Price { get; set; }//ملغ دوره
        public string TeacherName { get; set; }
    }

    public class CourseItemListViewModel
    {

        public int Id { get; set; }
        public string Title { get; set; }
        public string Image { get; set; }
        public double Price { get; set; }
        public TimeSpan Time { get; set; }
    }


    public class CourseFilterListViewModel
    {
        public string Title { get; set; }
        public AllEnums.Coust Coust { get; set; }
        public long StartPrice { get; set; }
        public long EndPrice { get; set; }
        public AllEnums.OrderBy OrderBy { get; set; }
        public List<int> Groups { get; set; }
    }
    public class CourseFilterAdminViewModel
    {
        public string Language { get; set; } = "fa";
        public string Title { get; set; }
        public string Description { get; set; }//توضیحات کامل دوره        

        public int StudentsCountFrom { get; set; }//تعداد دانجو هایی که در این دوره ثبت نام کرده اند

        public int StudentsCountTo { get; set; }//تعداد دانجو هایی که در این دوره ثبت نام کرده اند

        public long PriceFrom { get; set; }//ملغ دوره

        public long PriceTo { get; set; }//ملغ دوره

        public string Tags { get; set; }

        public int LevelId { get; set; }

        public int CourseStatusId { get; set; }

        public string TeacherOrCourseId { get; set; }

        public int GroupId { get; set; }

        public int ItemPerPage { get; set; }

        public int CoursesCount { get; set; }

        public int CurrentPage { get; set; }
    }
    public class CategoryDto
    {
        public int Id { get; set; }

        public string NamePersian { get; set; }
        public string NameEnglish { get; set; }
        public string NameArabic { get; set; }
        public string Image { get; set; }
        public IFormFile ImageFile { get; set; }
        public int? ParentId { get; set; }
        public CourseGroup Parent { get; set; }
        public List<CourseGroup> Childs { get; set; }
    }
    public class CategoryListDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Count { get; set; }
        public List<CategoryListDto> SubCategories { get; set; }
    }
    public class CategoryInputDto
    {
        public int Id { get; set; }

        public string NamePersian { get; set; }
        public string NameEnglish { get; set; }
        public string NameArabic { get; set; }

        public int ParentId { get; set; }
    }
    public class HomePageViewModel
    {
        public IEnumerable<CourseItemListViewModel> Courses { get; set; }
    }
}
