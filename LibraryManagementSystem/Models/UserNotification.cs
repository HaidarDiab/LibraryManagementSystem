using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace LibraryManagementSystem.Models
{
	public class UserNotification
	{
		[Key]
		[Column(Order = 1)]
		public string UserrId { get; set; }


		[Key]
		[Column(Order = 2)]
		public int NotificationId { get; set; }


		public ApplicationUser ApplicationUser { get; set; }
		public Notification Notification { get; set; }

		public bool IsRead { get; set; }
	}
}