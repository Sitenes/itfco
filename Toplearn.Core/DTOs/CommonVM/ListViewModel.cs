using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Xml.Linq;
using Microsoft.AspNetCore.Http;
using Toplearn.Core.DTOs.CourseVM;

namespace Toplearn.Core.DTOs.UserVM
{
	public class ListViewModel<T>
	{
        public IEnumerable<T> Data { get; set; }
        public int TotalCount { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }
	
	
}
