using PenDesign.Core.Interface.Service.BasicServiceInterface;
using System;
using System.Data.Entity;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PenDesign.WebUI.Controllers
{
    public class HomeController : Controller
    {
        private IConfigService _configService;
        private IBannerService _bannerService;
        private IBannerMappingService _bannerMappingService;

        public HomeController(IConfigService configService, IBannerService bannerService, IBannerMappingService bannerMappingService)
        {
            this._configService = configService;
            this._bannerService = bannerService;
            this._bannerMappingService = bannerMappingService;

        }

        public ActionResult Index()
        {
            var configModel = _configService.GetAll().SingleOrDefault();
            ViewBag.logo = configModel.LogoUrl;
            ViewBag.slogan = configModel.Slogan;
            return View();
        }

        [ChildActionOnly]
        public PartialViewResult _Banner()
        {
            var bannerModel = _bannerService.GetMany(b => b.Status == 0)
                .Include(b => b.BannerMappings)
                .Select(m => m.BannerMappings.Where(bm => bm.LanguageId == 129));

            return PartialView("_Banner", bannerModel);
        }

        [ChildActionOnly]
        public PartialViewResult _Footer()
        {
            ViewBag.footerContent = _configService.GetAll().FirstOrDefault().FooterContent;
            return PartialView("_Footer");
        }

        public ActionResult About()
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
            var configModel = _configService.GetAll().SingleOrDefault();
            ViewBag.contactForm = configModel.ContactForm;
            ViewBag.googleMap = configModel.GoogleMap;
            return View();
        }
    }
}