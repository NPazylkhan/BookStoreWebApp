using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BookingAppStore;
using BookingAppStore.Models;
using System.IO;
using System.Runtime.InteropServices;

namespace BookingAppStore.Controllers
{
    
        public class StoreController : Controller
        {
            private BookContext db = new BookContext();

            // GET: /Store/
            
            public ActionResult Index()
            {
                return View(db.Books.ToList());
            }
            public ActionResult AutocompleteSearch(string term)
            {
                var authors = db.Books.Where(a => a.Author.Contains(term)).ToList().Select(a => new { value = a.Author }).Distinct();

                return Json(authors, JsonRequestBehavior.AllowGet);
            }
            //public ActionResult CreatePicture(int? id)
            //{
            //    if (id == null)
            //    {
            //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            //    }
            //    Book book = db.Books.Find(id);
            //    if (book == null)
            //    {
            //        return HttpNotFound();
            //    }
            //    return View(book);
            //}

            //[HttpPost]
            //public ActionResult CreatePicture(HttpPostedFileBase uploadImage, int id)
            //{
            //    Book book = db.Books.Find(id);
            //    if (ModelState.IsValid && uploadImage != null)
            //    {
            //        byte[] imageData = null;
            //        // считываем переданный файл в массив байтов
            //        using (var binaryReader = new BinaryReader(uploadImage.InputStream))
            //        {
            //            imageData = binaryReader.ReadBytes(uploadImage.ContentLength);
            //        }
            //        // установка массива байтов
            //       // book.Image = imageData;

            //        db.Books.Add(book);
            //        db.SaveChanges();

            //        return RedirectToAction("Index");
            //    }
            //    return View(book);
            //}
            [HttpPost]
            public ActionResult BookSearch(string name)
            {
                var allbooks = db.Books.Where(a => a.Author.Contains(name)).ToList();
                if (allbooks.Count <= 0)
                {
                    return HttpNotFound();
                }
                return PartialView(allbooks);
            }

            [HttpPost]

            // GET: /Store/Details/5
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

            // GET: /Store/Create
            public ActionResult Create()
            {
                return View();
            }

            // POST: /Store/Create
            // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
            // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
            [HttpPost]
            [ValidateAntiForgeryToken]
            public ActionResult Create([Bind(Include = "Id,Name,Author,Price,Image")] Book book)
            {
                if (ModelState.IsValid)
                {
                    db.Books.Add(book);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }

                return View(book);
            }

            // GET: /Store/Edit/5
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
                return View(book);
            }

            // POST: /Store/Edit/5
            // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
            // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
            [HttpPost]
            [ValidateAntiForgeryToken]
            public ActionResult Edit([Bind(Include = "Id,Name,Author,Price")] Book book)
            {
                if (ModelState.IsValid)
                {
                    db.Entry(book).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                return View(book);
            }

            // GET: /Store/Delete/5
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

            // POST: /Store/Delete/5
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
            [HttpGet]
            public ActionResult Buy(int id)
            {
                ViewBag.BookId = id;
                return View();
            }
            [HttpPost]
            public ActionResult Buy(Purchase purchase,int id)
            {
                Book book = db.Books.Find(id);
                purchase.Date = DateTime.Now;
                db.Purchases.Add(purchase);
                db.SaveChanges();
                ViewBag.Message = "Спасибо, " + purchase.Person + ", за покупку!"+" Вы купили книгу "+book.Name+",автор книги "+book.Author;
                return View("BuyOrder");
            }
        }
    }
