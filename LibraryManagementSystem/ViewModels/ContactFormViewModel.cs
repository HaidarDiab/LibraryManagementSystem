using LibraryManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LibraryManagementSystem.ViewModels
{
	public class ContactFormViewModel
	{

		public Profile Profile { get; set; }


		[Required(ErrorMessage = "اسمك الكامل مطلوب")]
		[Display(Name = "اسمك الكامل")]
		[StringLength(50, ErrorMessage = "يجب ان لا يتجاوز الاسم 50 حرف")]
		public string FullName { get; set; }


		[Required(ErrorMessage = "البريد الالكتروني مطلوب")]
		[Display(Name = "بريدك")]
		[StringLength(80, ErrorMessage = "يجب ان لا يتجاوز البريد 80 حرف")]
		[EmailAddress(ErrorMessage = "البريد الالكتروني غير صالح")]
		public string YourEmail { get; set; }


		[Required(ErrorMessage = "موضوع أو عنوان البريد مطلوب")]
		[StringLength(100, ErrorMessage = "يجب ان لا يتجاوز العنوان 100 حرف")]
		[Display(Name = "الموضوع")]
		public string Subject { get; set; }

		[Required(ErrorMessage = "المحتوى مطلوب")]
		[StringLength(415, ErrorMessage = "يجب ان لا يتجاوز المتن 80 كلمة")]
		[Display(Name = "المحتوى")]
		public string Message { get; set; }
	}
}