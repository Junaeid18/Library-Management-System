using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Library_Management_System.Models;
using System.Web.Security;
namespace Library_Management_System.Controllers
{
    public class AccountController : Controller
    {
        // GET: Account
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(Librarian model)
        {
            using (var context = new LibraryEntities())
            {
                bool valid = context.Librarians.Any(x => x.l_id == model.l_id && x.l_password == model.l_password);
                if(valid)
                {
                    FormsAuthentication.SetAuthCookie(model.l_name, false);
                    return RedirectToAction("Index","Librarians");
                }
                else{
                    ModelState.AddModelError("", "Invalid Id & UserName");
                    return View();
                }
            }    
            return View();
        }
    }
}