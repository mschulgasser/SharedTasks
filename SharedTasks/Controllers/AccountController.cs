using SharedTasks.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace SharedTasks.Controllers
{
    public class AccountController : Controller
    {
        // GET: Account
        public ActionResult LogIn()
        {
            return View();
        }
        [HttpPost]
        public ActionResult LogIn(string email, string password)
        {
            var manager = new UserManager(Properties.Settings.Default.ConStr);
            User u = manager.Login(email, password);
            if (u != null)
            {
                FormsAuthentication.SetAuthCookie(u.Id.ToString(), true);
                return Redirect("/home/index");
            }
            return Redirect("/account/login");
        }
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(string firstName, string lastName, string email, string password)
        {
            var manager = new UserManager(Properties.Settings.Default.ConStr);
            int userId = manager.AddUser(firstName, lastName, email, password);
            FormsAuthentication.SetAuthCookie(userId.ToString(), true);
            return Redirect("/home/index");
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return Redirect("/");
        }
    }
}