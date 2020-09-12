using BookingAppStore.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace BookingAppStore.Controllers
{
    public class HomeController : Controller
    {
        BookContext db = new BookContext();
        private Person[] personData = {
 new Person {PersonId = 11, FirstName = "Нұрмахан", LastName = "Пазылхан",
 Role = Role.Admin},
 new Person {PersonId = 1, FirstName = "Jacqui", LastName = "Griffyth",
 Role = Role.User},
 new Person {PersonId = 2, FirstName = "John", LastName = "Smith",
 Role = Role.User},
 new Person {PersonId = 3, FirstName = "Anne", LastName = "Jones",
 Role = Role.Guest}
                                      };
   public interface IModelBinder
    {
        object BindModel(ControllerContext controllerContext, 
            ModelBindingContext bindingContext);
    }
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
        //public ActionResult AutocompleteSearch(string term)
        //{
        //    var authors = db.Books.Where(a => a.Author.Contains(term)).ToList().Select(a => new { value = a.Author }).Distinct();

        //    return Json(authors, JsonRequestBehavior.AllowGet);
        //}
        [HttpGet]
            public ActionResult Buy(int? id)
            {
                ViewBag.BookId = id;
                return View();
            }
        [HttpPost]
        public ActionResult Buy(Purchase purchase, int id)
        {
            Book book = db.Books.Find(id);
            purchase.Date = DateTime.Now;
            db.Purchases.Add(purchase);
            db.SaveChanges();
            ViewBag.Message = "Спасибо, " + purchase.Person + ", за покупку!" + " Вы купили книгу " + book.Name + ",автор книги " + book.Author;
            return View("BuyOrder");
        }
        public ActionResult Index()
        {            
            return View(db.Books);
        }
        public ActionResult Purchase()
        {
            return View(db.Purchases);
        }
        public ActionResult Index2()
        {
            var books = db.Books;
            return View(books);
        }
        public ActionResult Admin(int id=11)
        {
            Person dataItem = personData.Where(p => p.PersonId == id).First();
            return View(dataItem);
        }
        //public ActionResult CreatePerson()
        //{
        //    return View(new Person());
        //}

        //[HttpPost]
        //public ActionResult CreatePerson(Person model)
        //{
        //    return View(model);
        //}
        //[HttpGet]
        //public ActionResult CreateB()
        //{
        //    return View();
        //}
        //[HttpPost]
        //public ActionResult CreateB(Book book)
        //{
        //    db.Entry(book).State = EntityState.Added;
        //    db.SaveChanges();

        //    return RedirectToAction("Index");
        //}
        ////public ActionResult Create(Book book)
        ////{
        ////    db.Books.Add(book);
        ////    db.SaveChanges();

        ////    return RedirectToAction("Index");
        ////}
        //[HttpGet]
        //public ActionResult Delete(int id)
        //{
        //    Book b = new Book { Id = id };
        //    db.Entry(b).State = EntityState.Deleted;
        //    db.SaveChanges();

        //    return RedirectToAction("Index");
        //}
        //[HttpPost]

        //// GET: /Store/Details/5
        //public ActionResult Details(int? id)
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
        public ActionResult Delete(int id)
        {
            Purchase b = db.Purchases.Find(id);
            if (b == null)
            {
                return HttpNotFound();
            }
            return View(b);
        }
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Purchase b = db.Purchases.Find(id);
            if (b == null)
            {
                return HttpNotFound();
            }
            db.Purchases.Remove(b);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        //[HttpGet]
        //public ActionResult EditBook(int? id)
        //{
        //    if (id == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    Book book = db.Books.Find(id);
        //    if (book != null)
        //    {
        //        return View(book);
        //    }
        //    return HttpNotFound();
        //}
        //[HttpPost]
        //public ActionResult EditBook(Book book)
        //{
        //    db.Entry(book).State = EntityState.Modified;
        //    db.SaveChanges();
        //    return RedirectToAction("Index");
        //}
      
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        
        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}