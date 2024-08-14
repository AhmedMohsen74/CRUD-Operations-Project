using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Crudd2.Models;

namespace Crudd2.Controllers.Api
{
    public class BooksController : ApiController
    {
        private readonly ApplicationDBContext _context;

        public BooksController()
        {
            _context = new ApplicationDBContext();
        }
        [HttpDelete]
        public IHttpActionResult DeleteBook(int id)
        {
            var book = _context.Books.Find(id);
            if (book == null)
            {
                return NotFound();
            }
            _context.Books.Remove(book);
            _context.SaveChanges();

            return Ok();
        }
    }
}
