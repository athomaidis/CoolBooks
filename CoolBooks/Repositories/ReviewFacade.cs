using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CoolBooks.ViewModels;

namespace CoolBooks.Repositories
{
    public class ReviewFacade:IReviewFacade
    {
        BookRepository bookRepository = new BookRepository();
        /// <summary>
        /// Returns all books that not deleted
        /// </summary>
        /// <returns></returns>
        public List<BookReviewsViewModel> GetAllIncludingReviewsByUserId(string userId, string searchString)
        {
            return bookRepository.GetAllIncludingReviewsByUserId(userId, searchString);
        }

        /// <summary>
        /// Returns all books that not deleted
        /// </summary>
        /// <returns></returns>
        public List<BookReviewsViewModel> GetAllIncludingReviews(string searchString)
        {
            return bookRepository.GetAllIncludingReviews(searchString);
        }

    }
}