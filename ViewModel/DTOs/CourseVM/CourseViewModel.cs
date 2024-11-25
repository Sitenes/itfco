using itfco.Entity.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace itfco.ViewModel.DTOs.ProductVM
{

	public class ProductListAdminViewModel
	{
		public int ProductId { get; set; }

		public string ProductTitle { get; set; }
        
		public string Image { get; set; }//عکس دوره
        
		public int NumberOfStudents { get; set; }//تعداد دانجو هایی که در این دوره ثبت نام کرده اند

        public long Price { get; set; }//ملغ دوره

        public string TeacherName { get; set; }
    }

    public class ProductItemListViewModel
    {

        public int Id { get; set; }
        public string Title { get; set; }
        public string Image { get; set; }
        public long Price { get; set; }
        public TimeSpan Time { get; set; }
    }


    public class ProductFilterListViewModel
    {
        public string Title { get; set; }
        public Coust Coust{ get; set; }
        public long StartPrice { get; set; }
        public long EndPrice { get; set; }
        public OrderBy OrderBy { get; set; }
        public List<int> Groups { get; set; }
    }
	public class ProductFilterAdminViewModel
	{
		public string Title { get; set; }
        public string Description { get; set; }//توضیحات کامل دوره        

        public int StudentsCountFrom { get; set; }//تعداد دانجو هایی که در این دوره ثبت نام کرده اند

        public int StudentsCountTo { get; set; }//تعداد دانجو هایی که در این دوره ثبت نام کرده اند

        public long PriceFrom { get; set; }//ملغ دوره

        public long PriceTo { get; set; }//ملغ دوره

        public string Tags { get; set; }

        public int LevelId { get; set; }

        public int ProductStatusId { get; set; }

        public string TeacherOrProductId { get; set; }

        public int GroupId { get; set; }

        public int ItemPerPage { get; set; }

        public int ProductsCount { get; set; }

        public int CurrentPage { get; set; }
    }

    public class HomePageViewModel
    {
        public IEnumerable<ProductItemListViewModel> Products { get; set; }
    }
}
