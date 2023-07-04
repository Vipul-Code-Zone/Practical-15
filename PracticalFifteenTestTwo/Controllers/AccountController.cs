using PracticalFifteenTestTwo.DatabaseContext;
using System.Linq;
using System.Web.Mvc;
using System.Web.Security;

namespace PracticalFifteenTestTwo.Controllers
{
	public class AccountController : Controller
	{
		readonly PracrticalFifteenTestTwoEntities1 db = new PracrticalFifteenTestTwoEntities1();
		public ActionResult Login()
		{
			if (User.Identity.IsAuthenticated)
			{
				return RedirectToAction("Index", "Home");
			}
			return View(new User());
		}
		[HttpPost]
		public ActionResult Login(User user)
		{
			var temp = db.Users.FirstOrDefault(x => x.Name == user.Name);
			if (temp.Password == user.Password)
			{
				FormsAuthentication.SetAuthCookie(user.Name, true);
				return RedirectToAction("Index", "Home");
			}
			ModelState.AddModelError("", "Wrong Id and password");
			return View(user);
		}

		public ActionResult Logout()
		{
			FormsAuthentication.SignOut();
			return RedirectToAction("Login");
		}

		public ActionResult Register()
		{
			return View();
		}
		[HttpPost]
		public ActionResult Register(User user)
		{
			if (ModelState.IsValid)
			{

				db.Users.Add(user);
				db.SaveChanges();
				TempData["register"] = "Register Successfully!";
				return RedirectToAction("Login");
			}
			return View();
		}
	}
}