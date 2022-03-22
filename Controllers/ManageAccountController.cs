using FPTBookProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace FPTBookProject.Controllers
{
    public class ManageAccountController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: ManageAccount
        public ActionResult Index()
        {
            if (Session["UserName"] == "Admin" && Session["UserName"] != null)
            {
                var account = db.Accounts.ToList();

                return View(account);
            }
            else
            {
                return RedirectToAction("Log_in", "Account");
            }

        }


        public ActionResult RegisterAdmin()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult RegisterAdmin(Account account)
        {
            if (ModelState.IsValid)
            {
                var check = db.Accounts.FirstOrDefault(x => x.Email == account.Email);
                if (check == null)
                {
                    account.Password = GetMD5(account.Password);
                    account.ConfirmPass = GetMD5(account.ConfirmPass);
                    db.Accounts.Add(account);
                    db.SaveChanges();
                    return RedirectToAction("Index", "ManageAccount");
                }
                else
                {
                    ViewBag.Error = "This Email is already exist";
                    return View();
                }
            }
            return View("RegisterAdmin");
        }


        public ActionResult EditAccount(string userName)
        {
            Account account = db.Accounts.Find(userName);
            if (account == null)
            {
                return HttpNotFound();
            }
            return View(account);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditAccount([Bind(Include = "UserName,FullName,Password,ConfirmPass,PhoneNumber,Birthday,Address,Email,StatusCode")] Account obj)
        {
            if (ModelState.IsValid)
            {
                Account tmp = db.Accounts.ToList().Find(x => x.UserName == obj.UserName); //find the customer in a list have the same ID with the ID input
                if (tmp.Password != obj.Password)  //if find out the customer
                {
                    tmp.ConfirmPass = GetMD5(obj.ConfirmPass);
                    tmp.Password = GetMD5(obj.Password);
                }
                tmp.UserName = obj.UserName;
                tmp.FullName = obj.FullName;
                tmp.PhoneNumber = obj.PhoneNumber;
                tmp.Email = obj.Email;
                tmp.Address = obj.Address;
                tmp.StatusCode = obj.StatusCode;

                db.SaveChanges();
                return RedirectToAction("Index", "ManageAccount");
            }
            return View("EditAccount");
        }

        public ActionResult DeleteAccount(string userName)
        {
            Account tmp = db.Accounts.ToList().Find(x => x.UserName == userName);
            if (tmp != null)
            {
                db.Accounts.Remove(tmp);
                db.SaveChanges();
            }
            return RedirectToAction("Index");
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

    }
}