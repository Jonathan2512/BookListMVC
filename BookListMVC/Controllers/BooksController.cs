using BookListMVC.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookListMVC.Controllers
{
    public class BooksController : Controller
    {
        private readonly ApplicationDBContext db;
        [BindProperty]
        public Book Book { get; set; }
        public BooksController(ApplicationDBContext db)
        {
            this.db = db;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Upsert(int? id) // a nullable parameter
        {
            Book = new Book();
            // create
            if (id == null)
                return View(Book);

            // update
            Book = db.Books.FirstOrDefault(b => b.Id == id);
            if (Book == null)
                return NotFound();

            return View(Book);
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert() // a nullable parameter
        {
            if(ModelState.IsValid)
            {
                if (Book.Id == 0)
                    db.Books.Add(Book);
                else
                    db.Books.Update(Book);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(Book);
        }

        #region API CAllls

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Json(new { data = await db.Books.ToListAsync() });
        }
        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var bookFromDb = await db.Books.FirstOrDefaultAsync(u => u.Id == id);
            if (bookFromDb == null)
                return Json(new { success = false, message = "Error : Book can not be found in database" });
            db.Books.Remove(bookFromDb);
            await db.SaveChangesAsync();
            return Json(new { success = true, message = "Delete Successful" });
        }
        #endregion 
    }
}
