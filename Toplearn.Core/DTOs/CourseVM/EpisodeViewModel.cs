using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Toplearn.DataLayer.Entities.Courses;

namespace Toplearn.Core.DTOs.CourseVM
{
    public class EpisodeListViewModel
    {
        public int EpisodeId { get; set; }
        public string EpisodeTitle { get; set; }
        public TimeSpan EpisodeTime { get; set; }
        public bool IsFree { get; set; }
    }
    public class FilterEpisodeListViewModel
    {
        public int CourseId { get; set; }
        public string Title { get; set; }
        public TimeSpan EpisodeTimeFrom { get; set; }
        public TimeSpan EpisodeTimeTo { get; set; }
        public Enum.Coust EpisodeStatus { get; set; }
    }
}
