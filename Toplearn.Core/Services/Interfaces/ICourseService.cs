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
using Toplearn.DataLayer.Entities.Courses;

namespace Toplearn.Core.Services.Interfaces
{
	public interface ICourseService
	{
		#region Group
		IEnumerable<CourseGroup> GetGroups();
		IEnumerable<TeacherForCourseViewModel> GetTeachersName();
		IEnumerable<CourseStatus> GetStatuses();
		IEnumerable<CourseLevel> GetLevels();
        Task<IEnumerable<CourseListAdminViewModel>> GetCoursesList();
        Task<IEnumerable<CourseListAdminViewModel>> GetCoursesList(CourseFilterAdminViewModel Filter);
		Task<Course> SetGroup(Course course);
        Task AddCategory(CategoryDto categoryDto);
        Task<List<CategoryDto>> GetAllCourseGroups();
        Task<CategoryDto> GetCategoryById(int id);
        Task UpdateCategory(CategoryDto categoryDto);
        Task DeleteCategory(int id);
        Task<List<CategoryDto>> GetParentCourseGroups();
        Task<int> CountCategoryProducts(int groupId);
        Task<List<CourseGroup>> GetParentCategories();
        #endregion

        #region Courses
        Task<int> AddCourse(Course course, IFormFile ImageFile, IFormFile DemoFile);
		Task<bool> IsCourseExsit(int Id);
        Task<Course> GetCourse(int Id);
        Task<bool> RemoveCourse(int Id);
        Task<IEnumerable<CourseItemListViewModel>> GetCoursesList(CourseFilterListViewModel filter);
        Task<bool> Update(Course course, IFormFile ImageFile, IFormFile DemoFile);
        Task<int> CountCourses();
        #endregion

        #region Episode
        IEnumerable<EpisodeListViewModel> GetEpisodesList(FilterEpisodeListViewModel filters);
        Task<int> AddEpisode(Episode episode, IFormFile video);
        Task<bool> EditEpisode(Episode episode, IFormFile video);
        Task<Episode> GetEpisode(int Id);
        Task<bool> RemoveEpisode(int Id);

        #endregion

        #region Discount
        Task<List<Discount>> GetAllDiscountsAsync();
        Task<Discount> GetDiscountByIdAsync(int id);
        Task AddDiscountAsync(Discount discount);
        Task UpdateDiscountAsync(Discount discount);
        Task DeleteDiscountAsync(int id);
        Task<Discount> SearchDiscountsAsync(string keyword);
        Task<bool> ApplyDiscountAsync(string code, int userId, int courseId);
        #endregion
        Task SaveChanges();
    }
}
