using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CoolBooks.Controllers;
using CoolBooks.Repositories;
using Microsoft.AspNet.Identity;
using MvcFlashMessages;
using CoolBooks.ViewModels;
using System.Collections.Generic;
using System;
using Microsoft.AspNet.Identity.EntityFramework;

namespace CoolBooks.Models
{
    public class UsersController : Controller
    {
        private CoolBooksContext db = new CoolBooksContext();
        private UserRepository repository = new UserRepository();

        // GET: Users
        [Authorize(Roles = "Admin")]
        public ActionResult Index()
        {
            List<Users> users = db.Users.ToList();
            Session["UserIdNr"] = CreateUserIdNr(users);
            return View(users);
        }

        
        // GET: Users/Details/5
        [Authorize(Roles = "Admin")]
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Users users = db.Users.Find(id);
            if (users == null)
            {
                return HttpNotFound();
            }
            return View("Index", users);
        }
       
        // Edit User Profile
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "UserId,FirstName,LastName,Address,Zipcode,City,Email,Picture,Country")]Users users, HttpPostedFileBase file)
        {
            if (ModelState.IsValid && file != null)
            {
                using (MemoryStream ms = new MemoryStream())
                {
                    file.InputStream.CopyTo(ms);
                    byte[] array = ms.GetBuffer();
                    users.Image = array;
                    users.ImagePath = file.FileName;
                    users.MimeType = file.ContentType;
                }

                db.Entry(users).State = EntityState.Modified;
                users.Created = System.DateTime.Now;
                db.SaveChanges();

                // Set session flag for user-image
                if (users.Image != null)
                {
                    Session["UserImage"] = true;
                }
                Session["FullName"] = users.FirstName + " " + users.LastName;

                this.Flash("Success", "Your profile has been successfully updated.");
                return RedirectToAction("Index", "Manage");

            }else if (ModelState.IsValid)
            {
                // Use image already stored in db
                Users storedUser = db.Users.Find(users.UserId);
                users.Image = storedUser.Image;
                users.MimeType = storedUser.MimeType;
                users.ImagePath = storedUser.ImagePath;

                using (var context = new CoolBooksContext()) {
                    users.Created = System.DateTime.Now;
                    context.Entry(users).State = EntityState.Modified;
                    context.SaveChanges();
                }
                Session["FullName"] = users.FirstName + " " + users.LastName;
                this.Flash("Success", "Your profile has been successfully updated.");
                return RedirectToAction("Index", "Manage");

            }

            return RedirectToAction("Index", "Manage");

        }


        public ActionResult GetImage(string id)
        {

            var user = repository.GetById(id);
            if (user == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);
            }
            if (user.Image == null)
            {
                return RedirectToLocal("~/Images/dummyuserimage.jpg");
            }

            using (System.IO.MemoryStream ms = new System.IO.MemoryStream())
            {
                byte[] img = user.Image;
                if (img != null)
                {
                    ms.Write(img, 0, img.Length);
                    return File(ms.ToArray(), user.MimeType);
                }
                else {
                    return RedirectToLocal("~/Images/dummyuserimage.jpg");
                }
            }
        }

        public ActionResult GetCurrentUserImage()
        {
            Authentication authentication = new Authentication();
            string id = authentication.GetUserId(User);
            var user = repository.GetById(id);
            if (user == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);
            }
            if (user.Image == null)
            {
                return RedirectToLocal("~/Images/dummyuserimage.jpg");
            }

            using (System.IO.MemoryStream ms = new System.IO.MemoryStream())
            {
                byte[] img = user.Image;
                if (img != null)
                {
                    ms.Write(img, 0, img.Length);
                    return File(ms.ToArray(), user.MimeType);
                }
                else {
                    return RedirectToLocal("~/Images/dummyuserimage.jpg");
                }
            }
        }

        public ActionResult GetImageById(int id)
        {
            var userIds = (Dictionary<string, int>) Session["UserIdNr"];
            if (userIds == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError);
            }
            string userId=null;
            foreach (var item in userIds)
            {
                if (item.Value == id)
                {
                    userId = item.Key;
                    break;
                }
            }

            var user = repository.GetById(userId);
            if (user == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);
            }
            if (user.Image == null)
            {
                return RedirectToLocal("~/Images/dummyuserimage.jpg");
            }

            using (System.IO.MemoryStream ms = new System.IO.MemoryStream())
            {
                byte[] img = user.Image;
                if (img != null)
                {
                    ms.Write(img, 0, img.Length);
                    return File(ms.ToArray(), user.MimeType);
                }
                else {
                    return RedirectToLocal("~/Images/dummyuserimage.jpg");
                }
            }
        }

        // *******************************************************
        // Returns partial view with user profile data
        public PartialViewResult MyDetails(string id)
        {
            /*if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }*/

            Users users = db.Users.Find(id);
            
            /*if (users == null)
            {
                return HttpNotFound();
            }*/
            return PartialView(users);
        }

        // GET: Users/Create
        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Users/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "UserId,FirstName,LastName,Gender,Birthdate,Phone,Address,ZipCode,City,Country,Email,Info")] Users users, HttpPostedFileBase file)
        {
            var appContext = new CoolBooks.Models.ApplicationDbContext();

            users.IsDeleted = false;
            users.Created = DateTime.Now;

            if (ModelState.IsValid)
            {
                if (file != null)
                {
                    using (MemoryStream ms = new MemoryStream())
                    {
                        file.InputStream.CopyTo(ms);
                        byte[] array = ms.GetBuffer();
                        users.Image = array;
                        users.ImagePath = file.FileName;
                        users.MimeType = file.ContentType;
                    }
                }
                var appUser = new ApplicationUser { UserName = users.Email, Email = users.Email, PhoneNumber=users.Phone};
                var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(appContext));
                var result = userManager.Create(appUser,users.Email);  // Use user email as password. not so safe.

                if (result.Succeeded)
                {
                    users.UserId = appUser.Id;
                    userManager.AddToRole(users.UserId, "BasicUser");

                    
                    db.Users.Add(users);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }

            return View(users);
        }

        // GET: Users/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Users users = db.Users.Find(id);
            if (users == null)
            {
                return HttpNotFound();
            }
            return View(users);
        }

        // ********************************************************
        // Returns partial view with user profile edit
        public ActionResult MyEdit(string id)
        {
            /*if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }*/
            Users users = db.Users.Find(id);

            /*if (users == null)
            {
                return HttpNotFound();
            }*/

            return View(users);
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
     /*   [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "UserId,FirstName,LastName,Gender,Birthdate,Picture,Phone,Address,ZipCode,City,Country,Email,Info,Created,IsDeleted")] Users users)
        {
            if (ModelState.IsValid)
            {
                db.Entry(users).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(users);
        }*/

        // GET: Users/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Users users = db.Users.Find(id);
            if (users == null)
            {
                return HttpNotFound();
            }
            return View(users);
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Users users = db.Users.Find(id);
            db.Users.Remove(users);
            db.SaveChanges();
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

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Index", "Home");
        }

        private Dictionary<string, int> CreateUserIdNr(List<Users> users)
        {
            int i = 0;
            Dictionary<string, int> dict = new Dictionary<string, int>();
            foreach (var user in users)
            {
                    if (!dict.ContainsKey(user.UserId))
                    {
                        i++;
                        dict.Add(user.UserId, i);
                    }
                
            }
            return dict;
        }
    }
}
