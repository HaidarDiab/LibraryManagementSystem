using LibraryManagementSystem.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Owin;
using System;

[assembly: OwinStartupAttribute(typeof(LibraryManagementSystem.Startup))]
namespace LibraryManagementSystem
{
	public partial class Startup
	{
		ApplicationDbContext _context = new ApplicationDbContext();


		public void Configuration(IAppBuilder app)
		{
			ConfigureAuth(app);
			createRolesandUsers();

		}

		private void createRolesandUsers()
		{

			var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(_context));
			var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(_context));

			if (!roleManager.RoleExists("Admin"))
			{
				// first we create Admin rool    
				IdentityRole role = new IdentityRole();
				role.Name = "Admin";
				roleManager.Create(role);


				//Here we create a Admin super user who will maintain the website
		ApplicationUser user = new ApplicationUser();

				user.Email = "librarymailuserad@gmail.com";
				user.UserName = "librarymailuserad@gmail.com";

				string userPWD = "Admin12345@";

				var chkUser = userManager.Create(user, userPWD);

				//Add default User to Role Admin    
				if (chkUser.Succeeded)
				{
					userManager.AddToRole(user.Id, "Admin");
				}

				else
				{
					var e = new Exception("Couldnt add default user.");
					var enumerator = chkUser.Errors.GetEnumerator();
					foreach (var error in chkUser.Errors)
					{
						e.Data.Add(enumerator.Current, error);
					}
				}
			}


			// creating Creating Normal User role     
			if (!roleManager.RoleExists("NormalUser"))
			{
				var role = new IdentityRole();
				role.Name = "NormalUser";
				roleManager.Create(role);

			}
		}
	}
}
