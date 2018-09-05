using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using CoolBooks.Models;

namespace CoolBooks.Repositories
{
    public class ReviewRepository:IReviewRepository
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

        public void Delete(int id)
        {
            using (var context = new CoolBooksContext())
            {
                context.Database.ExecuteSqlCommand("Update Reviews SET IsDeleted=1 WHERE Id=@id", new SqlParameter("id", id));
            }
        }


        /// <summary>
        /// Returns all reviuews for a book that is not deleted
        /// </summary>
        /// <returns></returns>
        public List<ReviewViewModel> GetByBookId(int id)
        {

            using (var context = new CoolBooksContext())
            {
                List<Reviews> reviews = context.Reviews.Where(b => b.IsDeleted.Equals(false) & b.BookId.Equals(id)).Include(u => u.User).ToList();
                List<ReviewViewModel> list = new List<ReviewViewModel>();
                foreach (Reviews review in reviews)
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
        public Reviews GetByReviewId(int id)
        {
            using (var context = new CoolBooksContext())
            {
                Reviews review = context.Reviews.Where(r => r.IsDeleted.Equals(false) & r.Id.Equals(id)).Include(u => u.User).FirstOrDefault();
                return review;
            }
        }

      

    }
}