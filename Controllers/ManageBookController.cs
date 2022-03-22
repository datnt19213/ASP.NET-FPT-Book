using FPTBookProject.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FPTBookProject.Controllers
{
    public class ManageBookController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();


        // GET: ManageBook
        public ActionResult Index()
        {
            if (Session["UserName"] == "Admin" && Session["UserName"] != null)
            {
                var books = db.Books.ToList();
                return View(books);
            }
            else
            {
                return RedirectToAction("Log_in", "Account");
            }

        }

        public ActionResult AddBook()
        {
            ViewBag.AuthorId = new SelectList(db.Authors, "AuthorId", "AuthorName");
            ViewBag.CategoryId = new SelectList(db.Categories, "CategoryId", "CategoryName");
            ViewBag.PublisherID = new SelectList(db.Publishers, "PublisherId", "PublisherName");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddBook(HttpPostedFileBase Img, Book book)
        {
            ViewBag.AuthorId = new SelectList(db.Authors, "AuthorId", "AuthorName");
            ViewBag.CategoryId = new SelectList(db.Categories, "CategoryId", "CategoryName");
            ViewBag.PublisherID = new SelectList(db.Publishers, "PublisherId", "PublisherName");

            if (ModelState.IsValid)
            {
                var check = db.Books.FirstOrDefault(x => x.BookName.Equals(book.BookName));
                if (check == null && Img != null && Img.ContentLength > 0)
                {
                    string pic = Path.GetFileName(Img.FileName);
                    string path = Path.Combine(Server.MapPath("~/Content/Book images"), pic);
                    Img.SaveAs(path);
                    book.Img = pic;
                    db.Books.Add(book);
                    db.SaveChanges();
                    return RedirectToAction("Index", "ManageBook");
                }
                else
                {
                    ViewBag.Error = "This Book is already exist";
                    return View();
                }
            }

            return View("AddBook");
        }


        public ActionResult EditBook(int id)
        {
            ViewBag.AuthorId = new SelectList(db.Authors, "AuthorId", "AuthorName");
            ViewBag.CategoryId = new SelectList(db.Categories, "CategoryId", "CategoryName");
            ViewBag.PublisherID = new SelectList(db.Publishers, "PublisherId", "PublisherName");
            Book book = db.Books.Find(id);
            if (book == null)
            {
                return HttpNotFound();
            }
            return View(book);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditBook(HttpPostedFileBase Img, Book book)
        {
            var editbook = db.Books.Where(x => x.BookId == book.BookId).FirstOrDefault();
            string pic = "";
            if (Img != null)
            {
                string fileName = book.Img;
                string path1 = Server.MapPath("~/Content/Book images");
                FileInfo fileinfo = new FileInfo(path1 + fileName);
                if (fileinfo.Exists)
                {
                    fileinfo.Delete();
                }
                pic = System.IO.Path.GetFileName(Img.FileName);
                string path = Path.Combine(Server.MapPath("~/Content/Book images"), Path.GetFileName(Img.FileName));
                Img.SaveAs(path);
                editbook.Img = pic.ToString();
            }
            if (ModelState.IsValid)
            {
                if (editbook == null)
                {
                    return HttpNotFound();
                }
                else
                {
                    editbook.BookName = book.BookName;
                    editbook.Price = book.Price;
                    editbook.Quantity = book.Quantity;
                    editbook.Description = book.Description;
                    editbook.AuthorId = book.AuthorId;
                    editbook.CategoryId = book.CategoryId;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            ViewBag.AuthorID = new SelectList(db.Authors, "AuthorId", "AuthorName", book.AuthorId);
            ViewBag.CategoryID = new SelectList(db.Categories, "CategoryId", "CategoryName", book.CategoryId);
            ViewBag.PublisherID = new SelectList(db.Publishers, "PublisherId", "PublisherName", book.PublisherId);
            return View("EditBook");

        }

        public ActionResult DeleteBook(int id)
        {
            Book tmp = db.Books.ToList().Find(x => x.BookId == id);
            if (tmp != null)
            {
                db.Books.Remove(tmp);
                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }




    }
}