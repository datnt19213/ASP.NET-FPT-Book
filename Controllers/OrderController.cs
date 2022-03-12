using FPTBookProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FPTBookProject.Controllers
{
    public class OrderController : Controller
    {
        // GET: Order
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: ManageCategory
        public ActionResult Index()
        {
            if (Session["UserName"] == "Admin" && Session["UserName"] != null)
            {
                var orders = db.Orders.ToList();
                return View(orders);
            }
            else
            {
                return RedirectToAction("Log_in", "Account");
            }
        }

        public ActionResult DeleteOrder(int? id)
        {
            Order tmp = db.Orders.ToList().Find(x => x.OrderId == id);
            if (tmp != null)
            {
                db.Orders.Remove(tmp);
                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }
    }
}