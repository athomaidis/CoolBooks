using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CoolBooks.Models;
using CoolBooks.Repositories;
using CoolBooks.ViewModels;


namespace CoolBooks.Controllers
{
  

    public class ReviewsController : Controller
    {
        private IAuthentication authentication;
        private IAuthorization authorization;
        private IReviewFacade repository;

        /// <summary>
        /// Default constructor
        /// </summary>
        public ReviewsController()
        {
            authentication = new Authentication();
            authorization = new Authorization(authentication);
            repository = new ReviewFacade(); 
        }
        /// <summary>
        /// Constructor for unit testing
        /// </summary>
        /// <param name="factory">Object responsible for creating objects</param>
        public ReviewsController(IAuthentication authentication,IAuthorization authorization, IReviewFacade reviewFasade)
        {
            this.authentication = authentication;
            this.authorization = authorization;
            this.repository = reviewFasade;
        }


        private Dictionary<string,int> CreateUserIdNr(List<BookReviewsViewModel> booksList)
        {
            int i = 0;
            Dictionary<string, int> dict = new Dictionary<string, int>();
            foreach(var book in booksList)
            {
                foreach (var review in book.reviews)
                {

                    
                    if (!dict.ContainsKey(review.UserId))
                    {
                        i++;
                        dict.Add(review.UserId,i);
                    }
                }
            }
            return dict;
        }

        // GET: Review
        [Authorize(Roles = "Admin")]
        public ActionResult Index(string searchString)
        {
            List<BookReviewsViewModel> booksList;
            booksList = repository.GetAllIncludingReviews(searchString);
            Session["UserIdNr"] = CreateUserIdNr(booksList);

            return View(booksList);
        }
        // GET: Review
        [Authorize(Roles = "BasicUser,Admin")]
        public ActionResult MyReviews(string searchString)
        {
            List<BookReviewsViewModel> booksList;
            booksList = repository.GetAllIncludingReviewsByUserId(authentication.GetUserId(User), searchString);

            return View(booksList);
        }

    }
}