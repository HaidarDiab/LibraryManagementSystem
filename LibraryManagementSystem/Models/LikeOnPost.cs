﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LibraryManagementSystem.Models
{
	public class LikeOnPost
	{
		public int Id { get; set; }
		public bool Like { get; set; }
		public ApplicationUser ApplicationUser { get; set; }
		public string ApplicationUserId { get; set; }
		public Post Post { get; set; }
		public int PostId { get; set; }
	}
}