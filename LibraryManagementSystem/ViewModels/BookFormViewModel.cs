using LibraryManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace LibraryManagementSystem.ViewModels
{
	public class BookFormViewModel
	{
		
		[Display(Name = "الفئة")]
		public IEnumerable<BooksCategory> BooksCategories { get; set; }


		public Book Book { get; set; }

		public int Id { get; set; }


		[Required(ErrorMessage = "العنوان مطلوب")]
		[Display(Name = "العنوان")]
		public string Title { get; set; }


		[Required(ErrorMessage = "الملخص مطلوب")]
		[Display(Name = "ملخص")]
		public string Summary { get; set; }


		[Required(ErrorMessage = "تاريخ الإضافة مطلوب")]
		[Display(Name = "تاريخ الإضافة")]
		public DateTime? ReleaseDate { get; set; }

		[Required(ErrorMessage = "المؤلف مطلوب")]
		[Display(Name = "المؤلف")]
		public string AuthorName { get; set; }

		public string CoverImagePath { get; set; }
		public string CoverImageName { get; set; }


		[NotMapped]
		public HttpPostedFileBase CoverImageFile { get; set; }


		[Required(ErrorMessage = "التصنيف مطلوب")]
		[Display(Name = "التصنيف")]
		public int BooksCtegoryId { get; set; }

	}
}