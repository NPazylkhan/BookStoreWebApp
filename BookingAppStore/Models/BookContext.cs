using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace BookingAppStore.Models
{
    public class BookContext:DbContext
    {
        public DbSet<Book> Books { get; set; }
        public DbSet<Purchase> Purchases { get; set; }
        public DbSet<Person> People { get; set; }
    }
    public class BookDbInitializer : DropCreateDatabaseAlways<BookContext>
    {
        protected override void Seed(BookContext db)
        {
            db.Books.Add(new Book { Name = "Абай жолы", Author = "М. Ауезов", Price = 220, Image = "../Content/Images/bk.jpg" });
            db.Books.Add(new Book { Name = "Гарри Поттер и философский камень", Author = "Дж.К. Роулинг", Price = 220, Image = "../Content/Images/bk2.jpg" });
            db.Books.Add(new Book { Name = "Гарри Поттер и Тайная комната", Author = "Дж.К. Роулинг", Price = 220, Image = "../Content/Images/bk3.jpg" });
            db.Books.Add(new Book { Name = "Гарри Поттер и узник Азкабана", Author = "Дж.К. Роулинг", Price = 220, Image = "../Content/Images/bk4.jpg" });
            db.Books.Add(new Book { Name = "Гарри Поттер и Кубок огня", Author = "Дж.К. Роулинг", Price = 220, Image = "../Content/Images/bk5.jpg" });
            db.Books.Add(new Book { Name = "Гарри Поттер и Орден Феникса", Author = "Дж.К. Роулинг", Price = 220, Image = "../Content/Images/bk6.jpg" });
            db.Books.Add(new Book { Name = "Гарри Поттер и Принц-полукровка", Author = "Дж.К. Роулинг", Price = 220, Image = "../Content/Images/bk7.jpg" });
            db.Books.Add(new Book { Name = "Вокруг света за 80 дней", Author = "Ж. Верн", Price = 220, Image = "../Content/Images/book.jpg" });
            db.Books.Add(new Book { Name = "Таинственный остров", Author = "Ж. Верн", Price = 220, Image = "../Content/Images/bookstore.jpg" });
            db.Books.Add(new Book { Name = "Война и мир", Author = "Л.Толстой", Price = 220, Image = "../Content/Images/design.jpg" });
            base.Seed(db);
        }
    }
}