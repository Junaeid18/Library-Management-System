using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using LMS.Models;

namespace LMS.Controllers
{
    public class UsersController : Controller
    {
        private LMSEntities db = new LMSEntities();

        // GET: Users
        public ActionResult Index()
        {
            if (Session["ID"] != null)
            {
                return View(db.Users.ToList());
            }
            else
                return RedirectToAction("Login");
        }

        public ActionResult RegistrationList()
        {
            if (Session["ID"] != null)
            {
                return View(db.Students.ToList());
            }
            else
                return RedirectToAction("Login");
        }

        public ActionResult Logout()
        {
            if (Session["ID"] != null)
            {

                Session.Abandon();
                return RedirectToAction("Login", "Users");
            }

            return View();
        }

        public ActionResult Studentdetails(int ? id) {
            if (Session["ID"] != null)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Student user = db.Students.Find(id);
                if (user == null)
                {
                    return HttpNotFound();
                }

                return View(user);
            }
            else
                return RedirectToAction("Login","Users");
        }

        // GET: Users/Delete/5
        public ActionResult DeleteStudent(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student user = db.Students.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("DeleteStudent")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteStudentConfirmed(int id)
        {
            Student user = db.Students.Find(id);
            db.Students.Remove(user);
            db.SaveChanges();
            return RedirectToAction("RegistrationList");
        }

        // GET: Users/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // GET: Users/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Users/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Username,password,ID")] User user)
        {
            if (ModelState.IsValid)
            {
                db.Users.Add(user);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(user);
        }

        [HttpGet]
        public ActionResult Login() {

            return View();
        }

        [HttpPost]
        public ActionResult Login(User user)
        {
            using (LMSEntities db = new LMSEntities())
            {
                try
                {
                    var usr = db.Users.Single(u => u.Username == user.Username && u.password == user.password);
                    if (usr != null)
                    {
                        Session["ID"] = usr.ID.ToString();
                        Session["Username"] = usr.Username.ToString();
                        return RedirectToAction("LoginIN");
                    }
                    else
                    {
                        ModelState.AddModelError("", "wrong!!");
                    }
                }
                catch (Exception ex) {
                    ModelState.AddModelError("", "wrong Admin!!");
                }
                
            }
                return View();
        }

        public ActionResult LoginIN()
        {
            if (Session["ID"] != null)
            {
                return View();
            }
            else
                return RedirectToAction("Login");        }
        // GET: Users/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Username,password,ID")] User user)
        {
            if (ModelState.IsValid)
            {
                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(user);
        }

        // GET: Users/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            User user = db.Users.Find(id);
            db.Users.Remove(user);
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
    }
}
