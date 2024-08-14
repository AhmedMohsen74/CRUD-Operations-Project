using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Crudd2.ViewModels;
using Crudd2.Models;
using System.Data.Entity;
using System.Net;

namespace Crudd2.Controllers
{
    public class BooksController : Controller
    {
        private readonly ApplicationDBContext _context;
        
        public BooksController()
        {
            _context = new ApplicationDBContext();
        }
        // GET: Books
        public ActionResult Index()
        {

            var books = _context.Books.Include(m=> m.Category).ToList();
            return View(books);
        }

        public ActionResult Create()
        {
            var ViewModel = new BookFormViewModel()
            {
                //Categories = _context.Categories.Where(m => m.isActive).ToList()
                Categories = _context.Categories.SqlQuery("SELECT * FROM Categories WHERE IsActive = 1").ToList()

            };
            return View("BookForm",ViewModel);
        }

        public ActionResult Edit(int? id)
        {

            if(id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest); 
            }

            var book = _context.Books.Find(id);
            if (book == null)
            {
                return HttpNotFound();
            }

            var viewModel = new BookFormViewModel()
            {
                Id = book.Id,
                Title = book.Title,
                Author = book.Author,
                CategoryId = book.CategoryId,
                Description = book.Description,
                Categories = _context.Categories.SqlQuery("SELECT * FROM Categories WHERE IsActive = 1").ToList()
                //Categories = _context.Categories.Where(m => m.isActive).ToList()

            };

            return View("BookForm",viewModel);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var book = _context.Books.Find(id);
            if(book == null)
            {
                return HttpNotFound();
            }

            _context.Books.Remove(book);
            _context.SaveChanges();


            return RedirectToAction("Index");
        }

        public ActionResult Details( int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var book = _context.Books.Include(m=>m.Category).SingleOrDefault(m=>m.Id == id);

            if (book == null)
                return HttpNotFound();

            return View(book);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(BookFormViewModel model)
        {
            if(!ModelState.IsValid)
            {
                model.Categories = _context.Categories.SqlQuery("SELECT * FROM Categories WHERE IsActive = 1").ToList();
                return View("BookForm", model);
            }

            if(model.Id == 0)  //34an 24of hwa hy3ml create l book gded wla hy3dl f kytab 2deem     fa lw el id == 0 de m3naha 2no be3ml wa7d gded
            {
                var book = new Book
                {
                    Title = model.Title,
                    Author = model.Author,
                    CategoryId = model.CategoryId,
                    Description = model.Description
                };

                _context.Books.Add(book);
            }
            else
            {
                var book = _context.Books.Find(model.Id);
                if (book == null)
                {
                    return HttpNotFound();
                }

                book.Title = model.Title;
                book.Author = model.Author;
                book.CategoryId = model.CategoryId;
                book.Description = model.Description;
                //added on btdaf 2wl mra

            }


            _context.SaveChanges();


            TempData["Message"] = "Saved Successfully";
            return RedirectToAction("Index");
        }

    }
}