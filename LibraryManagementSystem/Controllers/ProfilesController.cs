using LibraryManagementSystem.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LibraryManagementSystem.Controllers
{

	[Authorize]
	public class ProfilesController : Controller
	{

		private ApplicationDbContext _context;

		public ProfilesController()
		{
			_context = new ApplicationDbContext();
		}

		
		[HttpGet]
		public ActionResult Index()
		{
			var userId = User.Identity.GetUserId();
			var profile = _context.Profiles.SingleOrDefault(p => p.ApplicationUserId == userId);
			if (profile != null)
			{

			return View(profile);
			}

            else return RedirectToAction("AddProfile", "Profiles");

        }


        [HttpGet]
		public ActionResult AddProfile()
		{
			var profile = new Profile();

			return View(profile);
		}



		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult AddProfile(Profile profile, HttpPostedFileBase file)
		{
			var userId = User.Identity.GetUserId();


			if (_context.Profiles.Any(p => p.ApplicationUserId == userId))
			{
				ViewBag.Result = "أنت تمتلك ملف شخصي";
				return View();
			}

			if (file != null && file.ContentLength > 0)
			{
				string path = Path.Combine(Server.MapPath("~/images"), Path.GetFileName(file.FileName));

				file.SaveAs(path);

				var pro = new Profile
				{
					ApplicationUserId = userId,
					FirstName = profile.FirstName,
					LastName = profile.LastName,
					BirthDate = profile.BirthDate,
					Email = profile.Email,
					PhoneNumber = profile.PhoneNumber,
					Address = profile.Address,
					ImageName = file.FileName,
					ImagePath = path,
				};
				_context.Profiles.Add(pro);
			}
			else if (file == null)
			{
				var pro = new Profile
				{
					ApplicationUserId = userId,
					FirstName = profile.FirstName,
					LastName = profile.LastName,
					BirthDate = profile.BirthDate,
					Email = profile.Email,
					PhoneNumber = profile.PhoneNumber,
					Address = profile.Address,
					ImageName = null,
					ImagePath = null,
				};
				_context.Profiles.Add(pro);
			}



			_context.SaveChanges();

			return RedirectToAction("Index", "Home");
		}




		[HttpGet]
		public ActionResult EditProfile(string id)
		{
			var userId = User.Identity.GetUserId();

			var profile = _context.Profiles.Single(p => p.ApplicationUserId == id);

			return View(profile);
		}


		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult EditProfile(Profile profile, HttpPostedFileBase file)
		{
			var userId = User.Identity.GetUserId();

			var profileInDb = _context.Profiles.Single(p => p.ApplicationUserId == userId);

			if (profile == null)
				return HttpNotFound();

			if (file != null && file.ContentLength > 0) 
			{
				string path = Path.Combine(Server.MapPath("~/images"), Path.GetFileName(file.FileName));

				file.SaveAs(path);

				profileInDb.FirstName = profile.FirstName;
				profileInDb.LastName = profile.LastName;
				profileInDb.BirthDate = profile.BirthDate;
				profileInDb.Email = profile.Email;
				profileInDb.PhoneNumber = profile.PhoneNumber;
				profileInDb.Address = profile.Address;
				profileInDb.ImageName = file.FileName;
				profileInDb.ImagePath = path;
			}

			else
			{
				profileInDb.FirstName = profile.FirstName;
				profileInDb.LastName = profile.LastName;
				profileInDb.BirthDate = profile.BirthDate;
				profileInDb.Email = profile.Email;
				profileInDb.PhoneNumber = profile.PhoneNumber;
				profileInDb.Address = profile.Address;
				profileInDb.ImageName = profileInDb.ImageName;
				profileInDb.ImagePath = profileInDb.ImagePath;
			}

			_context.SaveChanges();

			return RedirectToAction("Index", "Profiles");
		}
	
	}
}
