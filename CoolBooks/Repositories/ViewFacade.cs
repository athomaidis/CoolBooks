using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CoolBooks.Models;
using CoolBooks.ViewModels;

namespace CoolBooks.Repositories
{
    public class ViewFacade:IViewFacade
    {
        BookRepository bookRepository = new BookRepository();
        Repository repository = new Repository();
        ReviewRepository reviewRepository = new ReviewRepository();


        /// <summary>
        /// Get a book thats not deleted
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Books GetBookById(int id)
        {
            return bookRepository.GetById(id);
        }

        /// <summary>
        /// Get a book including deleted
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Books GetBookByIdIncludingDeleted(int id)
        {
            return bookRepository.GetByIdIncludingDeleted(id);
        }

        /// <summary>
        /// Gets user by user id thats not deleted
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public Users GetUserByUserId(string userId)
        {
            return repository.GetUserByUserId(userId);
        }



        /// <summary>
        /// Creates a new review
        /// </summary>
        /// <param name="review"></param>
        public void Create(Reviews review)
        {
            reviewRepository.Create(review);
        }

        /// <summary>
        /// Marks a review as deleted in the database
        /// </summary>
        /// <param name="id"></param>
        public void DeleteReview(int id)
        {
            reviewRepository.Delete(id);
        }


        /// <summary>
        /// Gets all reviews for a book thats not deleted
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public List<ReviewViewModel> GetReviewsByBookId(int id)
        {
            return reviewRepository.GetByBookId(id);
        }

        /// <summary>
        /// Gets a review for a book thats not deleted
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Reviews GetReviewByReviewId(int id)
        {
            return reviewRepository.GetByReviewId(id);
        }

    }
}