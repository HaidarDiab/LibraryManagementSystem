using LibraryManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LibraryManagementSystem.ViewModels
{
	public class SearchFormViewModel
	{
		public string SearchTearms { get; set; }
		public IEnumerable<Book> Books { get; set; }
	}
}