using PenDesign.Core.Interface.Service.BasicServiceInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PenDesign.WebUI.Controllers
{
    public class HomeController : Controller
    {
        private IConfigService _configService;

        public HomeController(IConfigService configService)
        {
            this._configService = configService;
        }

        public ActionResult Index()
        {
            ViewBag.logo = _configService.GetAll().FirstOrDefault().LogoUrl;
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