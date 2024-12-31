using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Toplearn.DataLayer.Entities.Courses
{
    public class Discount
	{
        [Key]
        public int Id { get; set; }
        public string Title { get; set; }
        public int Percent { get; set; }
        public DateTime CreateDate { get; set; }
        public string DiscountCode { get; set; }
    }
}
