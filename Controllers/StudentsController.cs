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
    public class StudentsController : Controller
    {
        private LMSEntities db = new LMSEntities();

        // GET: Students
        public ActionResult Index()
        {
            if (Session["SID"] != null)
            {
                return View(db.Students.ToList());
            }
            else
                return RedirectToAction("Login", "Students");
        }
        

        public ActionResult Search(String s)
        {
            return View(db.Books.Where(  x => x.Book_title.Contains(s) ).ToList());
            /*  var book = from c in db.Books select c;
              if (!String.IsNullOrEmpty(s)) {
                  book =book.Where(c => c.Book_title.Contains(s));
              }
              return View(book);*/

        }

        public ActionResult ser(String s1)
        {
            return View(db.Books.Where(x=>x.Book_title.Contains(s1)||s1==null).ToList());
        }

        [HttpGet]
        public ActionResult Login() {
            return View();
        }

        [HttpPost]

        public ActionResult Login(Student ur)
        {
            using (LMSEntities db = new LMSEntities())
            {
                try
                {
                    var usr = db.Students.Single(u => u.name == ur.name && u.password == ur.password);
                    if (usr != null)
                    {
                        Session["SID"] = usr.id.ToString();
                        Session["Sname"] = usr.name.ToString();
                        return RedirectToAction("BookListStudent");
                    }
                    else
                    {
                        ModelState.AddModelError("", "wrong!!");
                        return View();
                    }
                }
                catch (Exception) { }
               
            }
           return View();
            
        }

        [HttpGet]
        public ActionResult BookListStudent()
        {
            if (Session["SID"] != null)
            {
                return View(db.Books.ToList());
            }

            else
                return RedirectToAction("Login","Students");

        }
        public ActionResult LoginIN()
        {
            if (Session["ID"] != null)
            {
                return View();
            }
            else
                return RedirectToAction("Login");
        }

        // GET: Students/Details/5
        public ActionResult Details(int? id)
        {
            if (Session["SID"] != null)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Student student = db.Students.Find(id);
                if (student == null)
                {
                    return HttpNotFound();
                }
                return View(student);
            }
            else
                return RedirectToAction("Login", "Students");
        }

        // GET: Students/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Students/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,name,password,enrollment,email,contact,address")] Student student)
        {
            if (ModelState.IsValid)
            {
                db.Students.Add(student);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(student);
        }
        public ActionResult Logout() {
            if ( Session["SID"]!=null)
            {
               
                Session.Abandon();
                return RedirectToAction("Login","Students");
            }

            return View();
        }

        // GET: Students/Edit/5
        public ActionResult Edit(int? id)
        {
            if (Session["SID"] != null)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Student student = db.Students.Find(id);
                if (student == null)
                {
                    return HttpNotFound();
                }
                return View(student);
            }
            return RedirectToAction("Login","Students");
        }

        // POST: Students/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,name,password,enrollment,email,contact,address")] Student student)
        {
            if (ModelState.IsValid)
            {
                db.Entry(student).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(student);
        }

        // GET: Students/Delete/5
     /*   public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student student = db.Students.Find(id);
            if (student == null)
            {
                return HttpNotFound();
            }
            return View(student);
        }

        // POST: Students/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Student student = db.Students.Find(id);
            db.Students.Remove(student);
            db.SaveChanges();
            return RedirectToAction("Index");
        }*/

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
