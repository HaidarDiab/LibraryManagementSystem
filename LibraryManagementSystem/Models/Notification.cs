using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LibraryManagementSystem.Models
{
	public class Notification
	{
		public int Id { get; set; }
		public DateTime DateTime { get; set; }
		public NotificationType Type { get; set; }
		public DateTime? OriginalDateTime { get; set; }
		public string OriginalPost { get; set; }

		public Post Post { get; set; }
		//public int PostId { get; set; }
	}
}