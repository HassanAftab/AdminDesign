using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using StudentsCol.Models;
using System.Web.Security;

namespace StudentsCol.Controllers
{
    public class AccountController : Controller
    {
        //
        // GET: /Account/
        StudentComEntities db = new StudentComEntities();
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(FormCollection form)
        {
            string username = form["username"].ToString();
            string password = form["password"].ToString();

            var usr = (from u in db.Users
                       where u.Username == username && u.Password == password && u.IsActive==true
                       select u).FirstOrDefault();
            if (usr != null)
            {
                //Create seession/ token for loged in user
                FormsAuthentication.SetAuthCookie(usr.Username, false);
                return RedirectToAction("Index", "Dashboard");
            }
            TempData["Message"] = "Username and password is wrong";
            return View();
        }
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login");
        }

    }
}
