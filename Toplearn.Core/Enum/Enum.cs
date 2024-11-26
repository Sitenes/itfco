using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Toplearn.Core.Enum
{
    public enum Coust
    {
            Free = 1,
            Monetary = 2,
            NotDefined = 3,
            All = 4,
    }

    public enum OrderBy
    {
        None = 0,
        PriceAsc = 1,
        PriceDesc = 2,
        TimeAsc = 3,
        TimeDesc = 4,
        CreateDateAsc = 5,
        CreateDateDesc = 6,
    }

    public enum Permission
    {
        Management = 1,
        UserManagement = 2,
        AddUser = 3,
        EditUser = 4,
        RemoveUser = 5,
        RoleManagement = 6,
        EditRole = 7,
        AddRole = 8,
        RemoveRole = 9,
        CourseManagement = 10,
        AddCourse = 11,
        EditCourse = 12,
        RemoveCourse = 13,
        CourseEpisodesManagement = 14,
        AddEpisodeToCourse = 15,
        EditCourseEpisodes = 16,
        RemoveCourseEpisodes = 17,
    }
}
