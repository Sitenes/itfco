using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Toplearn.DataLayer.Entities.User;

namespace Toplearn.DataLayer.Entities.Blogs
{
	public class Blog
	{
        public int Id { get; set; }
        public string Title { get; set; }
        public string Image { get; set; }
        public DateTime CreateDate { get; set; }
        public string Content { get; set; }
		public string Tags { get; set; }
        public string ShortDescription { get; set; }
        public Guid UserCreatorId { get; set; }
		public Entities.User.User UserCreator { get; set; }
	}
}
