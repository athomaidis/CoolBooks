using CoolBooks.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;

namespace CoolBooks.Repositories
{
    public class GenresRepository : IGenresRepository
    {
        private CoolBooksContext db = new CoolBooksContext();

        // Create new Genre
        public Genres CreateGenre(Genres genre)
        {
            genre.Created = DateTime.Now;
            db.Genres.Add(genre);
            db.SaveChanges();
            return genre;
        }

        // Update Genre
        public bool EditGenre(Genres genre)
        {
            if (AllowEdit(genre.Name, genre.Id)) {

                using (var db = new CoolBooksContext())
                {
                    genre.Created = GetGenre(genre.Id).Created;
                    db.Entry(genre).State = EntityState.Modified;
                    db.SaveChanges();
                    return true;
                }
            }
            else { return false; }
        }

        // Get Genre by ID 
        public Genres GetGenre(int? id)
        {
            var genre = db.Genres.Find(id);
            return genre;
        }

        //Delete (Soft) Genre by ID
        public void DeleteGenre(int? id)
        {
            Genres genre = db.Genres.Find(id);
            db.Database.ExecuteSqlCommand("Update Genres SET IsDeleted=1 WHERE Id=@id", new SqlParameter("id", id));
            db.SaveChanges();
        }
        /// <summary>
        /// Returns all books that not deleted
        /// </summary>
        /// <returns></returns>
        public List<Genres> GetAll()
        {
            using (var context = new CoolBooksContext())
            {
                List<Genres> genresList = context.Genres.OrderBy(b => b.Id).ToList();
                return genresList;
            }
        }

        /// <summary>
        /// Checks if a genre already exists when creating one. It is case insensitive
        /// </summary>
        /// <param name="genreName"></param>
        /// <returns></returns>
        public bool AllowCreate(string genreName)
        {
            bool uniqueName = false;
          
            // checks if the genre name is unique. ( return true if the name does NOT exist)
            uniqueName = !db.Genres.Any(b => b.Name.Equals(genreName, StringComparison.OrdinalIgnoreCase));

            return uniqueName;
        }

        /// <summary>
        /// Checks if a genre already exists when creating one. It is case insensitive
        /// </summary>
        /// <param name="genreName"></param>
        /// <returns></returns>
        public bool AllowEdit(string genreName, int? id)
        {
            bool uniqueName = false;
            bool uniqueID = false;
            int currentID = GetGenre(id).Id;
            bool deletedState = GetGenre(id).IsDeleted;

           
        // checks if the genre name is unique. ( return true if the name does NOT exist)
            uniqueName = !db.Genres.Any(b => b.Name.Equals(genreName, StringComparison.OrdinalIgnoreCase));

            Genres existingGenre = db.Genres.FirstOrDefault(g => g.Id.Equals(currentID));

            if (currentID == existingGenre.Id)
            {
                uniqueID = true;
            }
            else
            {
                uniqueID = false;
            }

            if (uniqueName != true && uniqueID == true)
            {
                return true;
            }
            else
            {
                return false;
            }     
        }
    }
}