using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LibraryManagementSystem.Models
{
	public class Comment
	{
		public int Id { get; set; }

		public DateTime? CommentDateAdded { get; set; }

		[Display(Name = "تعليق")]
		public string CommentContent { get; set; }

		[Display(Name ="إعجاب")]
		public bool? Like  { get; set; }

		public ApplicationUser ApplicationUser { get; set; }
		public string ApplicationUserId { get; set; }

		public Post Post { get; set; }
		public int PostId { get; set; }

	}
}