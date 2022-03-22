using FPTBookProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FPTBookProject.Controllers
{
    public class ManageAuthorController : Controller
    {
        // GET: ManageAuthor
        private ApplicationDbContext db = new ApplicationDbContext();
        public ActionResult Index()
        {
            if (Session["UserName"] == "Admin" && Session["UserName"] != null)
            {
                var author = db.Authors.ToList();
                return View(author);
            }
            else
            {
                return RedirectToAction("Log_in", "Account");
            }
        }

        public ActionResult AddAuthor()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddAuthor(Author author)
        {
            if (ModelState.IsValid)
            {
                var check = db.Authors.FirstOrDefault(x => x.AuthorName.Equals(author.AuthorName));
                if (check == null)
                {
                    db.Authors.Add(author);
                    db.SaveChanges();
                    return RedirectToAction("Index", "ManageAuthor");
                }
                else
                {
                    ViewBag.Error = "This Author is already exist";
                    return View();
                }
            }
            return View("AddAuthor");
        }


        public ActionResult EditAuthor(int? id)
        {
            Author author = db.Authors.Find(id);
            if (author == null)
            {
                return HttpNotFound();
            }
            return View(author);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditAuthor(Author obj)
        {
            Author tmp = db.Authors.ToList().Find(x => x.AuthorId == obj.AuthorId);
            if (tmp != null)
            {
                tmp.AuthorName = obj.AuthorName;
                tmp.Description = obj.Description;
            }
            db.SaveChanges();
            return RedirectToAction("Index", "ManageAuthor");
        }

        public ActionResult DeleteAuthor(int? id)
        {
            Author tmp = db.Authors.ToList().Find(x => x.AuthorId == id);
            if (tmp != null)
            {
                db.Authors.Remove(tmp);
                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }

    }
}