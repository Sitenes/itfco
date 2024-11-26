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

        #endregion

        #region Courses
        Task<int> AddCourse(Course course, IFormFile ImageFile, IFormFile DemoFile);
		Task<bool> IsCourseExsit(int Id);
        Task<Course> GetCourse(int Id);
        Task<bool> RemoveCourse(int Id);
        Task<IEnumerable<CourseItemListViewModel>> GetCoursesList(CourseFilterListViewModel filter);
        Task<bool> Update(Course course, IFormFile ImageFile, IFormFile DemoFile);
        #endregion

        #region Episode
        IEnumerable<EpisodeListViewModel> GetEpisodesList(FilterEpisodeListViewModel filters);
        Task<int> AddEpisode(Episode episode, IFormFile video);
        Task<bool> EditEpisode(Episode episode, IFormFile video);
        Task<Episode> GetEpisode(int Id);
        Task<bool> RemoveEpisode(int Id);

        #endregion

        Task SaveChanges();
    }
}
