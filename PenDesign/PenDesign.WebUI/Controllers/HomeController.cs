using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PenDesign.WebUI.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Project()
        {
            return View();
        }

        public ActionResult About()
        {
            return View();
        }

        public ActionResult Video()
        {
            return View();
        }

        public ActionResult Construction()
        {
            return View();
        }

        public ActionResult Blog()
        {
            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}