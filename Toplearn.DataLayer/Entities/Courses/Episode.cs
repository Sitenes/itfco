using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Toplearn.DataLayer.Entities.Courses
{
    public class Episode
    {
        [Key]
        public int EpisodeId { get; set; }

        [MaxLength(100, ErrorMessage = "{0} نمی تواند از {1} کاراکتر بیشتر باشد")]
        [Display(Name = "عنوان قسمت")]
        [Required(ErrorMessage = "لطفا {0} را وارد نمائید")]
        public string EpisodeTitle { get; set; }

        [MaxLength(50, ErrorMessage = "{0} نمی تواند از {1} کاراکتر بیشتر باشد")]
        [Display(Name = "نام فایل قسمت")]
        [Column(TypeName = "varchar")]
        public string EpisodeFileName { get; set; }

        [Required(ErrorMessage = "لطفا {0} را وارد نمائید")]
        public TimeSpan EpisodeTime { get; set; }

        [Display(Name ="رایگان")]
        public bool IsFree { get; set; }

        public int CourseId { get; set; }

        #region Relations
        [ForeignKey("CourseId")]
        public Course Course { get; set; }
        #endregion
    }
}
