using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CoolBooks.Models;
using CoolBooks.ViewModels;

namespace CoolBooks.Repositories
{
    public interface IViewFacade
    {


        /// <summary>
        /// Get a book thats not deleted
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Books GetBookById(int id);

        /// <summary>
        /// Get a book including deleted
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Books GetBookByIdIncludingDeleted(int id);


        /// <summary>
        /// Gets user by user id thats not deleted
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        Users GetUserByUserId(string userId);

        /// <summary>
        /// Creates a new review
        /// </summary>
        /// <param name="review"></param>
        void Create(Reviews review);

        /// <summary>
        /// Marks a review as deleted in the database
        /// </summary>
        /// <param name="id"></param>
        void DeleteReview(int id);


        /// <summary>
        /// Gets all reviews for a book thats not deleted
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        List<ReviewViewModel> GetReviewsByBookId(int id);

        /// <summary>
        /// Gets a review for a book thats not deleted
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Reviews GetReviewByReviewId(int id);
    }

}