using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CoolBooks.Models;

namespace CoolBooks.Repositories
{
    public interface IReviewRepository
    {

        /// <summary>
        /// Creates a new review
        /// </summary>
        /// <param name="review"></param>
        void Create(Reviews review);

        /// <summary>
        /// Marks a review as deleted in the database
        /// </summary>
        /// <param name="id"></param>
        void Delete(int id);


        /// <summary>
        /// Gets all reviews for a book thats not deleted
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        List<ReviewViewModel> GetByBookId(int id);

        /// <summary>
        /// Gets a review for a book thats not deleted
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Reviews GetByReviewId(int id);

    }
}