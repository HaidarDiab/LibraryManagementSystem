using LibraryManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LibraryManagementSystem.ViewModels
{
	public class BlogFormViewModel
	{
		public IEnumerable<Post> Posts { get; set; }
		public IEnumerable<Comment> Comments { get; set; }

		public IEnumerable<Profile> Profiles { get; set; }
		public IEnumerable<LikeOnPost> LikeOnPosts { get; set; }


		public Profile Profile { get; set; }
		public Comment Comment { get; set; }
		public Post Post { get; set; }
		public LikeOnPost LikeOnPost { get; set; }
	}
}