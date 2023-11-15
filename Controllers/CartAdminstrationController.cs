using GacXep.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GacXep.Controllers
{
    public class CartAdminstrationController : Controller
    {
        private DBGacXepBookstoreEntities db = new DBGacXepBookstoreEntities();
        // GET: CartAdminstration
        public ActionResult Index()
        {
            return View(db.Orders.ToList());
        }





        [HttpGet]
        public ActionResult Delete(int? id)
        {
            if(id == null)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }
            var giohang = db.Orders.Where(g => g.OrderID == id).FirstOrDefault();
            if(giohang == null)
            {
                return HttpNotFound();
            }
            return View(giohang);
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                var giohang = db.Orders.Where(g => g.OrderID == id).FirstOrDefault();
                db.Orders.Remove(giohang);
                db.SaveChanges();
                return RedirectToAction("Index", "CartAdminstration");
            }
            catch
            {
                return Content("Không xoá được, xin hãy liên hệ bộ phận IT");
            }
        }
    }
}