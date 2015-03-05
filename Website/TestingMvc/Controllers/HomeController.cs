using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.Web.Routing;

namespace TestingMvc
{
    public class HomeController : Controller
    {
		private database.TestingContext db;

		public HomeController()
		{
			db = new database.TestingContext();
		}

        public ActionResult Index(HomeVM vm)
        {
			vm.UpdateFields(db);
            return View(vm);
        }

		[HttpPost]
		public ActionResult Login(HomeVM vm)
		{
			if (vm.LoginInfo != null && ModelState.IsValid)
			{
				if (Membership.ValidateUser(vm.LoginInfo.Username, vm.LoginInfo.Password))
				{
					FormsAuthentication.SetAuthCookie(vm.LoginInfo.Username, false);
					Response.AppendCookie(new HttpCookie("FlashMessage", "Logged In.") {
						Expires = DateTime.Now.AddMinutes(1)
					});
					if (!String.IsNullOrWhiteSpace(vm.LoginInfo.ReturnUrl))
					{
						return Redirect(vm.LoginInfo.ReturnUrl);
					}
					return RedirectToAction("Index");
				}
				else
				{
					ModelState.AddModelError("LoginInfo.Password", "Invalid username and/or password entered.");
				}
			}
			vm.UpdateFields(db);
			return View("Index", vm);
		}

		public ActionResult Logout()
		{
			FormsAuthentication.SignOut();
			return RedirectToAction("Index");
		}

		[Authorize(Roles="admin")]
		public ActionResult AddUser(UserVM vm)
		{
			if (Request.HttpMethod == "POST")
			{
				if(ModelState.IsValid)
				{
					var user = Membership.GetUser(vm.Username);
					if(user == null)
					{
						if(vm.PasswordsMatch())
						{
							var editUserUrl = Url.Action("EditProfile", "Home", new { Username = vm.Username });
							try
							{
								Membership.CreateUser(vm.Username, vm.NewPassword, vm.Email);
								Roles.AddUserToRoles(vm.Username, vm.UserRoles);
								Response.AppendCookie(new HttpCookie("FlashMessage", "User Added. " + editUserUrl) {
									Expires = DateTime.Now.AddMinutes(1)
								});
							}
							catch (System.Web.Security.MembershipCreateUserException)
							{
								Response.AppendCookie(new HttpCookie("FlashMessage", "error|Unable to create user") {
									Expires = DateTime.Now.AddMinutes(1)
								});
							}
						}
						else
						{
							ModelState.AddModelError("NewPassword", "Password and Confirmation do not match.");
						}
					}
					else
					{
						ModelState.AddModelError("Username", "User already exists.");
					}
				}
			}
			else
			{
				ModelState.Clear();
			}
			return View(vm);
		}

		[Authorize]
		public ActionResult EditProfile()
		{
			//Not implemented yet.
			return RedirectToAction("Index");
		}

		/// <summary>
		/// Adds the admin user. This can be manually used when first creating the website to initially add the admin user.
		/// </summary>
		/// <returns>The admin user.</returns>
		public ActionResult AddAdminUser()
		{
			var admin = Membership.GetUser("Admin");
			if (admin == null)
			{
				Membership.CreateUser("Admin", "admin", "admin@testingmvc.com");
				Response.AppendCookie(new HttpCookie("FlashMessage", "Admin user added.") {
					Expires = DateTime.Now.AddMinutes(1)
				});
			}
			return RedirectToAction("Index");
		}

		protected override void Dispose(bool disposing)
		{
			if (disposing) 
			{
				db.Dispose();
			}
			base.Dispose(disposing);
		}
    }
}
