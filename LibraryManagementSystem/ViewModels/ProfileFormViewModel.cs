using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LibraryManagementSystem.Models;

namespace LibraryManagementSystem.ViewModels
{
	public class ProfileFormViewModel
	{
		public Profile Profile{ get; set; }
		public string ActionName { get; set; }
	}
}