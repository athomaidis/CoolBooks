using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using CoolBooks.Models;
using CoolBooks.ViewModels;

namespace CoolBooks.Repositories
{
    public class BookRepository : IBookRepository
    {
        public void Create(Books book)
        {
            if (UniqueISBN(book.ISBN))
            {
                using (var context = new CoolBooksContext())
                {
                    book.Created = DateTime.Now;
                    context.Books.Add(book);
                    context.SaveChanges();
                }
            }
            else
            {
                throw new DataMisalignedException("ISBN number must be unique.");
            }
        }

        public void Delete(int id)
        {
            using (var context = new CoolBooksContext())
            {
                context.Database.ExecuteSqlCommand("Update Books SET IsDeleted=1 WHERE Id=@id", new SqlParameter("id", id));
            }
        }

        /// <summary>
        /// Returns all books that not deleted
        /// </summary>
        /// <returns></returns>
        public List<Books> GetAll()
        {

            using (var context = new CoolBooksContext())
            {
                List<Books> booksList = context.Books.Where(b => b.IsDeleted.Equals(false))
                    .Include(b => b.Authors).Include(b => b.Genres).
                    OrderBy(b => b.Title).ToList();
                return booksList;
            }
        }

        /// <summary>
        /// Returns all books 
        /// </summary>
        /// <returns></returns>
        public List<Books> GetAllIncludingDeleted()
        {

            using (var context = new CoolBooksContext())
            {
                List<Books> booksList = context.Books.
                    Include(b => b.Authors).Include(b => b.Genres)
                    .OrderBy(b => b.Title)
                    .ToList();
                return booksList;
            }
        }

        /// <summary>
        /// Returns top popular books 
        /// </summary>
        /// <returns></returns>
        public List<Books> GetTopPopular(int nr)
        {

            using (var context = new CoolBooksContext())
            {
                List<Books> booksList = context.Books 
                    .Include(b => b.Authors).Include(b => b.Genres)
                    .Where(b=>b.IsDeleted==false)
                    .OrderByDescending(b=>b.Reviews.Average(r=>r.Rating))
                    .Take(nr)
                    .ToList();
                return booksList;
            }
        }


        /// <summary>
        /// Returns latest created books 
        /// </summary>
        /// <returns></returns>
        public List<Books> GetLatestCreated(int nr)
        {

            using (var context = new CoolBooksContext())
            {
                List<Books> booksList = context.Books
                    .Include(b => b.Authors).Include(b => b.Genres)
                    .Where(b => b.IsDeleted == false)
                    .OrderByDescending(b => b.Created)
                    .Take(nr)
                    .ToList();
                return booksList;
            }
        }

        private List<ReviewViewModel> PopulateReviewViewModel(IEnumerable<Reviews> reviews)
        {
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
        
        /// <summary>
        /// Returns all books including reviews not including deleted books or reviews.
        /// </summary>
        /// <returns></returns>
        public List<BookReviewsViewModel> GetAllIncludingReviewsByUserId(string userId, string searchString)
        {
            using (var context = new CoolBooksContext())
            {
                List<BookReviewsViewModel> booksList;
                if (String.IsNullOrEmpty(searchString))
                {

                    booksList = context.Books
                     .Where(b => b.IsDeleted.Equals(false) && b.Reviews.Any(r => r.UserId.Equals(userId) && r.IsDeleted.Equals(false)))
                  .Select(b => new BookReviewsViewModel
                   {
                      books = b,
                  })
                 .OrderBy(b => b.books.Title)
                 .ToList();
                    foreach (BookReviewsViewModel bookItem in booksList)
                    {
                        var reviews = context.Reviews.Where(r => r.UserId.Equals(userId) && r.BookId.Equals(bookItem.books.Id) && r.IsDeleted.Equals(false)).Include(u => u.User);
                        bookItem.reviews = PopulateReviewViewModel(reviews);
                    } 
                }
                else
                {
                    booksList = context.Books
                                    .Where(b => b.IsDeleted.Equals(false) && b.Reviews.Any(r => r.UserId.Equals(userId) &&
                                    r.IsDeleted.Equals(false) && 
                                    (r.Title.Contains(searchString) || r.Text.Contains(searchString) | r.User.FirstName.Contains(searchString) | r.User.LastName.Contains(searchString)))
                                    || b.Reviews.Any(r => r.UserId.Equals(userId) &&
                                     r.IsDeleted.Equals(false) && 
                                     b.Title.Contains(searchString)
                                    ))
                                    .Select(b => new BookReviewsViewModel
                                    {
                                        books = b,
                                    })
                                   .OrderBy(b => b.books.Title)
                                 .ToList();
                    foreach (BookReviewsViewModel bookItem in booksList)
                    {
                        var reviews = context.Reviews.Where(r => r.BookId.Equals(bookItem.books.Id)
                        && r.UserId.Equals(userId) 
                        && r.IsDeleted.Equals(false)
                        && (r.Title.Contains(searchString) || r.Text.Contains(searchString) || r.User.FirstName.Contains(searchString) || r.User.LastName.Contains(searchString))
                        || (
                        r.UserId.Equals(userId)
                        && r.IsDeleted.Equals(false)
                        && bookItem.books.Title.Contains(searchString)
                        )
                        )
                        
                        .Include(u => u.User);

                        bookItem.reviews = PopulateReviewViewModel(reviews);
                    }
                }

                return booksList;
            }
        }

        /// <summary>
        /// Returns all books including reviews not including deleted books or reviews.
        /// </summary>
        /// <returns></returns>
        public List<BookReviewsViewModel> GetAllIncludingReviews(string searchString)
        {
            using (var context = new CoolBooksContext())
            {
                List<BookReviewsViewModel> booksList;
                if (String.IsNullOrEmpty(searchString))
                {
                    booksList = context.Books
                        .Where(b => b.IsDeleted.Equals(false) && b.Reviews.Any(r => r.IsDeleted.Equals(false)))
                        .Select(b => new BookReviewsViewModel
                        {
                            books = b,
                            
                        })
                        .OrderBy(b => b.books.Title)
                        .ToList();
                    foreach (BookReviewsViewModel bookItem in booksList)
                    {
                        var reviews = context.Reviews.Where(r => r.BookId.Equals(bookItem.books.Id) && r.IsDeleted.Equals(false)).Include(u=>u.User);
                        bookItem.reviews = PopulateReviewViewModel(reviews);
                    }
                }
                else
                {
                    booksList = context.Books
                                    .Where(b => b.IsDeleted.Equals(false) && b.Reviews.Any(r => 
                                    r.IsDeleted.Equals(false) &&
                                    (r.Title.Contains(searchString) || r.Text.Contains(searchString) | r.User.FirstName.Contains(searchString) | r.User.LastName.Contains(searchString)))
                                    || b.Reviews.Any(r => 
                                     r.IsDeleted.Equals(false) &&
                                     b.Title.Contains(searchString)
                                    ))
                                    .Select(b => new BookReviewsViewModel
                                    {
                                        books = b,
                                    })
                                   .OrderBy(b => b.books.Title)
                                 .ToList();
                    foreach (BookReviewsViewModel bookItem in booksList)
                    {
                        var reviews = context.Reviews.Where(r => r.BookId.Equals(bookItem.books.Id)
                        && r.IsDeleted.Equals(false)
                        && (r.Title.Contains(searchString) || r.Text.Contains(searchString) || r.User.FirstName.Contains(searchString) || r.User.LastName.Contains(searchString))
                        || (
                        r.IsDeleted.Equals(false)
                        && bookItem.books.Title.Contains(searchString)
                        )
                        )

                        .Include(u => u.User);

                        bookItem.reviews = PopulateReviewViewModel(reviews);
                    }
                }
                return booksList;
            }
        }

        /// <summary>
        /// Returns all books with the users own books first in the list
        /// </summary>
        /// <param name="UserId"></param>
        /// <returns></returns>
        public List<Books> GetAllSortByUserId(string userId)
        {

            using (var context = new CoolBooksContext())
            {
                List<Books> booksList = context.Books.Where(b => b.IsDeleted.Equals(false) & b.UserId.Equals(userId))
                    .Include(b => b.Authors).Include(b => b.Genres)
                    .OrderBy(b => b.Title)
                    .ToList();
                List<Books> allBooksUnsorted = context.Books.Where(b => b.IsDeleted.Equals(false))
                    .Include(b => b.Authors).Include(b => b.Genres)
                    .OrderBy(b => b.Title)
                    .ToList();
                List<Books> booksExceptUsers = allBooksUnsorted.Except(booksList).ToList();
                booksList.AddRange(booksExceptUsers);
                return booksList;
            }

        }

        /// <summary>
        /// Returns all books with the users own books first in the list
        /// </summary>
        /// <param name="UserId"></param>
        /// <returns></returns>
        public List<Books> GetAllSortByUserIdIncludingDeleted(string userId)
        {

            using (var context = new CoolBooksContext())
            {
                List<Books> booksList = context.Books.Where(b => b.UserId.Equals(userId))
                    .Include(b => b.Authors).Include(b => b.Genres)
                    .OrderBy(b => b.Title)
                    .ToList();
                List<Books> allBooksUnsorted = context.Books
                    .Include(b => b.Authors).Include(b => b.Genres)
                    .OrderBy(b => b.Title)
                    .ToList();
                List<Books> booksExceptUsers = allBooksUnsorted.Except(booksList).ToList();
                booksList.AddRange(booksExceptUsers);
                return booksList;
            }

        }

        /// <summary>
        /// Get a book that not deleted
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Books GetById(int id)
        {
            using (var context = new CoolBooksContext())
            {
                Books book = context.Books.Include(b => b.Authors).Where(b => b.IsDeleted.Equals(false) & b.Id == id).Include(b => b.Genres).Include(b => b.Users).FirstOrDefault();
                return book;
            }
        }

        /// <summary>
        /// Get a book included deleted
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Books GetByIdIncludingDeleted(int id)
        {
            using (var context = new CoolBooksContext())
            {
                Books book = context.Books.Include(b => b.Authors).Where(b => b.Id == id).Include(b => b.Genres).Include(b => b.Users).FirstOrDefault();
                return book;
            }
        }


        /// <summary>
        /// finds books matching searchstring and not deleted
        /// </summary>
        /// <param name="searchString"></param>
        /// <returns></returns>
        public List<Books> Search(string searchString)
        {

            if (String.IsNullOrWhiteSpace(searchString))
            {
                return GetAll();
            }
            else
                using (var context = new CoolBooksContext())
                {
                    searchString = searchString.Trim();
                    List<Books> booksList = context.Books.Where(b => b.IsDeleted.Equals(false) & (b.Title.Contains(searchString) | b.AlternativeTitle.Contains(searchString) | b.Description.Contains(searchString) | b.ISBN.Contains(searchString) | b.Authors.FirstName.Contains(searchString) | b.Authors.LastName.Contains(searchString)))
                      .Include(b => b.Authors).Include(b => b.Genres).OrderBy(b => b.Title).ToList();
                    return booksList;
                }

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

            if (String.IsNullOrWhiteSpace(searchString))
            {
                return GetAllSortByUserId(userId);
            }
            else
                using (var context = new CoolBooksContext())
                {
                    searchString = searchString.Trim();
                    List<Books> booksList = context.Books.Include(b => b.Authors).Include(b => b.Genres)
                    .Where(b => b.IsDeleted.Equals(false) & b.UserId.Equals(userId) & (b.Title.Contains(searchString) | b.AlternativeTitle.Contains(searchString) | b.Description.Contains(searchString) | b.ISBN.Contains(searchString) | b.Authors.FirstName.Contains(searchString) | b.Authors.LastName.Contains(searchString)))
                    .ToList();
                    List<Books> allBooksUnsorted = context.Books.Include(b => b.Authors).Include(b => b.Genres)
                        .Where(b => b.IsDeleted.Equals(false) & (b.Title.Contains(searchString) | b.AlternativeTitle.Contains(searchString) | b.Description.Contains(searchString) | b.ISBN.Contains(searchString) | b.Authors.FirstName.Contains(searchString) | b.Authors.LastName.Contains(searchString)))
                        .ToList();
                    List<Books> booksExceptUsers = allBooksUnsorted.Except(booksList).ToList();
                    booksList.AddRange(booksExceptUsers);
                    return booksList;
                }
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

            if (String.IsNullOrWhiteSpace(searchString))
            {
                return GetAllSortByUserIdIncludingDeleted(userId);
            }
            else
                using (var context = new CoolBooksContext())
                {
                    searchString = searchString.Trim();
                    List<Books> booksList = context.Books.Include(b => b.Authors).Include(b => b.Genres)
                    .Where(b => b.UserId.Equals(userId) & (b.Title.Contains(searchString) | b.AlternativeTitle.Contains(searchString) | b.Description.Contains(searchString) | b.ISBN.Contains(searchString) | b.Authors.FirstName.Contains(searchString) | b.Authors.LastName.Contains(searchString)))
                    .ToList();
                    List<Books> allBooksUnsorted = context.Books.Include(b => b.Authors).Include(b => b.Genres)
                        .Where(b => b.Title.Contains(searchString) | b.AlternativeTitle.Contains(searchString) | b.Description.Contains(searchString) | b.ISBN.Contains(searchString) | b.Authors.FirstName.Contains(searchString) | b.Authors.LastName.Contains(searchString))
                        .ToList();
                    List<Books> booksExceptUsers = allBooksUnsorted.Except(booksList).ToList();
                    booksList.AddRange(booksExceptUsers);
                    return booksList;
                }
        }

        public bool UniqueISBN(string ISBN)
        {
            bool unique = false;
            using (var context = new CoolBooksContext())
            {
                unique = !context.Books.Any(b => b.ISBN.Equals(ISBN));
            }
            return unique;

        }

        public void Update(Books book)
        {
            using (var context = new CoolBooksContext())
            {
                context.Entry(book).State = EntityState.Modified;
                context.SaveChanges();
            }
        }


    }
}