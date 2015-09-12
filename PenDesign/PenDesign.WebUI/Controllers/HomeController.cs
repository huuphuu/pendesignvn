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
        private readonly Core.Model.Config configModel;
        private IGroupControlService _groupControlService;
        private IControlService _controlService;

        public HomeController(IConfigService configService, IBannerService bannerService, 
                                IBannerMappingService bannerMappingService, IGroupControlService groupControlService, IControlService controlService)
        {
            this._configService = configService;
            this._bannerService = bannerService;
            this._bannerMappingService = bannerMappingService;
            this._groupControlService = groupControlService;
            this._controlService = controlService;
            this.configModel = _configService.GetAll().SingleOrDefault();
        }

        // ---------------------------------------------------
        // Index Page
        // ---------------------------------------------------
        public ActionResult Index()
        {
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

        // ---------------------------------------------------
        // HomeLayout
        // ---------------------------------------------------

        [ChildActionOnly]
        public PartialViewResult _Footer()
        {
            ViewBag.footerContent = configModel.FooterContent;
            return PartialView("_Footer");
        }

        [ChildActionOnly]
        public PartialViewResult _Header()
        {
            ViewBag.logo = configModel.LogoUrl;
            ViewBag.companyName = configModel.CompanyName;
            //var menuModel = _groupControlService.GetMany(m => m.Type == "Menu" && m.Status == 0)
            //                                    .Select(m => m.Controls.Where(c => c.Status == 0));
            var menuModel = _controlService.GetMany(c => c.GroupControl.Type == "Menu" && c.Status == 0)
                                            .Include(c => c.ControlMappings)
                                            .Select(c => c.ControlMappings.Where(cm => cm.LanguageId == 129));
            return PartialView("_Header", menuModel);
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
            ViewBag.contactForm = configModel.ContactForm;
            ViewBag.googleMap = configModel.GoogleMap;
            return View();
        }
    }
}