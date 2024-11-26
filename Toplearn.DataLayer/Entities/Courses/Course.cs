using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Toplearn.DataLayer.Entities.Courses
{
    public class Course
    {
        [Key]
        public int CourseId { get; set; }

        [MaxLength(200, ErrorMessage = "{0} نمی تواند از {1} کاراکتر بیشتر باشد")]
        [Required(ErrorMessage = "لطفا {0} را وارد نمائید")]
        [Display(Name = "عنوان دوره")]
        public string Title { get; set; }//عنوان دوره


        [Column(TypeName = "varchar(50)")]
        public string Image { get; set; }//عکس دوره

        [MaxLength(50,ErrorMessage = "{0} نمی تواند از {1} کاراکتر بیشتر باشد")]
        public string DemoFileName { get; set; }

 
        [Display(Name = "توضیحات کامل دوره")]
        public string Description { get; set; }//توضیحات کامل دوره        

        public int NumberOfStudents { get; set; }//تعداد دانجو هایی که در این دوره ثبت نام کرده اند

        [Required(ErrorMessage = "لطفا {0} را وارد نمائید")]
        public long Price { get; set; }//ملغ دوره

        [Required(ErrorMessage = "لطفا {0} را وارد نمائید")]
        public DateTime RegistrationDate { get; set; }//تاریخ ایجاد کردن این دوره و آپلود اولین فیلم آموزشی

        [Required(ErrorMessage = "لطفا {0} را وارد نمائید")]
        public DateTime LastUpdate { get; set; }//آخرین بروزرسانی این دوره

        [MaxLength(500,ErrorMessage = "{0} نمی تواند از {1} کاراکتر بیشتر باشد")]
        public string Tags { get; set; }

        public int? SubGroupId { get; set; }
        
        public int LevelId { get; set; }

        public int CourseStatusId { get; set; }

        public Guid TeacherId { get; set; }

        public int GroupId { get; set; }


        #region Relations

        [ForeignKey("LevelId")]
        public CourseLevel CourseLevel { get; set; }

        [ForeignKey("CourseStatusId")]
        public CourseStatus CourseStatus { get; set; }

        [ForeignKey("TeacherId")]
        public User.User Teacher { get; set; }

        [ForeignKey("GroupId")]
        public CourseGroup Group { get; set; }

        [ForeignKey("SubGroupId")]
        public CourseGroup SubGroup { get; set; }

        public List<Episode> CourseEpisode { get; set; }
        #endregion
    }
}
