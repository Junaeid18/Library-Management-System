using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LMS.Models;
using System.Data.Entity;
using System.Net;
using System.Web.UI.WebControls;

namespace LMS.Controllers
{
    public class BooksController : Controller
    {

        private LMSEntities db = new LMSEntities();
        
        // GET: Books
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult Add() {
            if (Session["ID"] != null)
            {
                return View();
            }
            else
                return RedirectToAction("Login","Users");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add([Bind(Include ="Bid,Book_title,Book_arthor,Book_image,Book_isbn,available")] Book book)
        {
            if (ModelState.IsValid)
            {
                db.Books.Add(book);
                db.SaveChanges();
                return RedirectToAction("List");
            }

            return View();
        }
        [HttpGet]
        public ActionResult List()
        {
            if (Session["ID"] != null)
            {
                return View(db.Books.ToList());
            }

            else
               return RedirectToAction("Login", "Users");
           
        }

    }
}