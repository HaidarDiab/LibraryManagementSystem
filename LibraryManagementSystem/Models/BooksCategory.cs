using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace LibraryManagementSystem.Models
{
	public class BooksCategory
	{
		public int Id { get; set; }

		[Required(ErrorMessage = "اسم التصنيف مطلوب")]
		[Display(Name = "اسم التصنيف")]
		[StringLength(26)]
		public string Name { get; set; }

		public string ImageName { get; set; }
		public string ImagePath { get; set; }

		[NotMapped]
		public HttpPostedFileBase file { get; set; }
	}
}