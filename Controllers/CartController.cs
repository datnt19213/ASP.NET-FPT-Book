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
        private const string carts = "cart";
        // GET: Cart
        ApplicationDbContext db = new ApplicationDbContext();
        public ActionResult Index()
        {
            var cart = Session[carts];
            if (Session["UserName"] != null)
            {
                int condition = Convert.ToInt32(Session["Count"]);
                if (condition > 0)
                {
                    return View((List<BookCart>)cart);
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

        public ActionResult CheckoutNotify()
        {
            return View();
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
            li.RemoveAll(x => x.BookId == item.BookId);
            Session["cart"] = li;
            Session["count"] = Convert.ToInt32(Session["count"]) - item.quantity1;
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
        [ValidateAntiForgeryToken]
        public ActionResult MakeOrder(string address)
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

                    //Book bk = new Book();
                    List<BookCart> li = (List<BookCart>)Session["cart"];
                    var order1 = db.Orders.OrderByDescending(x => x.OrderId).FirstOrDefault();
                    /*
                    for (int i= 0; i < li.Count; i++)
                    {
                        Orderdetail detail = new Orderdetail();
                        detail.BookId = li[i].BookId;
                        detail.OrderId = order1.OrderId;
                        detail.Price = li[i].Price;
                        detail.Quantity = li[i].quantity1;

                        db.Orderdetails.Add(detail);
                        db.SaveChanges();

                        /*if(bk.BookId == li[i].BookId && ){
                            db.Books.Remove(li[i].BookId);
                        }*/
                    /*
                        var book = db.Books.Where(x => x.BookId == li[i].BookId).FirstOrDefault();
                        book.Quantity = book.Quantity - li[i].quantity1;
                        
                        db.SaveChanges();

                    }
                    */

                    foreach (var item in li)
                    {
                        Orderdetail detail = new Orderdetail();
                        detail.BookId = item.BookId;
                        detail.OrderId = order1.OrderId;
                        detail.Price = item.Price;
                        detail.Quantity = item.quantity1;

                        db.Orderdetails.Add(detail);

                        /*if(bk.BookId == li[i].BookId && ){
                            db.Books.Remove(li[i].BookId);
                        }*/
                        /*
                        var book = db.Books.Where(x => x.BookId == item.BookId).FirstOrDefault();
                        book.Quantity = book.Quantity - item.quantity1;
                        */
                    }
                    
                    db.SaveChanges();


                    Session.Clear();
                    Session["UserName"] = user;
                    Session["success"] = $"Order successfull- total:{order.TotalPrice}" +
                                 $" The book will delivery to address:{order.Addressdilivery} next 5 days.";
                    return RedirectToAction("CheckoutNotify", "Cart");
                }
            }
            return RedirectToAction("CheckoutNotify", "Cart");

        }

    }
}