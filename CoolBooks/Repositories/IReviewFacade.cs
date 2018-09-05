using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CoolBooks.ViewModels;

namespace CoolBooks.Repositories
{
    public interface IReviewFacade
    {

        /// <summary>
        /// Returns all books that not deleted
        /// </summary>
        /// <returns></returns>
        List<BookReviewsViewModel> GetAllIncludingReviewsByUserId(string userId, string searchString);

        /// <summary>
        /// Returns all books that not deleted
        /// </summary>
        /// <returns></returns>
        List<BookReviewsViewModel> GetAllIncludingReviews(string searchString);

    }
}