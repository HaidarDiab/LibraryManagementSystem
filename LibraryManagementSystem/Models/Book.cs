using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace LibraryManagementSystem.Models
{
	public class Book
	{
		public int Id { get; set; }


		[Required(ErrorMessage = "العنوان مطلوب")]
		[Display(Name = "العنوان")]
		[StringLength(44, ErrorMessage ="عنوان الكتاب يجب أن يكون أقل من 45 حرف")]
		public string Title { get; set; }


		[Required(ErrorMessage = "الملخص مطلوب")]
		[Display(Name = "ملخص")]
		[StringLength(415, ErrorMessage = "ملخص الكتاب يجب أن يكون أقل من 80 كلمة")]
		public string Summary { get; set; }


		[Required(ErrorMessage = "تاريخ الإضافة مطلوب")]
		[Display(Name = "تاريخ الإضافة")]
		public DateTime? ReleaseDate { get; set; }

		[Required(ErrorMessage = "المؤلف مطلوب")]
		[Display(Name = "المؤلف")]
		public string AuthorName { get; set; }

		public string CoverImagePath { get; set; }
		public string CoverImageName { get; set; }

		public string BookFilePath { get; set; }


		[NotMapped]
		public HttpPostedFileBase CoverImageFile { get; set; }

		public BooksCategory BooksCategory { get; set; }

		[Required(ErrorMessage = "التصنيف مطلوب")]
		[Display(Name = "التصنيف")]
		public int BooksCtegoryId { get; set; }
	}
}