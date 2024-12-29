using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Toplearn.Core.DTOs.TeacherVM
{
	public class BlogFilter
	{
		public string Title { get; set; }
		public DateTime? StartDate { get; set; }
		public DateTime? EndDate { get; set; }
		public string Tags { get; set; }
		public Guid? UserCreatorId { get; set; }
        public int PageSize { get; set; }
        public int PageNumber { get; set; }
    }


}
