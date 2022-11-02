using LibraryManagementSystem.Models;
using LibraryManagementSystem.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UglyToad.PdfPig;
using UglyToad.PdfPig.XObjects;

namespace LibraryManagementSystem.Controllers
{
	[Authorize(Roles = "Admin")]
	public class AdminController : Controller
	{
		private ApplicationDbContext _context;

		public AdminController()
		{
			_context = new ApplicationDbContext();
		}



		[HttpGet]
		public ActionResult AdminDashboard()
		{
			return View();
		}


		[HttpGet]
		public ActionResult ManageUser()
		{
			var viewModel = new AdminViewModel
			{
				ApplicationUsers = _context.Users.ToList(),
				Profiles = _context.Profiles.ToList(),
			};

			return View(viewModel);
		}


		[HttpGet]
		public ActionResult ManageCategory()
		{
			var categories = _context.BooksCategories.ToList();

			return View(categories);
		}


		[HttpGet]
		public ActionResult AddCategory()
		{
			var category = new BooksCategory();

			return View(category);
		}


		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult AddCategory(BooksCategory category, HttpPostedFileBase file)
		{
			if (!ModelState.IsValid)
				return View("AddCategory", category);

			if (_context.BooksCategories.Any(c => c.Id == category.Id || c.Name.Equals(category.Name)))
			{
				ViewBag.Result = "موجود مسبقاً";

				return View();
			}


			if (file != null && file.ContentLength > 0)
			{
				string path = Path.Combine(Server.MapPath("~/images"), Path.GetFileName(file.FileName));
				file.SaveAs(path);

				var bookCategory = new BooksCategory
				{
					Name = category.Name,
					ImageName = file.FileName,
					ImagePath = path,
				};

				_context.BooksCategories.Add(bookCategory);
			}

			else
			{
				var bookCategory = new BooksCategory
				{
					Name = category.Name,
					ImageName = null,
					ImagePath = null,
				};

				_context.BooksCategories.Add(bookCategory);
			}

			_context.SaveChanges();


			ViewBag.Result = "تمت عملية الإضافة بنجاح";

			return View();
		}


		[HttpGet]
		public ActionResult EditCategory(int id)
		{
			var category = _context.BooksCategories.SingleOrDefault(c => c.Id == id);

			return View(category);
		}


		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult EditCategory(BooksCategory category, HttpPostedFileBase file)
		{

			if (!ModelState.IsValid)
				return View("EditCategory", category);

			var categoryInDb = _context.BooksCategories.SingleOrDefault(c => c.Id == category.Id);

			if (categoryInDb == null)
				return View("NotFound");


			if (file != null && file.ContentLength > 0)
			{
				string path = Path.Combine(Server.MapPath("~/images"), Path.GetFileName(file.FileName));

				file.SaveAs(path);

				categoryInDb.Name = category.Name;
				categoryInDb.ImageName = file.FileName;
				categoryInDb.ImagePath = path;
			}

			else
			{
				categoryInDb.Name = category.Name;
				categoryInDb.ImageName = categoryInDb.ImageName;
				categoryInDb.ImagePath = categoryInDb.ImagePath;
			}

			_context.SaveChanges();

			ViewBag.Result = "تمت عملية التعديل بنجاح";

			return View();
		}


		public ActionResult DeleteCategory(int id)
		{
			var categories = _context.BooksCategories.ToList();
			var category = _context.BooksCategories.SingleOrDefault(c => c.Id == id);


			if (category == null)
			{
				return View("NotFound");
			}

			string name = category.Name;
			_context.BooksCategories.Remove(category);
			_context.SaveChanges();

			ViewBag.Result =  $"تمت عملية حذف {name} بنجاح";

			return View("ManageCategory", categories);

		}


		[HttpGet]
		public ActionResult ManageBook()
		{
			var books = _context.Books.ToList();

			return View(books);
		}



		[HttpGet]
		public ActionResult AddBook()
		{
			var viewModel = new BookFormViewModel
			{
				BooksCategories = _context.BooksCategories.ToList(),
			};

			return View(viewModel);
		}



		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult AddBook(Book bookModel, HttpPostedFileBase file)
		{

			var viewModel = new BookFormViewModel
			{
				BooksCategories = _context.BooksCategories.ToList(),
				Id = bookModel.Id,
				Title = bookModel.Title,
				AuthorName = bookModel.AuthorName,
				ReleaseDate = bookModel.ReleaseDate,
				Summary = bookModel.Summary,
				BooksCtegoryId = bookModel.BooksCtegoryId,
				CoverImageName = bookModel.CoverImageName,
				CoverImagePath = bookModel.CoverImagePath,
			};

			if (!ModelState.IsValid)
			{

				ViewBag.Result = "الرجاء التحقق من الحقول المدخلة";
				return View("AddBook", viewModel);
			}

			if (_context.Books.Any(b => b.Title == bookModel.Title && b.AuthorName == bookModel.AuthorName))
			{
				ViewBag.Result = "الكتاب موجود مسبقاً";
				return View(viewModel);
			}



			//string FileName = "";
			//if (Request.Files.Count > 0)
			//{
			//	var files = Request.Files;

			//	//iterating through multiple file collection   
			//	foreach (string str in files)
			//	{
			//		HttpPostedFileBase file = Request.Files[str] as HttpPostedFileBase;
			//		//Checking file is available to save.  
			//		if (file != null)
			//		{
			//			FileName = file.FileName;
			//			var InputFileName = Path.GetFileName(file.FileName);
			//			var ServerSavePath = Path.Combine(Server.MapPath("~/uploadedBooks/") + InputFileName);
			//			//Save file to server folder  
			//			file.SaveAs(ServerSavePath);

			//		}

			//	}
			//	return Json(FileName);
			//}
			//else
			//{
			//	return Json("No files to upload");
			//}




			try
			{
				if (file != null && file.ContentLength > 0)
				{
					string path = Path.Combine(Server.MapPath("~/uploadedBooks"), Path.GetFileName(file.FileName));
					file.SaveAs(path);



					//the path where we will save extracted images to use it in display in view
					string targetFolderPath = Server.MapPath("~/ExtractedImages");

					try
					{
						using (PdfDocument pdfDocument = PdfDocument.Open(path))
						{
							int imageCount = 1;

							var page = pdfDocument.GetPage(1);

							List<XObjectImage> images = page.GetImages().Cast<XObjectImage>().ToList();

							foreach (XObjectImage image in images)
							{
								byte[] imageRawBytes = image.RawBytes.ToArray();

								using (FileStream stream = new FileStream($"{targetFolderPath}\\{bookModel.Title}.png", FileMode.Create, FileAccess.Write))
								using (BinaryWriter writer = new BinaryWriter(stream))
								{
									writer.Write(imageRawBytes);
									writer.Flush();
								}
								imageCount++;
							}

							var book = new Book
							{
								Id = bookModel.Id,
								Title = bookModel.Title,
								AuthorName = bookModel.AuthorName,
								ReleaseDate = bookModel.ReleaseDate,
								Summary = bookModel.Summary,
								BooksCtegoryId = bookModel.BooksCtegoryId,
								CoverImageName = $"{ bookModel.Title}.png",
								CoverImagePath = targetFolderPath,
								BookFilePath = path,
							};

							_context.Books.Add(book);
						}
					}
					catch (Exception)
					{
						ViewBag.Result = "الملف لايحتوي على صور لإضافتها";
					}
				}
				else
				{
					var book = new Book
					{
						Id = bookModel.Id,
						Title = bookModel.Title,
						AuthorName = bookModel.AuthorName,
						ReleaseDate = bookModel.ReleaseDate,
						Summary = bookModel.Summary,
						BooksCategory = bookModel.BooksCategory,
						BooksCtegoryId = bookModel.BooksCtegoryId,
						CoverImageName = null,
						CoverImagePath = null,
					    BookFilePath = null,
					};

					_context.Books.Add(book);
				}

			}

			catch (DbEntityValidationException ex)
			{
				Console.WriteLine(ex.Message);
			}


			_context.SaveChanges();

			ViewBag.Result = "تمت عملية الإضافة بنجاح";

			return View("AddBook", viewModel);

		}



		[HttpGet]
		public ActionResult EditBook(int id)
		{
			var book = _context.Books.SingleOrDefault(b => b.Id == id);

			var viewModel = new BookFormViewModel
			{
				Book = book,
				BooksCategories = _context.BooksCategories.ToList(),
			};

			return View(viewModel);
		}


		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult EditBook(Book book, HttpPostedFileBase file)
		{

			var viewModel = new BookFormViewModel
			{
				Book = book,
				BooksCategories = _context.BooksCategories.ToList(),
			};


			var bookInDb = _context.Books.SingleOrDefault(b => b.Id == book.Id);

			if (bookInDb == null)
				return View("NotFound");

			if (_context.Books.Any(b => b.Title == book.Title && b.AuthorName == book.AuthorName))
			{
				ViewBag.Result = "الكتاب موجود مسبقاً";
				return View("EditBook", viewModel);
			} 
			


			if (file != null && file.ContentLength > 0)
			{
				string path = Path.Combine(Server.MapPath("~/uploadedBooks"), Path.GetFileName(file.FileName));

				file.SaveAs(path);

				bookInDb.Id = book.Id;
				bookInDb.Title = book.Title;
				bookInDb.AuthorName = book.AuthorName;
				bookInDb.ReleaseDate = book.ReleaseDate;
				bookInDb.Summary = book.Summary;
				bookInDb.BooksCategory = book.BooksCategory;
				bookInDb.BooksCtegoryId = book.BooksCtegoryId;
				bookInDb.CoverImageName = file.FileName;
				bookInDb.CoverImagePath = path;
			}

			else
			{
				bookInDb.Id = book.Id;
				bookInDb.Title = book.Title;
				bookInDb.AuthorName = book.AuthorName;
				bookInDb.ReleaseDate = book.ReleaseDate;
				bookInDb.Summary = book.Summary;
				bookInDb.BooksCategory = book.BooksCategory;
				bookInDb.BooksCtegoryId = book.BooksCtegoryId;
				bookInDb.CoverImageName = bookInDb.CoverImageName;
				bookInDb.CoverImagePath = bookInDb.CoverImageName;

			}

			_context.SaveChanges();

			ViewBag.Result = "تمت عملية التعديل بنجاح";

			return View("EditBook", viewModel);
		}


		public ActionResult DeleteBook(int id)
		{
			var books = _context.Books.ToList();
			var book = _context.Books.SingleOrDefault(c => c.Id == id);


			if (book == null)
			{
				return View("NotFound");
			}

			string name = book.Title;

			_context.Books.Remove(book);


			if (System.IO.File.Exists(book.BookFilePath))
			{
				System.IO.File.Delete(book.BookFilePath);
			}

			string imageFullPath = book.CoverImagePath + "\\" + book.CoverImageName;

			if (System.IO.File.Exists(imageFullPath))
			{
				System.IO.File.Delete(imageFullPath);

			}
			_context.SaveChanges();

			ViewBag.Result = $"تمت عملية حذف {name} بنجاح";

			return View("ManageBook", books);
		}



	   
	}
}