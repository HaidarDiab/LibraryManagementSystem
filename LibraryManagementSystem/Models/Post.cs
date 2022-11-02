using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LibraryManagementSystem.Models
{
	public class Post
	{
		public int Id { get; set; }

		[Required(ErrorMessage = "هذا الحقل مطلوب")]
		[Display(Name = "محتوى المنشور")]
		[StringLength(1300, ErrorMessage ="تجاوزت الحد المسموح به من الكلمات")]
		public string Contains { get; set; }

		[Required(ErrorMessage = "تاريخ النشر مطلوب")]
		[Display(Name = "تاريخ المنشور")]
		public DateTime? DateAdded { get; set; }
		public Profile Profile { get; set; }
		public int ProfileId { get; set; }

		public ApplicationUser ApplicationUser { get; set; }
		public string ApplicationUserId { get; set; }
	}
}