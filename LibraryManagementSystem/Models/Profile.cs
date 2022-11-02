using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace LibraryManagementSystem.Models
{
	public class Profile
	{
		public int Id { get; set; }

		[Display(Name = "الاسم")]
		public string FirstName { get; set; }


		[Display(Name = "الكنية")]
		public string LastName { get; set; }

		[Display(Name = "تاريخ الميلاد")]
		public DateTime BirthDate { get; set; }

		[Display(Name = "العنوان")]
		public string Address { get; set; }

		[Display(Name = "البريد")]
		public string Email { get; set; }


		[Display(Name = "رقم الهاتف")]
		public string PhoneNumber { get; set; }
		public string ImageName { get; set; }
		public string ImagePath { get; set; }

		[NotMapped]
		public HttpPostedFileBase httpPostedFileBase { get; set; }
		public ApplicationUser ApplicationUser { get; set; }
		public string ApplicationUserId { get; set; }

	}
}