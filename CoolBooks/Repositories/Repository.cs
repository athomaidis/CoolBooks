using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using CoolBooks.Models;

namespace CoolBooks.Repositories
{
    public class Repository:IRepository
    {
   
        public void Create(Reviews review)
        {

                using (var context = new CoolBooksContext())
                {
                    review.Created = DateTime.Now;
                    context.Reviews.Add(review);
                    context.SaveChanges();
                }
        }
   
        public void DeleteReview(int id)
        {
            using (var context = new CoolBooksContext())
            {
                context.Database.ExecuteSqlCommand("Update Reviews SET IsDeleted=1 WHERE Id=@id", new SqlParameter("id", id));
            }
        }
   

    
  

        /// <summary>
        /// Gets all authors thats not delted
        /// </summary>
        /// <returns></returns>
        public List<Authors> GetAllAuthors()
        {
            using (var context = new CoolBooksContext())
            {
                List<Authors> authors = context.Authors.Where(b => b.IsDeleted.Equals(false)).OrderBy(b => b.LastName).ToList();
                return authors;
            }
        }

        /// <summary>
        /// Gets all genres thats not delted
        /// </summary>
        /// <returns></returns>
        public List<Genres> GetAllGenres()
        {
            using (var context = new CoolBooksContext())
            {
                List<Genres> genres = context.Genres.Where(b => b.IsDeleted.Equals(false)).OrderBy(b=>b.Name).ToList();
                return genres;
            }
        }

    

   
        /// <summary>
        /// Returns all reviuews for a book that is not deleted
        /// </summary>
        /// <returns></returns>
        public List<ReviewViewModel> GetReviewsByBookId(int id)
        {

            using (var context = new CoolBooksContext())
            {
                List<Reviews> reviews = context.Reviews.Where(b => b.IsDeleted.Equals(false) & b.BookId.Equals(id)).Include(u=>u.User).ToList();
                List<ReviewViewModel> list = new List<ReviewViewModel>();
                foreach(Reviews review in reviews)
                {
                    ReviewViewModel vm = new ReviewViewModel
                    {
                        BookId = review.BookId,
                        Created = review.Created,
                        Id = review.Id,
                        Rating = review.Rating,
                        Text = review.Text,
                        Title = review.Title,
                        UserId = review.UserId,
                        UserFullName = review.User.FirstName + " " + review.User.LastName
                    };
                    list.Add(vm);
                }
                return list;
            }
        }

        /// <summary>
        /// Returns a reviuew for a book that is not deleted
        /// </summary>
        /// <returns></returns>
        public Reviews GetReviewByReviewId(int id)
        {
            using (var context = new CoolBooksContext())
            {
                Reviews review = context.Reviews.Where(r => r.IsDeleted.Equals(false) & r.Id.Equals(id)).Include(u=>u.User).FirstOrDefault();
                return review;
            }
        }

        /// <summary>
        /// Gets user by user id thats not deleted
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public Users GetUserByUserId(string userId)
        {
            using (var context = new CoolBooksContext())
            {
                Users user = context.Users.Where(u => u.IsDeleted.Equals(false) & u.UserId.Equals(userId)).FirstOrDefault();
                return user;
            }
        }

   
    }
}