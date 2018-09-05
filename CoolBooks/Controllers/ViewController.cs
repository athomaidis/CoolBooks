using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using CoolBooks.Models;
using CoolBooks.Repositories;
using MvcFlashMessages;

namespace CoolBooks.Controllers
{
    public class ViewController : Controller
    {
        private IAuthentication authentication;
        private IAuthorization authorization;
        private IViewFacade viewRepository;

        /// <summary>
        /// Default constructor
        /// </summary>
        public ViewController()
        {
            authentication = new Authentication();
            authorization = new Authorization(authentication);
            viewRepository = new ViewFacade();
        }
        /// <summary>
        /// Constructor for unit testing
        /// </summary>
        /// <param name="factory">Object responsible for creating objects</param>
        public ViewController(IAuthentication authentication, IAuthorization authorization, IViewFacade viewFasade)
        {
            this.authentication = authentication;
            this.authorization = authorization;
            viewRepository = viewFasade;
        }
        // GET: View/Index/5
        public ActionResult Index(int? id,string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Books books ;
            if (User!=null && User.IsInRole("Admin"))
            {
                books = viewRepository.GetBookByIdIncludingDeleted((int)id);
            }
            else
            {
                books = viewRepository.GetBookById((int)id);
            }

            if (books == null)
            {
                return HttpNotFound();
            }
            return View(books);
        }
        /// <summary>
        /// Gets a list of reviews for a book
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // GET: View
        public ActionResult _Reviews(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            List<ReviewViewModel> list = viewRepository.GetReviewsByBookId((int)id);
            return PartialView("_Reviews", list);
        }

        // GET: View/Create
        [Authorize(Roles = "BasicUser,Admin")]
        public ActionResult _Create(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CreateReviewViewModel vm = new CreateReviewViewModel
            {
                Review = new Reviews
                {
                    BookId = (int)id
                },
                User = viewRepository.GetUserByUserId(authentication.GetUserId(User))
            };

            return PartialView("_Create", vm);
        }

        // POST: View/Create
        [Authorize(Roles = "BasicUser,Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult _Create([Bind(Include = "UserId,BookId,Title,Text,Rating")] Reviews review)
        {
            CreateReviewViewModel vm = new CreateReviewViewModel();
            review.UserId = authentication.GetUserId(User);
            vm.User = viewRepository.GetUserByUserId(review.UserId);

            if (ModelState.IsValid)
            {
                viewRepository.Create(review);

                ViewBag.Saved = true;
                vm.Review = new Reviews
                {
                    BookId = review.BookId
                };
                return View(vm);
            }

            vm.Review = review;
            return PartialView(vm);
        }


        // GET: View/Delete/5?Return=/Review/Index
        [Authorize(Roles = "BasicUser,Admin")]
        public ActionResult Delete(int id,string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            Reviews review = viewRepository.GetReviewByReviewId(id);
            if (!authorization.IsAuthorizedToDeleteReview(review, User))
            {
                return new HttpStatusCodeResult(HttpStatusCode.Forbidden);
            }
            return View(review);
        }

        // POST: View/Delete/5
        [Authorize(Roles = "BasicUser,Admin")]
        [HttpPost]
        public ActionResult Delete(int id, string returnUrl,FormCollection collection)
        {
            ViewBag.ReturnUrl = returnUrl;
            Reviews review = viewRepository.GetReviewByReviewId(id);
            if (!authorization.IsAuthorizedToDeleteReview(review, User))
            {
                return new HttpStatusCodeResult(HttpStatusCode.Forbidden);
            }
            viewRepository.DeleteReview(id);
            this.Flash("Success ", "Your review has been successfully deleted.");
            if (string.IsNullOrEmpty(returnUrl)) { 
            return RedirectToAction("Index",new { id=review.BookId });
            }
            else
            {
                return RedirectToLocal(returnUrl);
            }

        }
        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Index", "Home");
        }
    }
}
