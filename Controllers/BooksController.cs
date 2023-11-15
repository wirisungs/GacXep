using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using GacXep.Models;
using System.IO;

namespace GacXep.Controllers
{
    public class BooksController : Controller
    {
        private DBGacXepBookstoreEntities db = new DBGacXepBookstoreEntities();
        // GET: Products
        public ActionResult Index()
        {
            var books = db.Books.Include(b => b.Category);
            return View(books.ToList());
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Book book = db.Books.Find(id);
            if (book == null)
            {
                return HttpNotFound();
            }
            return View(book);
        }

        public ActionResult Create()
        {
            ViewBag.CatID = new SelectList(db.Categories, "CatID", "NameCate");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ProID,ProName,CatID,ProImage,NameDecription," +
            "CreatedDate, UploadImage")] Book book)
        {
            if (ModelState.IsValid)
            {
                //bo sung doan code de gan duong dan anh cho ProImage va luu anh vao thu muc Images tren server
                if (book.UploadImage != null)
                {
                    string path = "~/Images/";
                    string filename = Path.GetFileName(book.UploadImage.FileName);
                    book.ProImage = path + filename;
                    book.UploadImage.SaveAs(Path.Combine(Server.MapPath(path), filename));
                }
                book.CreatedDate = DateTime.Today;

                //doan code giu nguyen
                db.Books.Add(book);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CatID = new SelectList(db.Categories, "CatID", "NameCate", book.CatID);
            return View(book);
            //return View();
        }


        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Book book = db.Books.Find(id);
            if (book == null)
            {
                return HttpNotFound();
            }
            ViewBag.CatID = new SelectList(db.Categories, "CatID", "NameCate", book.CatID);
            return View(book);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public ActionResult Edit([Bind(Include = "ProID,ProName,CatID,ProImage,NameDecription," +
            "CreatedDate, UploadImage")] Book book)
        {
            if (ModelState.IsValid)
            {
                if (book.UploadImage != null)
                {
                    string path = "~/Images/";
                    string filename = Path.GetFileName(book.UploadImage.FileName);
                    book.ProImage = path + filename;
                    book.UploadImage.SaveAs(Path.Combine(Server.MapPath(path), filename));
                }

                db.Entry(book).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CatID = new SelectList(db.Categories, "CatID", "NameCate", book.CatID);
            return View(book);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Book book = db.Books.Find(id);
            if (book == null)
            {
                return HttpNotFound();
            }
            return View(book);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Book book = db.Books.Find(id);
            db.Books.Remove(book);
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