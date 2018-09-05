using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CoolBooks.Models;

namespace CoolBooks.Repositories
{
    public interface IRepository
    {


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