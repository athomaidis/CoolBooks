using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using CoolBooks.Models;

namespace CoolBooks.Controllers.Api
{
    public class AuthorsController : ApiController
    {
        private CoolBooksContext db;

        public AuthorsController()
        {
            db = new CoolBooksContext();
        }


        // GET all Authors
        public IEnumerable<Authors> GetAuthors()
        {
            return db.Authors.ToList();
        }

        // GET all Author/1

        public Authors GetAuthor(int id)
        {
            var Author = db.Authors.Find(id);
            if (Author == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
            return Author;
        }

        // POST /api/Author
        [HttpPost]
        public Authors CreateAuthor(Authors authors)
        {
            if (!ModelState.IsValid)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }

            db.Authors.Add(authors);
            db.SaveChanges();
            return authors;
        }

        // PUT /api/Author/1
        [HttpPut]
        public void UpdateAuthor(int id, Authors authors)
        {
            if (!ModelState.IsValid)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }

            var authorInDb = db.Authors.SingleOrDefault(e => e.Id == id);
            if (authorInDb == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            authorInDb.FirstName = authors.FirstName;
            authorInDb.LastName = authors.LastName;
            authorInDb.Created = authors.Created;
            authorInDb.IsDeleted = authors.IsDeleted;
            db.SaveChanges();
        }

        // DELETE /api/Author/1
        [HttpDelete]
        [Authorize(Roles = "Admin")]
        public void DeleteAuthor(int id)
        {
            var authorInDb = db.Authors.SingleOrDefault(e => e.Id == id);
            if (authorInDb == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);
            authorInDb.IsDeleted = true;
            db.SaveChanges();

        }

    }
}
