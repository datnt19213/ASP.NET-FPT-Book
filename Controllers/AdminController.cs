using FPTBookProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FPTBookProject.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        private ApplicationDbContext db = new ApplicationDbContext();
        public ActionResult Index()
        {
            if (Session["UserName"] == "Admin" && Session["UserName"] != null)
            {
                var totalBook = db.Books.Count();
                var orderItem = db.Orders.Count();
                var totalUser = db.Accounts.Count();

                int revenue = 0;
                foreach (var x in db.Orders)
                {
                    revenue = Convert.ToInt32(x.TotalPrice) + revenue;
                }

                ViewBag.TotalBook = totalBook;
                ViewBag.OrderItem = orderItem;
                ViewBag.TotalUser = totalUser;
                ViewBag.Revenue = revenue;

                return View();

            }
            else
            {
                return RedirectToAction("Log_in", "Account");
            }
        }

    }
}