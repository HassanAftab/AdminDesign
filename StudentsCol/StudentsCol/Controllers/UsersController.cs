using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using StudentsCol.Models;

namespace StudentsCol.Controllers
{
    [Authorize]
    public class UsersController : Controller
    {
        //
        // GET: /Users/
        StudentComEntities db = new StudentComEntities();
        public ActionResult Index()
        {
            //get data from database using LINQ
            var user = (from u in db.Users
                        select u).ToList();
            return View(user);
        }
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(FormCollection form )
        {
            string username = form["username"].ToString();
            string password = form["password"].ToString();
            string confirm = form["confirm"].ToString();
            string email = form["email"].ToString();
            if (password != confirm)
            {
                TempData["Message"] = "Password and confirm password does not match";
            }
            

            var usr = (from u in db.Users
                       where u.Username == username
                       select u).FirstOrDefault();
            if (usr == null)
            {
                //Save values / User
                User us = new User();
                us.Username = username;
                us.Password = password;
                us.Email = email;
                us.IsActive = true;
                db.Users.Add(us);
                db.SaveChanges();
                //changes saved
                return RedirectToAction("Index");
            }
            else
            {
                TempData["message"] = "User already exists";
            }

            return View();
        }
        public ActionResult Edit(int id)
        {
            var user = db.Users.Where(x => x.UserID == id).FirstOrDefault();
            return View(user);
        }
        [HttpPost]
        public ActionResult Edit(FormCollection form)
        {
            int id = Convert.ToInt32(form["UserId"].ToString());
            var user = db.Users.Where(x => x.UserID == id).FirstOrDefault();
            if (user != null)
            {
                string username = form["username"].ToString();
                string password = form["password"].ToString();
                string confirm = form["confirm"].ToString();
                string email = form["email"].ToString();
                bool ac = false;
                string IsActive = form["IsActive"];
                if (IsActive == "false")
                {
                    ac = false;
                }
                else
                {
                    ac = true;
                }
            if (password != confirm)
            {
                TempData["Message"] = "Password and confirm password does not match";
                return View();
            }
                user.Password=password;
                user.Email = email;
                user.IsActive=ac;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(user);
        }

    }
}
