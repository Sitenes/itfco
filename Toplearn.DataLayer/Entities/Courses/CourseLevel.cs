using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Toplearn.DataLayer.Entities.Courses
{
    public class CourseLevel
    {
        [Key]
        public int CourseLevelId { get; set; }
        
        [Required(ErrorMessage ="لطفا {0} را وارد نمائید")]
        [MaxLength(200,ErrorMessage = "{0} نمی تواند از {1} کاراکتر بیشتر باشد")]
        public string LevelTitle { get; set; }

        #region Relations
        /// <summary>
        /// Courses That Has This CourseLevel
        /// </summary>
        public List<Course> Courses { get; set; }

        #endregion
    }
}
