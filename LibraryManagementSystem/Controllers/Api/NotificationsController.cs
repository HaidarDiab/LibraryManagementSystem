using LibraryManagementSystem.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace LibraryManagementSystem.Controllers.Api
{
	public class NotificationsController : ApiController
	{

		private ApplicationDbContext _context;

		public NotificationsController()
		{
			_context = new ApplicationDbContext();
		}

		//Get: /api/Notifications
		public IEnumerable<Notification> GetNotifications()
		{
			var userId = User.Identity.GetUserId();
			var notifications = _context.UserNotifications.Where(u => u.UserrId == userId && u.IsRead == false).Select(u => u.Notification).ToList();

			return notifications;
		}


		
	}
}
