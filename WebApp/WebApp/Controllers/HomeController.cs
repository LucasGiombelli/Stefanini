using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;
using WebApp.Models;

namespace WebApp.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View("Login");
        }

        public ActionResult Login()
        {
            return View("Login");
        }

        [HttpPost]
        public ActionResult Login(FormCollection collection)
        {
            var email = Request.Form["email"];
            var password = Request.Form["password"];

            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
                return View("LoginError");

            var context = new WebAppContext();
            var user = context.Users.Where(u => u.Email == email).FirstOrDefault();

            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
                return View("LoginError");

            var encryptedPassword = EncryptPassword(password);

            if (string.Compare(user.Password, encryptedPassword, true) == 0)
            {
                var dummySession = new DummySession() { UserID = user.ID.ToString() };

                TempData["DummySession"] = dummySession;

                if(user.IsAdmin)
                    return RedirectToAction("Index", "AdminCustomerList");

                return RedirectToAction("Index", "CustomerList");
            }

            return View("LoginError");
        }

        private string EncryptPassword(string password)
        {
            using (MD5 md5 = MD5.Create())
            {
                var computedHash = md5.ComputeHash(Encoding.UTF8.GetBytes(password));

                var sb = new StringBuilder();
                for (int i = 0; i < computedHash.Length; i++)
                    sb.Append(computedHash[i].ToString("x2"));

                return sb.ToString();
            }
        }
    }
}