using GacXep.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Net;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GacXep.Controllers
{
    public class BookDetailsController : Controller
    {
        private DBGacXepBookstoreEntities db = new DBGacXepBookstoreEntities();
        // GET: BookDetails
        public ActionResult Index(int? proID)
        {
            var bookDetails = db.BookDetails.Include(b => b.Publisher).Include(b => b.Book);
            if(proID == null)
            {
                return View(bookDetails.ToList());
            }
            else
            {
                return View(bookDetails.Where(b => b.ProID == proID).ToList());
            }
        }

        [ChildActionOnly]
        public ActionResult ImportedProducts(int id)
        {
            var importedProducts = (from pd in db.BookDetails
                                    where pd.ProID == id
                                    select pd).ToList();
            return PartialView("ImportedProducts", importedProducts);
        }

        public ActionResult Details(int? id)
        {
            if(id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BookDetail bookDetail = db.BookDetails.Find(id);
            if(bookDetail == null)
            {
                return HttpNotFound();
            }
            return View(bookDetail);
        }


        public ActionResult Create()
        {

            ViewBag.AuthorID = new SelectList(db.Authors, "AuthorID", "AuthorName");
            ViewBag.ProID = new SelectList(db.Books, "ProID", "ProName");
            ViewBag.PubID = new SelectList(db.Publishers, "PubID", "PubName");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ProDeID,ProID,PubID,AuthorID,Pages,Language,Price,RemainQuantity,SoldQuantity,ViewQuantity")] BookDetail bookDetail)
        {
            if(ModelState.IsValid) 
            {
                db.BookDetails.Add(bookDetail);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.AuthorID = new SelectList(db.Authors, "AuthorID", "AuthorName", bookDetail.AuthorID);
            ViewBag.ProID = new SelectList(db.Books, "ProID", "ProName", bookDetail.ProID);
            ViewBag.PubID = new SelectList(db.Publishers, "PubID", "PubName", bookDetail.PubID);
            return View(bookDetail);

        }

        public ActionResult Edit(int? id) 
        {
            if(id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BookDetail bookDetail = db.BookDetails.Find(id);
            if(bookDetail == null) 
            {
                return HttpNotFound();
            }

            ViewBag.AuthorID = new SelectList(db.Authors, "AuthorID", "AuthorName", bookDetail.AuthorID);
            ViewBag.ProID = new SelectList(db.Books, "ProID", "ProName", bookDetail.ProID);
            ViewBag.PubID = new SelectList(db.Publishers, "PubID", "PubName", bookDetail.PubID);

            return View(bookDetail);

        }


        [HttpPost]
        [ValidateAntiForgeryToken]

        public ActionResult Edit([Bind(Include = "ProDeID,ProID,PubID,AuthorID,Pages,Language,Price,RemainQuantity,SoldQuantity,ViewQuantity")] BookDetail bookDetail)
        {
            if(ModelState.IsValid) 
            {
                db.Entry(bookDetail).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.AuthorID = new SelectList(db.Authors, "AuthorID", "AuthorName", bookDetail.AuthorID);
            ViewBag.ProID = new SelectList(db.Books, "ProID", "ProName", bookDetail.ProID);
            ViewBag.PubID = new SelectList(db.Publishers, "PubID", "PubName", bookDetail.PubID);
            return View(bookDetail);

        }


        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BookDetail bookDetail = db.BookDetails.Find(id);
            if (bookDetail == null)
            {
                return HttpNotFound();
            }
            return View(bookDetail);
        }

        // POST: ProDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            BookDetail bookDetail = db.BookDetails.Find(id);
            db.BookDetails.Remove(bookDetail);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

    }
}