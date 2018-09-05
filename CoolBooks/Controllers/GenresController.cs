using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Net;
using System.Web.Mvc;
using CoolBooks.Repositories;
using MvcFlashMessages;

namespace CoolBooks.Models
{
    public class GenresController : Controller
    {
        private GenresRepository repository = new GenresRepository();
        private CoolBooksContext db = new CoolBooksContext();

        // GET: Genres
        [Authorize(Roles = "Admin")]
        public ActionResult Index()
        {
            //List<Genres> genresList = db.Genres.Where(g => g.IsDeleted.Equals(false)).OrderBy(g => g.Name).ToList();
            List<Genres> genresList = repository.GetAll();
            return View(genresList);

        }

        // GET: Genres/Details/5
        [Authorize(Roles = "Admin")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Genres genres = repository.GetGenre(id);
            if (genres == null)
            {
                return HttpNotFound();
            }
            return View(genres);
        }

        // GET: Genres/Create
        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Genres/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Description,Created,IsDeleted")] Genres genres)
        {
            if (ModelState.IsValid)
            {
                if (repository.AllowCreate(genres.Name))
                {
                    repository.CreateGenre(genres);
                    this.Flash("Success ", "Genre has been successfully created.");
                    return RedirectToAction("Index");
                }
                else
                {
                    this.Flash("Error ", "This genre already exists.");
                    return RedirectToAction("Index");
                }
                
            }

            return View(genres);
        }

        // GET: Genres/Edit/5
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Genres genres = repository.GetGenre(id);

            if (genres == null)
            {
                return HttpNotFound();
            }
            return View(genres);
        }

        // POST: Genres/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Description")] Genres genres)
        {
            if (ModelState.IsValid)
            {
                if (repository.AllowEdit(genres.Name, genres.Id))
                {
                    this.Flash("Success ", "Genre has been successfully updated.");
                    return RedirectToAction("Index");
                }
                else
                {
                    this.Flash("Error ", "This genre already exists.");
                    return RedirectToAction("Index");
                }
            }
            return View(genres);
        }

        // GET: Genres/Delete/5
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Genres genres = repository.GetGenre(id);

            if (genres == null)
            {
                return HttpNotFound();
            }
            return View(genres);
        }

        // POST: Genres/Delete/5
        [Authorize(Roles = "Admin")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            // Soft Delete entry
            repository.DeleteGenre(id);

            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
