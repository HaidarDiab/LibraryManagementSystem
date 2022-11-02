using LibraryManagementSystem.Dtos;
using LibraryManagementSystem.Models;
using LibraryManagementSystem.ViewModels;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace LibraryManagementSystem.Controllers
{
	public class HomeController : Controller
	{

		private ApplicationDbContext _context;

		public HomeController()
		{
			_context = new ApplicationDbContext();
		}


//..........................................Implement Post .........................................


		//this is for blog, thats mean it will be the home page for our application, it will be public to see but if you want to comment you should register
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



		//.................................implement Comments on the post....................



		[HttpGet]
		public ActionResult DisplayComments(int id) 
		{
			var userId = User.Identity.GetUserId();
			ViewBag.ApplicationUserId = userId;

			var post = _context.Posts.SingleOrDefault(p => p.Id == id);

			var viewModel = new BlogFormViewModel
			{
				Post = post,
				Posts = _context.Posts.Include("Profile").OrderByDescending(p => p.DateAdded).ToList(),
				Comments = _context.Comments.OrderByDescending(c => c.CommentDateAdded).ToList(),
				Profile = _context.Profiles.SingleOrDefault(p => p.Id == post.ProfileId),
				Profiles = _context.Profiles.ToList(),
				LikeOnPosts = _context.LikeOnPosts.Where(l => l.ApplicationUserId == userId).ToList(),
			};

			return View(viewModel);

		}


		[Authorize]
		[HttpGet]
		public ActionResult AddComment(int id)
		{
			var userId = User.Identity.GetUserId();


			var viewModel = new BlogFormViewModel
			{
				Post = _context.Posts.Include("Profile").SingleOrDefault(p => p.Id == id),
				Comment = new Comment(),
				Profile = _context.Profiles.SingleOrDefault(p => p.ApplicationUserId == userId),
				LikeOnPosts = _context.LikeOnPosts.Where(l => l.PostId == id && l.ApplicationUserId == userId).ToList(),
			};

			return View(viewModel);
		}


		[Authorize]
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult AddComment(BlogFormViewModel viewModel)
		{
			var userId = User.Identity.GetUserId();

			var newComment = new Comment
			{
				ApplicationUserId = userId,
				Like = false,
				CommentDateAdded = DateTime.Now,
				CommentContent = viewModel.Comment.CommentContent,
				PostId = viewModel.Post.Id,
			};

			_context.Comments.Add(newComment);
			_context.SaveChanges();

			return RedirectToAction("DisplayComments", "Home", new { id = viewModel.Post.Id });
		}
		//..................... ;;;;;;;;  .......................................


		[Authorize]
		public ActionResult DeleteComment(int commentId)
		{
			var userId = User.Identity.GetUserId();

			var comment = _context.Comments.Single(c => c.Id == commentId && c.ApplicationUserId == userId);

			if (comment == null)
				return View("NotFound");

			_context.Comments.Remove(comment);
			_context.SaveChanges();

			var post = _context.Posts.SingleOrDefault(p => p.Id == comment.PostId);

			return RedirectToAction("DisplayComments", "Home", new { id = post.Id });
		}




		[Authorize]
		[HttpPost]
		public ActionResult LikePost(PostDto dto)
		{
			var userId = User.Identity.GetUserId();
			var likeInDb = _context.LikeOnPosts.SingleOrDefault(l => l.PostId == dto.PostId && l.ApplicationUserId == userId);

			if (_context.LikeOnPosts.Any(l => l.PostId == dto.PostId && l.ApplicationUserId == userId))
			{
				_context.LikeOnPosts.Remove(likeInDb);
			}

			else
			{
				var like = new LikeOnPost
				{
					ApplicationUserId = userId,
					Like = true,
					PostId = dto.PostId,
				};

				_context.LikeOnPosts.Add(like);
			}
			_context.SaveChanges();

			return RedirectToAction("Index", "Home");
		}



		[Authorize]
		[HttpGet]
		public ActionResult AddPost()
		{
			var userId = User.Identity.GetUserId();

			var post = new Post
			{
				ApplicationUserId = userId,
				Profile = _context.Profiles.SingleOrDefault(p => p.ApplicationUserId == userId),
			};

			return View(post);
		}



		[Authorize]
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult AddPost(Post post)
		{
			var userId = User.Identity.GetUserId();
			var profile = _context.Profiles.SingleOrDefault(p => p.ApplicationUserId == userId);

			var postaftermessage = new Post
			{
				Profile = profile,
				ApplicationUserId = userId,
			};

			//if (!ModelState.IsValid)
			//{
			//	ViewBag.result = "الرجاء التحقق من الحقول المدخلة";
			//	return View(postaftermessage);
			//}



			if (_context.Posts.Any(p => p.ApplicationUserId == userId && (p.Contains.Equals(post.Contains) && p.DateAdded >= DateTime.Today)))
			{
				ViewBag.Result = "قمت بنشر هذا البوست اليوم";
				return View(postaftermessage);
			}

			var newPost = new Post
			{
				Contains = post.Contains,
				DateAdded = DateTime.Now,
				Profile = profile,
				ApplicationUserId = userId
			};

			_context.Posts.Add(newPost);


			var notification = new Notification
			{
				DateTime = DateTime.Now,
				Type = NotificationType.PostCreated,
			};

			_context.Notifications.Add(notification);

			var userNotification = new UserNotification
			{
				UserrId = userId,
				NotificationId = notification.Id,
			};
			_context.UserNotifications.Add(userNotification);

			_context.SaveChanges();


			return RedirectToAction("MyPosts", "Home");
		}



		[HttpGet]
		public ActionResult MyPosts()
		{
			var userId = User.Identity.GetUserId();

			var viewModel = new BlogFormViewModel
			{
				Posts = _context.Posts.Include("Profile").Where(p => p.ApplicationUserId == userId).ToList(),
			};

			return View(viewModel);
		}




		[Authorize]
		[HttpPut]
		public ActionResult UpdatePost(int id, string contents)
		{
			var userId = User.Identity.GetUserId();

			var postInDb = _context.Posts.Single(p => p.ApplicationUserId == userId && p.Id == id);

			if (postInDb == null)
				return View("NotFound");

			else
			{

				postInDb.DateAdded = DateTime.Now;
				postInDb.ApplicationUserId = userId;
				postInDb.Contains = contents;
			}

			_context.SaveChanges();



			var viewModel = new BlogFormViewModel
			{
				Posts = _context.Posts.Include("Profile").Where(p => p.ApplicationUserId == userId).ToList(),
			};


			return View("MyPosts", viewModel);
		}

		[Authorize]
		[HttpDelete]
		public ActionResult DeletePost(int id)
		{
			var userId = User.Identity.GetUserId();

			var post = _context.Posts.Single(p => p.Id == id && p.ApplicationUserId == userId);

			if (post == null)
				return View("NotFound");

			_context.Posts.Remove(post);

			_context.SaveChanges();


			var viewModel = new BlogFormViewModel
			{
				Posts = _context.Posts.Include("Profile").Where(p => p.ApplicationUserId == userId).ToList(),
			};


			return View("MyPosts", viewModel);
		}

//................................... ;;;;;;; ......................................









//..............................Cntact Us.............................

		[HttpGet]
		public ActionResult Contact()
		{
			var viewModel = new ContactFormViewModel
			{
				Profile = _context.Profiles.SingleOrDefault(p => p.FirstName == "Admin"),

			};

			return View(viewModel);
		}


		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Contact(ContactFormViewModel viewModel, HttpPostedFileBase file)
		{

			var contactModel = new ContactFormViewModel
			{
				Profile = _context.Profiles.SingleOrDefault(p => p.FirstName == "Admin"),

				FullName = viewModel.FullName,
				YourEmail = viewModel.YourEmail,
				Subject = viewModel.Subject,
				Message = viewModel.Message,
			};


			if (!ModelState.IsValid)
				return View(contactModel);

			try
			{
				var mailMessage = new MailMessage(viewModel.YourEmail, ConfigurationManager.AppSettings["toMailAccount"]);

				mailMessage.Subject = viewModel.Subject;
				mailMessage.Body = $"Name: {viewModel.FullName} <br/> Email: {viewModel.YourEmail} <br/> Body: {viewModel.Message}";

				if (file != null)
				{
					var fileName = Path.GetFileName(file.FileName);
					mailMessage.Attachments.Add(new Attachment(file.InputStream, file.FileName));
				}

				mailMessage.IsBodyHtml = true;

				var smtp = new SmtpClient();
				smtp.Host = ConfigurationManager.AppSettings["host"];
				smtp.EnableSsl = true;

				var networkCredential = new NetworkCredential(ConfigurationManager.AppSettings["fromMailAccount"],
					ConfigurationManager.AppSettings["mailPassword"]);

				smtp.UseDefaultCredentials = true;
				smtp.Credentials = networkCredential;
				smtp.Port = int.Parse(ConfigurationManager.AppSettings["port"]);
				smtp.Send(mailMessage);
				ViewBag.Result = " نشكرك على مراسلتنا، سيتم الرد في أقرب وقت ممكن";
			}

			catch (Exception)
			{
				ViewBag.Error = "فشل الإرسال، من فضلك أعد المحاولة لاحقاً";
			}

			return View(contactModel);
		}


//.......................... ;;........................................




//..................Ctegorybooks and Books Implemntetions.....................


		[HttpGet]
		public ActionResult CategoriesIndex()
		{
			var categories = _context.BooksCategories.ToList();

			return View(categories);
		}


		[HttpGet]
		public ActionResult DisplaySpecificBooksCategory(int id)
		{
			var books = _context.Books.Where(b => b.BooksCtegoryId == id).ToList();

			return View(books);
		}

		[HttpGet]
		public ActionResult BooksIndex()
		{
			var books = _context.Books.ToList().OrderByDescending(b => b.ReleaseDate);
			return View(books);
		}


		[HttpGet]
		public ActionResult BookDetails(int id)
		{
			var book = _context.Books.SingleOrDefault(b => b.Id == id);

			var viewModel = new EvaluationBookDetailsFormViewModel
			{
				Book = book,
				Evaluations = _context.Evaluations.Where(e => e.BookId == book.Id).ToList(),
			};

			return View(viewModel);
		}



		[HttpGet]
		public ActionResult BrowsePdf(int id)
		{
			var book = _context.Books.Single(b => b.Id == id);

			string filePath = book.BookFilePath;

			return File(filePath, "application/pdf");
		}



		[HttpGet]
		public ActionResult DownloadBook(int id)
		{

			var book = _context.Books.Single(b => b.Id == id);
			string path = book.BookFilePath;

			byte[] pdfByte = GetBytesFromFile(path);
			return File(pdfByte, "applicaion/pdf", book.BookFilePath);
		}


		// Function To Download Pdf
		public byte[] GetBytesFromFile(string path)
		{
			// this method is limited to 2^32 byte files (4.2 GB)
			FileStream fs = null;
			try
			{
				fs = System.IO.File.OpenRead(path);
				byte[] bytes = new byte[fs.Length];
				fs.Read(bytes, 0, Convert.ToInt32(fs.Length));
				return bytes;
			}
			finally
			{
				if (fs != null)
				{
					fs.Close();
					fs.Dispose();
				}
			}
		}


//..................Ctegory Of Book Implemntetions.....................





//....................Evaluation implement.............................

		[HttpGet]
		public ActionResult UserEvaluations(int id)
		{
			var userId = User.Identity.GetUserId();
			var evaluations = _context.Evaluations.ToList().Where(e => e.Id == id && e.ApplicationUserId == userId);

			return View(evaluations);
		}


		[HttpGet]
		[Authorize]
		public ActionResult AddEvaluations(int id)
		{

			var evaluation = new Evaluation
			{
				Book = _context.Books.SingleOrDefault(b => b.Id == id),
			};

			return View(evaluation);
		}


		[HttpPost]
		[Authorize]
		[ValidateAntiForgeryToken]
		public ActionResult AddEvaluations(Evaluation evaluationModel)
		{
			var userId = User.Identity.GetUserId();


			if (_context.Evaluations.Any(e => e.BookId == evaluationModel.Book.Id && e.ApplicationUserId == userId))
			{
				var evaluationInDb = _context.Evaluations.Single(e => e.BookId == evaluationModel.Book.Id && e.ApplicationUserId == userId);

				evaluationInDb.Level = evaluationModel.Level;
				evaluationInDb.ApplicationUserId = userId;
				evaluationInDb.BookId = evaluationModel.Book.Id;
			}

			else
			{

				var evaluation = new Evaluation
				{
					ApplicationUserId = userId,
					BookId = evaluationModel.Book.Id,
					Level = evaluationModel.Level,
				};

				_context.Evaluations.Add(evaluation);
			}


			_context.SaveChanges();

			ViewBag.Result = "تمت العملية بنجاح";
			return View(evaluationModel);
		}


		[Authorize]
		[HttpDelete]
		[ValidateAntiForgeryToken]
		public ActionResult DeleteEvaluation(int id)
		{
			var userId = User.Identity.GetUserId();

			var evaluation = _context.Evaluations.Single(e => e.Id == id && e.ApplicationUserId == userId);

			if (evaluation == null)
				return View("NotFound");

			_context.Evaluations.Remove(evaluation);
			_context.SaveChanges();
			return RedirectToAction("UserEvaluations", "Home");
		}

//..................   ;;;   .....................




//...........................implement favourit list of books....................
		[Authorize]
		[HttpGet]
		public ActionResult FavouriteBookListIndex()
		{
			var userId = User.Identity.GetUserId();

			var viewModel = _context.FavouriteBookLists.Include("Book").Where(f => f.ApplicationUserId == userId).ToList();

			return View(viewModel);
		}



		[Authorize]
		[HttpGet]
		public ActionResult AddBookToFavouriteList(int id)
		{
			var book = _context.Books.SingleOrDefault(b => b.Id == id);
			var userId = User.Identity.GetUserId();


			// beacuse this action dont have view and we user instead of it "BookDetails View" That use this viewModel so we have to use aand intalise this ViewModel here
			var viewModel = new EvaluationBookDetailsFormViewModel
			{
				Book = book,
				Evaluations = _context.Evaluations.Where(e => e.BookId == book.Id).ToList(),
			};


			if (_context.FavouriteBookLists.Any(e => e.BookId == id && e.ApplicationUserId == userId))
			{
				ViewBag.Result = "الكتاب موجود بالمفضلة";
				return View("BookDetails", viewModel);
			}


			var bookInFvaouriteList = new FavouriteBookList
			{
				BookId = book.Id,
				ApplicationUserId = userId,
			};

			_context.FavouriteBookLists.Add(bookInFvaouriteList);

			_context.SaveChanges();

			ViewBag.Result = "تمت إضافة الكتاب للمفضلة";

			return View("BookDetails", viewModel);
		}






		// ...................Implement Serach Book...............................

		[HttpGet]
		public ActionResult Search()
		{
			var viewModel = new SearchFormViewModel();
			return View(viewModel);
		}


		[HttpPost]
		public ActionResult Search(SearchFormViewModel viewModel)
		{

			return RedirectToAction("IndexSearchBooks", "Home", new { query = viewModel.SearchTearms });
		}


		[HttpGet]
		public ActionResult IndexSearchBooks(string query)
		{
			var books = _context.Books.OrderByDescending(b => b.ReleaseDate).Where(b => b.Id >= 1);

			if (!String.IsNullOrWhiteSpace(query))
			{
				books = _context.Books.Where(b => b.Title.Contains(query) ||
				b.AuthorName.Contains(query));
			}

			var viewModel = new SearchFormViewModel
			{
				Books = books,
				SearchTearms = query,
			};

			return View(viewModel);
		}


		//.......................... ;;........................................








		[Authorize]
		public ActionResult DeleteBookFromFavourite(int id)
		{
			var userId = User.Identity.GetUserId();

			var favouriteBookList = _context.FavouriteBookLists.Single(e => e.Id == id && e.ApplicationUserId == userId);

			if (favouriteBookList == null)
				return View("NotFound");

			_context.FavouriteBookLists.Remove(favouriteBookList);
			_context.SaveChanges();

			var viewModel = _context.FavouriteBookLists.ToList();

			return RedirectToAction("Index", "Home");
		}


//......................... ;;;;;..............................

	}
}