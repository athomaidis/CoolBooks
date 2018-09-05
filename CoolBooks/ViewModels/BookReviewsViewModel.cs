using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CoolBooks.Models;

namespace CoolBooks.ViewModels
{
    public class BookReviewsViewModel
    {
        public Books books;
        public List<ReviewViewModel> reviews;
    }
}