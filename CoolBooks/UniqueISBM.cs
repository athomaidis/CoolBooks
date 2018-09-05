using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using CoolBooks.Models;
using CoolBooks.Repositories;

namespace CoolBooks
{
    public class UniqueISBN : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            Books book = (Books) validationContext.ObjectInstance;
            var rep = new BookRepository();

            book = rep.GetByIdIncludingDeleted(book.Id);
            if (book != null)
            {
                // changed ISBN and not unique gives errormesage
                if (!book.ISBN.Equals(value) && !rep.UniqueISBN((string)value))
                {
                    return new ValidationResult("ISBN must be unique for updated books.");
                }
            }
            else {
                // new not unique ISBN gives errormesage
                if (!rep.UniqueISBN((string)value))
                {
                    return new ValidationResult("ISBN must be unique for new books.");
                };
            }

            return null;
        }

  
    }
}