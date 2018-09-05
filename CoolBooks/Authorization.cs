using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CoolBooks.Models;

namespace CoolBooks
{
    public class Authorization  :IAuthorization
    {
        private IAuthentication authentication;

        public Authorization(IAuthentication authentication)
        {
            this.authentication = authentication;

        }

        public bool IsAuthorizedToEditBook(Books book, System.Security.Principal.IPrincipal user)
        {
            bool authorized = false;

           // Authorization check
            string userId = authentication.GetUserId(user);
            if (book.UserId==userId)
            {
                authorized= true;
            }

            if (user.IsInRole("Admin"))
            {
                authorized = true;
            }
            return authorized;
        }

        public bool IsAuthorizedToDeleteBook(Books book, System.Security.Principal.IPrincipal user)
        {
            return IsAuthorizedToEditBook(book, user);
        }

        public bool IsAuthorizedToDeleteReview(ReviewViewModel review, System.Security.Principal.IPrincipal user)
        {
            bool authorized = false;

            // Authorization check
            string userId = authentication.GetUserId(user);
            if (review.UserId == userId)
            {
                authorized = true;
            }

            if (user.IsInRole("Admin"))
            {
                authorized = true;
            }
            return authorized;
        }

        public bool IsAuthorizedToDeleteReview(Reviews review, System.Security.Principal.IPrincipal user)
        {
            bool authorized = false;

            // Authorization check
            string userId = authentication.GetUserId(user);
            if (review.UserId == userId)
            {
                authorized = true;
            }

            if (user.IsInRole("Admin"))
            {
                authorized = true;
            }
            return authorized;
        }
    }
}