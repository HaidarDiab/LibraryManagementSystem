using LibraryManagementSystem.Dtos;
using LibraryManagementSystem.Models;
using LibraryManagementSystem.ViewModels;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace LibraryManagementSystem.Controllers
{
	public class TestController : Controller
	{
		private ApplicationDbContext _context;

		public TestController()
		{
			_context = new ApplicationDbContext();
		}



		[HttpGet]
		public ActionResult Index()
		{
			var userId = User.Identity.GetUserId();
			ViewBag.ApplicationUserId = userId;

			var viewModel = new BlogFormViewModel
			{
				Posts = _context.Posts.Include("Profile").OrderByDescending(p => p.DateAdded).ToList(),
				Comments = _context.Comments.ToList(),
				Comment = new Comment(),
				Profile = _context.Profiles.SingleOrDefault(p => p.ApplicationUserId == userId),
				Profiles = _context.Profiles.ToList(),
				LikeOnPosts = _context.LikeOnPosts.Where(l => l.ApplicationUserId == userId).ToList(),
			};

			return View(viewModel);
		}

		[HttpPost]
		public ActionResult UploadFiles()
		{
			string FileName = "";
			if (Request.Files.Count > 0)
			{
				var files = Request.Files;

				//iterating through multiple file collection   
				foreach (string str in files)
				{
					HttpPostedFileBase file = Request.Files[str] as HttpPostedFileBase;
					//Checking file is available to save.  
					if (file != null)
					{
						FileName = file.FileName;
						var InputFileName = Path.GetFileName(file.FileName);
						var ServerSavePath = Path.Combine(Server.MapPath("~/uploadedBooks/") + InputFileName);
						//Save file to server folder  
						file.SaveAs(ServerSavePath);

					}

				}
				return Json(FileName);
			}
			else
			{
				return Json("No files to upload");
			}
		}

	}
}