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

        /*
        [HttpPost]
        public ActionResult Index(string searchingstring)
        {
            List<Book> data = new List<Book>();
            data = db.Books.Where(x => x.BookName.Contains(searchingstring)).ToList();

            if (data == null)
            {
                return RedirectToAction("Index");
            }

            return View(data);
        }*/

        public ActionResult Search()
        {
            var book = db.Books.ToList();
            return View(book);
        }

        [HttpPost]
        public ActionResult Search(string searchingstring)
        {
            List<Book> data = new List<Book>(db.Books.Where(x => x.BookName.Contains(searchingstring)).ToList());


            

            if (data == null)
            {
                return RedirectToAction("SearchNotify");

            }
            else
            {
                return View(data);
            }
        }

        public ActionResult SearchNotify()
        {
            return View();
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

            List<Book> data = new List<Book>();
            data = db.Books.Where(x => x.Category.CategoryName.Contains("Business")).ToList();


            return View(data);
        }

        public ActionResult Computing()
        {
            var book = db.Books.ToList();
            ViewBag.Message = "Computing books page.";

            List<Book> data = new List<Book>();
            data = db.Books.Where(x => x.Category.CategoryName.Contains("Computing")).ToList();

            return View(data);
        }

        public ActionResult Marketing()
        {
            var book = db.Books.ToList();
            ViewBag.Message = "Marketing books page.";

            List<Book> data = new List<Book>();
            data = db.Books.Where(x => x.Category.CategoryName.Contains("Marketing")).ToList();

            return View(data);
        }
    }
}