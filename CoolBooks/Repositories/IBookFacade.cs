﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CoolBooks.Models;
using CoolBooks.ViewModels;

namespace CoolBooks.Repositories
{
    public interface IBookFacade
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
        /// Gets a user by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Users GetUserById(string id);


        /// <summary>
        /// Gets top ten popular books based on reviews
        /// </summary>
        /// <returns></returns>
        List<Books> GetTopPopular(int nr);

 
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

        /// -----------------------------------
        /// Repository

        /// <summary>
        /// Gets all authors thats not delted
        /// </summary>
        /// <returns></returns>
        List<Authors> GetAllAuthors();

        /// <summary>
        /// Gets all genres thats not delted
        /// </summary>
        /// <returns></returns>
        List<Genres> GetAllGenres();

        /// <summary>
        /// Gets user by user id thats not deleted
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        Users GetUserByUserId(string userId);


    }

}