using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CoolBooks.Models;
using CoolBooks.ViewModels;


namespace CoolBooks.Repositories
{
    public class UserRepository
    {
        private CoolBooksContext db = new CoolBooksContext();

        public Users GetById(string id)
        {
            var user = db.Users.Find(id);
            return user;

        }
        public Users GetByEmail(string email)
        {
            var user = db.Users.Where(u => u.Email == email).FirstOrDefault();
            return user;

        }
    }
}