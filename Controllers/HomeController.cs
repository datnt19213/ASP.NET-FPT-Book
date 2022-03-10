using FPTBookProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FPTBookProject.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult Index()
        {
            var book = db.Books.ToList();
            return View(book);
        }

        public ActionResult BestSellers()
        {
            var book = db.Books.ToList();
            ViewBag.Message = "Best sellers book page.";

            return View(book);
        }

        public ActionResult NewReleased()
        {
            var book = db.Books.ToList();
            ViewBag.Message = "New releases books page.";

            return View(book);
        }

        public ActionResult Business()
        {
            var book = db.Books.ToList();
            ViewBag.Message = "Business books page.";

            return View(book);
        }

        public ActionResult Computing()
        {
            var book = db.Books.ToList();
            ViewBag.Message = "Computing books page.";

            return View(book);
        }

        public ActionResult Marketing()
        {
            var book = db.Books.ToList();
            ViewBag.Message = "Marketing books page.";

            return View(book);
        }
    }
}