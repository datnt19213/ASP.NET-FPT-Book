using FPTBookProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FPTBookProject.Controllers
{
    public class ManageCategoryController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: ManageCategory
        public ActionResult Index()
        {
            if (Session["UserName"] == "Admin" && Session["UserName"] != null)
            {
                var category = db.Categories.ToList();
                return View(category);
            }
            else
            {
                return RedirectToAction("Log_in", "Account");
            }
        }

        public ActionResult AddCategory()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddCategory(Category category)
        {
            if (ModelState.IsValid)
            {
                var check = db.Categories.FirstOrDefault(x => x.CategoryName.Equals(category.CategoryName));
                if (check == null)
                {
                    db.Categories.Add(category);
                    db.SaveChanges();
                    return RedirectToAction("Index", "ManageCategory");
                }
                else
                {
                    ViewBag.Error = "This Category is already exist";
                    return View();
                }
            }
            return View("AddCategory");
        }


        public ActionResult EditCategory(int? id)
        {
            Category category = db.Categories.Find(id);
            if (category == null)
            {
                return HttpNotFound();
            }
            return View(category);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditCategory(Category obj)
        {
            Category tmp = db.Categories.Find(obj.CategoryId);
            if (tmp != null)
            {
                tmp.CategoryName = obj.CategoryName;
                tmp.Description = obj.Description;
            }
            db.SaveChanges();
            return RedirectToAction("Index", "ManageCategory");
        }

        public ActionResult DeleteCategory(int? id)
        {
            Category tmp = db.Categories.ToList().Find(x => x.CategoryId == id);
            if (tmp != null)
            {
                db.Categories.Remove(tmp);
                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }

    }
}