using Flashcard_web_app.Models;
using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Flashcard_web_app.Controllers
{
    public class AccountController : Controller
    {
        private Flashcard_web_appContext db = new Flashcard_web_appContext();

        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                MD5 md5Hash = MD5.Create();
                string HashedPassword = PasswordHasher(md5Hash, model.Password);
                QuizzyUser user = db.QuizzyUsers.Where(a => a.Username.Equals(model.Username) && a.Password.Equals(HashedPassword)).FirstOrDefault();
                if (user != null)
                {
                    Session["Username"] = user.Username;
                    Session["UserID"] = user.ID;
                    Session.Add("CurrentUser", user);
                     
                    string username = user.Username;
                    FormsAuthentication.SetAuthCookie(username, false);
                    return RedirectToAction("Dashboard");
                }
                else
                    ModelState.AddModelError(string.Empty, "Username and/or password is incorrect.");
            }
            return View();
        }

        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();
            Session.Abandon();

            // Clear authentication cookie
            HttpCookie rFormsCookie = new HttpCookie(FormsAuthentication.FormsCookieName, "");
            rFormsCookie.Expires = DateTime.Now.AddYears(-1);
            Response.Cookies.Add(rFormsCookie);

            // Clear session cookie 
            HttpCookie rSessionCookie = new HttpCookie("ASP.NET_SessionId", "");
            rSessionCookie.Expires = DateTime.Now.AddYears(-1);
            Response.Cookies.Add(rSessionCookie);
            return RedirectToAction("Index", "Home");
        }

        [AllowAnonymous]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                MD5 md5Hash = MD5.Create();
                //Check if user with username already exists or if both passwords match
                if (db.QuizzyUsers.Where(a => a.Username == model.Username).Any() || model.Password != model.ConfirmPassword)
                {
                    ModelState.AddModelError(string.Empty, "Username is already taken or passwords do not match.");
                }
                else
                {
                    var user = new QuizzyUser() { Username = model.Username, Password = PasswordHasher(md5Hash, model.Password) };
                    db.QuizzyUsers.Add(user);
                    db.SaveChanges();

                    Session["Username"] = user.Username;
                    Session["UserID"] = user.ID;
                    Session.Add("CurrentUser", user);
                    string username = user.Username;
                    FormsAuthentication.SetAuthCookie(username, false);
                    return RedirectToAction("Dashboard");
                }
            }
            return View(model);
        }

        static string PasswordHasher(MD5 md5Hash, string input)
        {

            // Convert the input string to a byte array and compute the hash.
            byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));

            // Create a new Stringbuilder to collect the bytes
            // and create a string.
            StringBuilder sBuilder = new StringBuilder();

            // Loop through each byte of the hashed data 
            // and format each one as a hexadecimal string.
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }

            // Return the hexadecimal string.
            return sBuilder.ToString();
        }

        [Authorize]
        public ActionResult Dashboard()
        {
            return View(db.Decks.ToList());
        }
    }
}