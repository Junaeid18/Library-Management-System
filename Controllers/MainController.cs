using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.SqlClient;

namespace Library_Management_System.Controllers
{
    public class MainController : Controller
    {
        SqlConnection con = new SqlConnection("Data Source=DESKTOP-6TVJUQ6/SQLEXPRESS;Initial Catalog=Library;Integrated Security=True");
        // GET: Main
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult signin()
        {
            ViewBag.Msg = "Enter Your ID and Password to log in";
            return View();
        }
        public ActionResult signup()
        {
            return View();
        }

        public ActionResult profile()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Book()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}