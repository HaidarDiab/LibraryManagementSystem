using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LibraryManagementSystem.Models
{
	public class FavouriteBookList
	{
		public int Id { get; set; }
		public Book Book { get; set; }

		public int  BookId { get; set; }

		public ApplicationUser ApplicationUser { get; set; }
		public string ApplicationUserId { get; set; }

	}
}