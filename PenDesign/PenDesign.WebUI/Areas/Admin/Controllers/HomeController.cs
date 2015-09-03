using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;


namespace PenDesign.WebUI.Areas.Admin.Controllers
{
    
    public class HomeController : Controller
    {
        // GET: Admin/Home
        public ActionResult Index()
        {
            if (!User.Identity.IsAuthenticated)
                return RedirectToAction("Login");
            return View();
        }

        public ActionResult Login()
        {
            return View();
        }
        //protected override void Dispose(bool disposing)
        //{
        //    if (disposing)
        //    {
        //        db.Dispose();
        //    }
        //    base.Dispose(disposing);
        //}
    }
}