using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CoolBooks.Models;
using CoolBooks.Repositories;
using MvcFlashMessages;

namespace CoolBooks.Controllers
{
    
    public class AuthorsController : Controller
    {
        private AuthorsRepository repository = new AuthorsRepository();
        private CoolBooksContext db = new CoolBooksContext();

        // GET: Authors
        [Authorize(Roles = "Admin")]
        public ActionResult Index()
        {
            var authors = db.Authors.Where(c => c.IsDeleted == false).ToList();
            return View(authors);
        }

        // Get Create
        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            return View("Create");
        }

        // Post Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult Create(Authors authors)
        {
            if (ModelState.IsValid)
            {
                if (repository.CreateAuthor(authors) == true)
                {
                    this.Flash("Success ", "Your author successfully created.");
                    return RedirectToAction("Index");
                }
                this.Flash("Error", "Author name already exist with the same name. Please try with another name.");
                return RedirectToAction("Index");

            }
            return View("Create");
        }

        // Get Author Edit
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var author = repository.GetAuthor(id);
            if (author == null)
            {
                return HttpNotFound();
            }
            return View("Edit", author);
        }

        // Edit Post Request

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult Edit([Bind(Include = "Id,FirstName,LastName,Description,Created")]Authors authors)
        {
            if (ModelState.IsValid)
            {
                if (repository.EditAuthor(authors) == true)
                {
                    this.Flash("Success", "Your author has been successfully updated.");
                    return RedirectToAction("Index");
                }
                this.Flash("Error", "Author name already exist with the same name. Please try with another name.");
                return RedirectToAction("Index");

            }
            return View("Edit");
        }

        // Get Author Details
        [Authorize(Roles = "Admin")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var author = repository.GetAuthor(id);
            return View("Details", author);
        }

    }
}