using LibraryManagementSystem.Models;
using LibraryManagementSystem.ViewModels;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace LibraryManagementSystem.Controllers.Api
{
	public class AdminController : ApiController
	{
		private ApplicationDbContext _context;

		public AdminController()
		{
			_context = new ApplicationDbContext();
		}



		// Get: /api/Admin/Users
		public IEnumerable<ApplicationUser> GetUsers()
		{

			return _context.Users.ToList();
		}



		// Delete:  /api/Admin/Delete/1
		[HttpDelete]
		public IHttpActionResult DeleteUser(string id)
		{
			var user = _context.Users.SingleOrDefault(u => u.Id == id);
			var profile = _context.Profiles.SingleOrDefault(p => p.ApplicationUserId == id);

			if (user == null && profile == null)
				return NotFound();

			_context.Profiles.Remove(profile);
			_context.Users.Remove(user);

			_context.SaveChanges();

			return Ok();
		}
	}
}
