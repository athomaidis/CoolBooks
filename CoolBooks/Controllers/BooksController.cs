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
using MvcFlashMessages;
using CoolBooks;
using System.IO;

namespace CoolBooks.Controllers
{
    public class BooksController : Controller
    {
        private IAuthentication authentication;
        private IAuthorization authorization;
        private IBookFacade bookRepository;

        /// <summary>
        /// Default constructor
        /// </summary>
        public BooksController()
        {
            authentication = new Authentication();
            authorization = new Authorization(authentication);
            bookRepository = new BookFacade();

            string userId = authentication.GetUserId(User);

        }
        /// <summary>
        /// Constructor for unit testing
        /// </summary>
        /// <param name="factory">Object responsible for creating objects</param>
        public BooksController(IAuthentication authentication, IAuthorization authorization, IBookFacade bookRepository)
        {
            this.authentication = authentication;
            this.authorization = authorization;
            this.bookRepository = bookRepository;
        }

        // GET: Books/Enokssen
        public ActionResult Index(string searchString)
        {
            List<Books> booksList;
            if (User!=null && (User.IsInRole("BasicUser") )) {
                    booksList = bookRepository.SearchSortByUserId(authentication.GetUserId(User),searchString);
            }
            else if (User != null && (User.IsInRole("Admin")))
            {
                booksList = bookRepository.SearchSortByUserIdIncludingDeleted(authentication.GetUserId(User), searchString);
            }
            else {
                    booksList = bookRepository.Search(searchString);
            }
            return View(booksList);
        }

        // GET: Books/Enokssen
        public ActionResult TopBooks(string searchString)
        {
            List<Books> booksList;
            booksList = bookRepository.GetTopPopular(10);

            return View(booksList);
        }

        [Authorize(Roles ="BasicUser,Admin")]
        // GET: Books/Create
        public ActionResult Create()
        {
            ViewBag.UserId = authentication.GetUserId(User);
            ViewBag.AuthorId = new SelectList(bookRepository.GetAllAuthors(), "Id", "FullName");
            ViewBag.GenreId = new SelectList(bookRepository.GetAllGenres(), "Id", "Name");
            Books book = new Books();
            return View(book);
        }

        // POST: Books/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "BasicUser,Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,UserId,AuthorId,GenreId,Title,AlternativeTitle,Part,Description,ISBN,PublishDate")] Books books, HttpPostedFileBase file)
        {
            books.UserId=authentication.GetUserId(User);
            
            if (ModelState.IsValid && file != null)
            {
                using (MemoryStream ms = new MemoryStream())
                {
                    file.InputStream.CopyTo(ms);
                    byte[] array = ms.GetBuffer();
                    books.Image = array;
                    books.ImagePath = file.FileName;
                    books.MimeType = file.ContentType;
                }
                bookRepository.Create(books);
                this.Flash("Success ", "Your book has been successfully created.");
                return RedirectToAction("Index");
            }
            else if(ModelState.IsValid)
            {
                bookRepository.Create(books);
                this.Flash("Success ", "Your book has been successfully created.");
                return RedirectToAction("Index");
            }

            ViewBag.UserId = books.UserId;
            ViewBag.AuthorId = new SelectList(bookRepository.GetAllAuthors(), "Id", "FullName");
            ViewBag.GenreId = new SelectList(bookRepository.GetAllGenres(), "Id", "Name");
            return View(books);
        }

        // GET: Books/Edit/5
        [Authorize(Roles = "BasicUser,Admin")]
        public ActionResult Edit(int? id,string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Books books = bookRepository.GetByIdIncludingDeleted((int)id);
            if (books == null)
            {
                return HttpNotFound();
            }
            if (!authorization.IsAuthorizedToEditBook(books, User))
            {
                return new HttpStatusCodeResult(HttpStatusCode.Forbidden);
            }

            ViewBag.AuthorId = new SelectList(bookRepository.GetAllAuthors(), "Id", "FullName",books.AuthorId);
            ViewBag.GenreId = new SelectList(bookRepository.GetAllGenres(), "Id", "Name",books.GenreId);
            return View(books);
        }

        // POST: Books/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "BasicUser,Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,UserId,AuthorId,GenreId,Title,AlternativeTitle,Part,Description,ISBN,PublishDate,Created,IsDeleted")] Books books, string returnUrl, HttpPostedFileBase file)
        {
            ViewBag.ReturnUrl = returnUrl;
            if (!authorization.IsAuthorizedToEditBook(books, User))
            {
                return new HttpStatusCodeResult(HttpStatusCode.Forbidden);
            }

            if (ModelState.IsValid && file != null)
            {
                using (MemoryStream ms = new MemoryStream())
                {
                    file.InputStream.CopyTo(ms);
                    byte[] array = ms.GetBuffer();
                    books.Image = array;
                    books.ImagePath = file.FileName;
                    books.MimeType = file.ContentType;
                }
                bookRepository.Update(books);
                this.Flash("Success ", "Your book has been successfully updated.");
                if (string.IsNullOrEmpty(returnUrl))
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    return RedirectToLocal(returnUrl);
                }

            }
            else
            {
                // If user edited properties and didn´t select a image file to upload
                if(ModelState.IsValid)
                {
                    // Reuse image properties already stored in db.
                    Books storedBook = bookRepository.GetByIdIncludingDeleted(books.Id);
                    books.Image = storedBook.Image;
                    books.ImagePath = storedBook.ImagePath;
                    books.MimeType=storedBook.MimeType;

                    bookRepository.Update(books);
                    this.Flash("Success ", "Your book has been successfully updated.");
                    if (string.IsNullOrEmpty(returnUrl))
                    {
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        return RedirectToLocal(returnUrl);
                    }

                }

            }
            ViewBag.AuthorId = new SelectList(bookRepository.GetAllAuthors(), "Id", "FullName");
            ViewBag.GenreId = new SelectList(bookRepository.GetAllGenres(), "Id", "Name");
            return View(books);
        }

        /// <summary>
        /// Gets an book image from database
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult GetImage(int id)
        {

            Books book;
            // Admin allowed to see deleted book images
            if (User.IsInRole("Admin"))
            {
                book = bookRepository.GetByIdIncludingDeleted(id);
            }
            else
            {
                book = bookRepository.GetById(id);
            }
            if (book== null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);
            }
            if (book.Image == null)
            {
                return RedirectToLocal("~/Images/ImageNotFound.png");
            }

            using (System.IO.MemoryStream ms = new System.IO.MemoryStream())
            {
                byte[] img = book.Image;
                if (img != null)
                {
                    ms.Write(img, 0, img.Length);
                    return File(ms.ToArray(), book.MimeType);
                }
                else
                {
                    return RedirectToLocal("~/Images/ImageNotFound.png");
                }
            }

        }

        // GET: Books/Delete/5
        [Authorize(Roles = "BasicUser,Admin")]
        public ActionResult Delete(int? id,string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Books books = bookRepository.GetById((int)id);
            if (books == null)
            {
                return HttpNotFound();
            }
            if (!authorization.IsAuthorizedToDeleteBook(books, User))
            {
                return new HttpStatusCodeResult(HttpStatusCode.Forbidden);
            }

            return View(books);
        }

        // POST: Books/Delete/5
        [Authorize(Roles = "BasicUser,Admin")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id,string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            Books books = bookRepository.GetById((int)id);

            if (!authorization.IsAuthorizedToDeleteBook(books, User))
            {
                return new HttpStatusCodeResult(HttpStatusCode.Forbidden);
            }
            bookRepository.Delete((int)id);
            this.Flash("Success ", "Your book has been successfully deleted.");
            
            if (string.IsNullOrEmpty(returnUrl))
            {
                return RedirectToAction("Index");
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
