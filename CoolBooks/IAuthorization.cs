using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CoolBooks.Models;

namespace CoolBooks
{
    public interface IAuthorization
    {
        bool IsAuthorizedToEditBook(Books book, System.Security.Principal.IPrincipal user);
        bool IsAuthorizedToDeleteBook(Books book, System.Security.Principal.IPrincipal user);
        bool IsAuthorizedToDeleteReview(ReviewViewModel review, System.Security.Principal.IPrincipal user);
        bool IsAuthorizedToDeleteReview(Reviews review, System.Security.Principal.IPrincipal user);
    }
}