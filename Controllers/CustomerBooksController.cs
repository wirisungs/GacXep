using GacXep.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GacXep.Controllers
{
    public class CustomerBooksController : Controller
    {
        private DBGacXepBookstoreEntities db = new DBGacXepBookstoreEntities();
        // GET: CustomerProducts
        public ActionResult Index()
        {
            var booksDetail = db.BookDetails;
            return View(booksDetail.ToList());
        }

        public ActionResult NewArrivals()
        {
            var booksDetails = db.BookDetails;
            return View(booksDetails.ToList());
        }

        public ActionResult Details(int? id)
        {
            if(id == null)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }
            BookDetail bookDetail = db.BookDetails.Find(id);
            if(bookDetail == null) 
            {
                return HttpNotFound();
            }
            return View(bookDetail);
        }
    }
}