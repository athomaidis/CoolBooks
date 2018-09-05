using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CoolBooks.Models;
using CoolBooks.ViewModels;

namespace CoolBooks.Repositories
{
    public interface IBookRepository
    {
            /// <summary>
            /// Creates a new book
            /// </summary>
            /// <param name="book"></param>
            void Create(Books book);


            /// <summary>
            /// Marks a book as deleted in the database
            /// </summary>
            /// <param name="id"></param>
            void Delete(int id);


            /// <summary>
            /// Returns all books that not deleted
            /// </summary>
            /// <returns></returns>
            List<Books> GetAll();

        /// <summary>
        /// Returns all books that not deleted
        /// </summary>
        /// <returns></returns>
        List<BookReviewsViewModel> GetAllIncludingReviewsByUserId(string userId,string searchString);

        /// <summary>
        /// Returns all books that not deleted
        /// </summary>
        /// <returns></returns>
        List<BookReviewsViewModel> GetAllIncludingReviews(string searchString);

        /// <summary>
        /// Returns all books 
        /// </summary>
        /// <returns></returns>
        List<Books> GetAllIncludingDeleted();

            /// <summary>
            /// Returns all books with the users own books first in the list thats not deleted
            /// </summary>
            /// <param name="UserId"></param>
            /// <returns></returns>
            List<Books> GetAllSortByUserId(string userId);

        /// <summary>
        /// Gets top ten popular books based on reviews
        /// </summary>
        /// <returns></returns>
        List<Books> GetTopPopular(int nr);

        /// <summary>
        /// Returns latest created books 
        /// </summary>
        /// <returns></returns>
        List<Books> GetLatestCreated(int nr);

        /// <summary>
        /// Get a book thats not deleted
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Books GetById(int id);

            /// <summary>
            /// Get a book including deleted
            /// </summary>
            /// <param name="id"></param>
            /// <returns></returns>
            Books GetByIdIncludingDeleted(int id);


            /// <summary>
            /// finds books matching searchstring and not deleted
            /// </summary>
            /// <param name="SearchString"></param>
            /// <returns></returns>
            List<Books> Search(string SearchString);

            /// <summary>
            /// Finds books matching searchstring and not deleted
            /// the users own books are listed first
            /// </summary>
            /// <param name="userId"></param>
            /// <param name="searchString"></param>
            /// <returns></returns>
            List<Books> SearchSortByUserId(string userId, string searchString);


            /// <summary>
            /// Finds books matching searchstring
            /// the users own books are listed first
            /// </summary>
            /// <param name="userId"></param>
            /// <param name="searchString"></param>
            /// <returns></returns>
            List<Books> SearchSortByUserIdIncludingDeleted(string userId, string searchString);

            /// <summary>
            /// Checks if a ISBN is unique
            /// </summary>
            /// <param name="ISBN"></param>
            /// <returns></returns>
            bool UniqueISBN(string ISBN);

            /// <summary>
            /// Updates a book
            /// </summary>
            /// <param name="book"></param>
            void Update(Books book);

        }
    }