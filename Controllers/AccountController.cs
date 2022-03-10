using System;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using FPTBookProject.Models;
using System.Security.Cryptography;
using System.Text;

namespace FPTBookProject.Controllers
{
    public class AccountController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public AccountController()
        {
        }
        // GET: Account
        public ActionResult Index()
        {
            if (Session["UserName"] != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Log_in");
            }
        }

        public ActionResult Register()
        {
            ViewBag.Message = "Your Register page.";

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register([Bind(Include = "UserName,FullName,Password,ConfirmPass,PhoneNumber,Address,Email")] Account account)
        {
            if (ModelState.IsValid)
            {
                var check = db.Accounts.FirstOrDefault(x => x.UserName == account.UserName);
                if (check == null)
                {
                    account.Password = GetMD5(account.Password);
                    account.ConfirmPass = GetMD5(account.ConfirmPass);
                    account.StatusCode = 0;
                    db.Accounts.Add(account);
                    db.SaveChanges();
                    return RedirectToAction("Log_in");
                }
                else
                {
                    ViewBag.Error = "Register fail";
                    return View();
                }
            }
            ViewBag.Error = "Register fail";
            return View("Register");
        }


        public ActionResult Log_in()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Log_in(string userName, string Password)
        {
            if (ModelState.IsValid)
            {
                var _password = GetMD5(Password);
                var data = db.Accounts.Where(s => s.UserName.Equals(userName) && s.Password.Equals(_password)).ToList();
                if (data.Count() > 0)
                {
                    if (data.FirstOrDefault().StatusCode == 0)
                    {
                        //add session for the customer and driecrt to the index page
                        Session["UserName"] = data.FirstOrDefault().UserName;
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        Session["UserName"] = data.FirstOrDefault().UserName;
                        Session["UserName"] = "Admin";
                        return RedirectToAction("Index", "Admin");

                    }
                }
                else
                {
                    ViewBag.error = "Login failed";
                    return View("Log_in");
                }
            }
            return View();
        }


        //Logout we claer tghe user session
        public ActionResult Log_out()
        {
            Session["UserName"] = null;//remove session
            return RedirectToAction("Index", "Home");
        }

        public ActionResult Edit_Infor()
        {
            var user = Session["UserName"];
            Account obj = db.Accounts.ToList().Find(x => x.UserName.Equals(user));
            if (obj == null)
            {
                return HttpNotFound();
            }
            return View(obj);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit_Infor(Account obj)
        {
            if (ModelState.IsValid)
            {
                Account tmp = db.Accounts.ToList().Find(x => x.UserName == obj.UserName); //find the customer in a list have the same ID with the ID input 
                if (/*tmp != null &&*/ tmp.Password != obj.Password)  //if find out the customer
                {
                    tmp.Password = GetMD5(obj.Password);
                    tmp.ConfirmPass = GetMD5(obj.ConfirmPass);
                }
                tmp.UserName = obj.UserName;
                tmp.FullName = obj.FullName;
                tmp.PhoneNumber = obj.PhoneNumber;
                tmp.Email = obj.Email;
                tmp.Address = obj.Address;
                tmp.StatusCode = 0;

                db.SaveChanges();
                return RedirectToAction("Index", "Home");
            }
            return View("Edit_Infor");
        }

        //create a string MD5 to hash the password
        public static string GetMD5(string str)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] fromData = Encoding.UTF8.GetBytes(str);
            byte[] targetData = md5.ComputeHash(fromData);
            string byte2String = null;

            for (int i = 0; i < targetData.Length; i++)
            {
                byte2String += targetData[i].ToString("x2");

            }
            return byte2String;
        }

        internal class ChallengeResult : ActionResult
        {
            private string provider;
            private string v1;
            private string v2;

            public ChallengeResult(string provider, string v1, string v2)
            {
                this.provider = provider;
                this.v1 = v1;
                this.v2 = v2;
            }

            public override void ExecuteResult(ControllerContext context)
            {
                throw new NotImplementedException();
            }
        }
    }
}