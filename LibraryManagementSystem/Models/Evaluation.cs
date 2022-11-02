using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LibraryManagementSystem.Models
{
	public class Evaluation
	{
		public int Id { get; set; }

		[Required(ErrorMessage = "الحقل مطلوب")]
		[Range(1, 5, ErrorMessage = "يجب أن يكون التقييم بين 1 و 5")]
		[Display(Name = "مستوى التقييم")]
		public byte Level { get; set; }
		public ApplicationUser ApplicationUser { get; set; }

		[Required(ErrorMessage = "الحقل مطلوب")]
		public string ApplicationUserId { get; set; }

		public Book Book { get; set; }

		[Required(ErrorMessage = "الحقل مطلوب")]
		public int BookId { get; set; }
	}
}