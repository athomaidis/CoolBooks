using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using CoolBooks.Models;

namespace CoolBooks.Repositories
{
    public class AuthorsRepository
    {
        private CoolBooksContext db = new CoolBooksContext();

        // Create new Author
        public Boolean CreateAuthor(Authors author)
        {
            if (db.Authors.Any(a => a.FirstName == author.FirstName && a.LastName == author.LastName))
            {
                return false;
            }
            author.Created = DateTime.Now;
            db.Authors.Add(author);
            db.SaveChanges();
            return true;
        }

        // Update Author
        public Boolean EditAuthor(Authors authors)
        {
            if (db.Authors.Any(a => a.FirstName == authors.FirstName && a.LastName == authors.LastName & a.Id!=authors.Id))
            {
                return false;
            }
            db.Entry(authors).State = EntityState.Modified;
            authors.Created = DateTime.Now;
            db.SaveChanges();
            return true;
        }
        // Get by ID Author 
        public Authors GetAuthor(int? id)
        {
            var author = db.Authors.Find(id);
            return author;
        }
        

    }
}