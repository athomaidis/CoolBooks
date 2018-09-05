using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CoolBooks.Repositories;
using CoolBooks.Models;

namespace CoolBooks.Controllers
{
    public class HomeController : Controller
    {
        private IBookRepository bookRepository = new BookRepository();

        public ActionResult Index()
        {
            List<Books> bookList = bookRepository.GetTopPopular(12);
            List<Books> bookListLatest = bookRepository.GetLatestCreated(8);
            ViewBag.LatestBooks = bookListLatest;
            ViewBag.TopBook = bookList[0];
            return View(bookList);
        }

        public ActionResult About()
        {
            ViewBag.Message = "CoolBooks lets you review books.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Team()
        {
            ViewBag.Message = "Our Team.";

            return View();
        }
    }
}