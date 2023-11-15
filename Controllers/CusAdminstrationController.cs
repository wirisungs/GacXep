using GacXep.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GacXep.Controllers
{
    public class CusAdminstrationController : Controller
    {
        private DBGacXepBookstoreEntities db = new DBGacXepBookstoreEntities();
        // GET: CusAdminstration
        public ActionResult Index()
        {
            return View(db.Customers.ToList());
        }
        

        public ActionResult Details(string id)
        {
            var khachhang = db.Customers.Where(k => k.CusPhone == id).FirstOrDefault();
            return View();
        }


        [HttpGet]

        public ActionResult Delete(string id)
        {
            if(id == null)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);

            }

            var khachhang = db.Customers.Where(k => k.CusPhone == id).FirstOrDefault();
            if(khachhang == null)
            {
                return HttpNotFound();
            }
            return View(khachhang);
        }

        [HttpPost, ActionName("Delete")]

        public ActionResult DeleteConfirmed(string id)
        {
            try
            {
                var khachhang = db.Customers.Where(k => k.CusPhone == id).FirstOrDefault();
                db.Customers.Remove(khachhang);
                db.SaveChanges();
                return RedirectToAction("Index", "CusAdminstration");
            }
            catch
            {
                return Content("Không xoá được do liên quan đến bảng khác");
            }
        }
    }
}