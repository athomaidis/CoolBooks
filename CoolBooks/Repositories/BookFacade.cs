using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CoolBooks.Models;
using CoolBooks.ViewModels;

namespace CoolBooks.Repositories
{
    public class BookFacade:IBookFacade
    {
        BookRepository bookRepository = new BookRepository();
        Repository repository = new Repository();
        UserRepository userRepository = new UserRepository();

        /// <summary>
        /// Creates a new book
        /// </summary>
        /// <param name="book"></param>
        public void Create(Books book)
        {
            bookRepository.Create(book);
        }


        /// <summary>
        /// Marks a book as deleted in the database
        /// </summary>
        /// <param name="id"></param>
        public void Delete(int id)
        {
            bookRepository.Delete(id);
        }


   

        /// <summary>
        /// Get a book thats not deleted
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Books GetById(int id)
        {
            return bookRepository.GetById(id);
        }

        /// <summary>
        /// Get a book including deleted
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Books GetByIdIncludingDeleted(int id)
        {
            return bookRepository.GetByIdIncludingDeleted(id);
        }


        /// <summary>
        /// Get a user by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Users GetUserById(string id)
        {
            return userRepository.GetById(id);
        }

        /// <summary>
        /// Get top popular books
        /// </summary>
        /// <param name="nr"></param>
        /// <returns></returns>
        public List<Books> GetTopPopular(int nr)
        {
            return bookRepository.GetTopPopular(nr);
        }

        /// <summary>
        /// finds books matching searchstring and not deleted
        /// </summary>
        /// <param name="SearchString"></param>
        /// <returns></returns>
        public List<Books> Search(string searchString)
        {
            return bookRepository.Search(searchString);
        }

        /// <summary>
        /// Finds books matching searchstring and not deleted
        /// the users own books are listed first
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="searchString"></param>
        /// <returns></returns>
        public List<Books> SearchSortByUserId(string userId, string searchString)
        {
            return bookRepository.SearchSortByUserId(userId, searchString);
        }


        /// <summary>
        /// Finds books matching searchstring
        /// the users own books are listed first
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="searchString"></param>
        /// <returns></returns>
        public List<Books> SearchSortByUserIdIncludingDeleted(string userId, string searchString)
        {
            return bookRepository.SearchSortByUserIdIncludingDeleted(userId, searchString);
        }

        /// <summary>
        /// Checks if a ISBN is unique
        /// </summary>
        /// <param name="ISBN"></param>
        /// <returns></returns>
        public bool UniqueISBN(string ISBN)
        {
            return bookRepository.UniqueISBN(ISBN);
        }

        /// <summary>
        /// Updates a book
        /// </summary>
        /// <param name="book"></param>
        public void Update(Books book)
        {
            bookRepository.Update(book);
        }


        /// ----------------------
        /// Repository

        /// <summary>
        /// Gets all authors thats not delted
        /// </summary>
        /// <returns></returns>
        public List<Authors> GetAllAuthors()
        {
            return repository.GetAllAuthors();
        }

        /// <summary>
        /// Gets all genres thats not delted
        /// </summary>
        /// <returns></returns>
        public List<Genres> GetAllGenres()
        {
            return repository.GetAllGenres();
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

    }
}