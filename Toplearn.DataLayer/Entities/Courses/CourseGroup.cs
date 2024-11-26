using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Toplearn.DataLayer.Entities.Courses
{
    public class CourseGroup
    {
        [Key]
        public int GroupId { get; set; }

        [MaxLength(100, ErrorMessage = "{0} نمی تواند از {1} کاراکتر بیشتر باشد")]
        [Display(Name = "عنوان گروه")]
        [Required(ErrorMessage = "لطفا {0} را وارد نمائید")]
        public string GroupTitle { get; set; }

        [Display(Name = "حذف شده ؟")]
        public bool IsDeleted { get; set; }

        public int? ParentId { get; set; }

        #region Relations
        [ForeignKey("ParentId")]
        public CourseGroup Parent { get; set; }
        
        [InverseProperty("Group")]
        public List<Course> CourseGroups { get; set; }

        [InverseProperty("SubGroup")]
        public List<Course> SubGroups { get; set; }

        #endregion
    }
}
