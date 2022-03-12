using FPTBookProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FPTBookProject.Controllers
{
    public class CartController : Controller
    {
        // GET: Cart
        ApplicationDbContext db = new ApplicationDbContext();
        public ActionResult Index()
        {
            if (Session["UserName"] != null)
            {
                int condition = Convert.ToInt32(Session["Count"]);
                if (condition > 0)
                {
                    return View((List<BookCart>)Session["cart"]);
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            else
            {
                return RedirectToAction("Log_in", "Account");
            }

        }

        public ActionResult Add(BookCart book)
        {
            if (Session["cart"] == null)
            {
                List<BookCart> li = new List<BookCart>();
                book.quantity1 = 1;
                li.Add(book);
                Session["cart"] = li;
                ViewBag.cart = li.Count();
                Session["count"] = 1;
            }
            else
            {
                List<BookCart> li = (List<BookCart>)Session["cart"];
                book.quantity1 = 1;
                li.Add(book);
                Session["cart"] = li;
                ViewBag.cart = li.Count();
                Session["count"] = Convert.ToInt32(Session["count"]) + 1;
            }
            return RedirectToAction("Index", "Home");
            /*
            if (Session["UserName"] != null)
            {

            }
            else
            {
                return RedirectToAction("Log_in", "Account");
            }*/
        }

        public ActionResult DeleteItem(BookCart item)
        {

            List<BookCart> li = (List<BookCart>)Session["cart"];
            li.RemoveAll(x => x.BookName == item.BookName);
            Session["cart"] = li;
            Session["count"] = Convert.ToInt32(Session["count"]) - 1;
            return RedirectToAction("Index", "Cart");
        }

        public ActionResult MakeOrder()
        {
            if (Session["UserName"] != null)
            {
                string username = Session["UserName"].ToString();
                var user = db.Accounts.Where(x => x.UserName.Equals(username)).FirstOrDefault();

                return View(user);

            }
            else
            {
                return RedirectToAction("Log_in", "Account");
            }
        }

        [HttpPost]
        public ActionResult MakeOrder(string name, string address, int phone, int total)
        {
            if (Session["UserName"] == null)
            {
                return HttpNotFound();
            }
            else
            {
                if (ModelState.IsValid)
                {
                    Order order = new Order();
                    var user = Session["UserName"].ToString();
                    order.UserName = user;
                    order.OrderDate = DateTime.Now;
                    order.TotalPrice = Convert.ToInt32(Session["sum"]);
                    order.Addressdilivery = address;
                    db.Orders.Add(order);
                    db.SaveChanges();

                    List<BookCart> li = (List<BookCart>)Session["cart"];
                    var order1 = db.Orders.OrderByDescending(x => x.OrderId).FirstOrDefault();
                    foreach (var item in li)
                    {
                        Orderdetail detail = new Orderdetail();
                        detail.BookId = item.BookId;
                        detail.OrderId = order1.OrderId;
                        detail.Price = item.Price;
                        detail.Quantity = item.quantity1;

                        db.Orderdetails.Add(detail);
                        db.SaveChanges();

                        var book = db.Books.Where(x => x.BookId == item.BookId).FirstOrDefault();
                        book.Quantity = book.Quantity - item.quantity1;
                        db.SaveChanges();
                    }
                    Session.Clear();
                    Session["UserName"] = user;
                    return RedirectToAction("Index", "Home");
                }

            }
            return RedirectToAction("Index", "Home");

        }
    }
}