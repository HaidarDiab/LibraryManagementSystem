using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace LibraryManagementSystem.Models
{

	public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
	{

		public DbSet<Profile> Profiles { get; set; }

		public DbSet<Book> Books { get; set; }

		public DbSet<BooksCategory> BooksCategories { get; set; }

		public DbSet<Evaluation> Evaluations { get; set; }

		public DbSet<FavouriteBookList> FavouriteBookLists { get; set; }

		public DbSet<Post> Posts { get; set; }

		public DbSet<Comment> Comments { get; set; }

		public DbSet<LikeOnPost> LikeOnPosts { get; set; }

		public DbSet<Notification> Notifications { get; set; }

		public DbSet<UserNotification> UserNotifications { get; set; }


		public ApplicationDbContext()
			: base("DefaultConnection", throwIfV1Schema: false)
		{
		}

		public static ApplicationDbContext Create()
		{
			return new ApplicationDbContext();
		}
	}
}