using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LibraryManagementSystem.Models;

namespace LibraryManagementSystem.ViewModels
{
	public class AdminViewModel
	{
		public ApplicationUser ApplicationUser { get; set; }
		public IEnumerable<ApplicationUser> ApplicationUsers { get; set; }

		public IEnumerable<Profile> Profiles { get; set; }
		public Profile Profile { get; set; }
	}
}