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
        
        private INewsService _newsService;
        private IQueryable<Core.Model.Project> _projectService;
        private INewsCategoryService _newsCategoryService;

        public HomeController(IConfigService configService, IBannerService bannerService, 
                                IBannerMappingService bannerMappingService, IGroupControlService groupControlService, IControlService controlService,
                            IProjectService projectService, INewsService newsService, INewsCategoryService newsCategoryService)
        {
            this._configService = configService;
            this._bannerService = bannerService;
            this._bannerMappingService = bannerMappingService;
            this._groupControlService = groupControlService;
            this._controlService = controlService;
            this.configModel = _configService.GetAll().SingleOrDefault();
            this._projectService = projectService.GetAll();
            this._newsService = newsService;
            this._newsCategoryService = newsCategoryService;

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
                                            .Select(m => m.BannerMappings.Where(bm => bm.LanguageId == 129));

            return PartialView("_Banner", bannerModel);
        }

        // ---------------------------------------------------
        // HomeLayout
        // ---------------------------------------------------

        [ChildActionOnly]
        public PartialViewResult _Socials(string type)
        {
            if(type == "Sidebar")
                return PartialView("_SidebarSocial");
            else if(type == "Footer") 
                return PartialView("_FooterSocial");

            return PartialView();
        }

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
            var menuModel = _controlService.GetMany(c => c.GroupControl.Type == "Menu" && c.Status == 0)
                                            //.Include(c => c.ControlMappings)
                                            .Select(c => c.ControlMappings.Where(cm => cm.LanguageId == 129));
            return PartialView("_Header", menuModel);
        }

        [ChildActionOnly]
        public PartialViewResult _Construction()
        {
            var constructionModel = _newsService.GetMany(n => n.ProjectId == 16 && n.Status == 0).Take(24);
            return PartialView("_Construction", constructionModel);
        }

        [ChildActionOnly]
        public PartialViewResult _Footer_Video()
        {
            var videoModel = _projectService.Where(p => p.Id == 15).SingleOrDefault()
                                                                    .ProjectImages.Where(n => n.Status == 0)
                                                                    .OrderBy(n => n.ZOrder)
                                                                    .First();
            return PartialView("_Footer_Video", videoModel);
        }

        [ChildActionOnly]
        public PartialViewResult _Footer_News(int newsCategoryId)
        {
            var newsCategoryModel = _newsCategoryService.Get(n => n.Id == newsCategoryId)
                                                            .NewsCategoryMappings
                                                            .SingleOrDefault(nm => nm.LanguageId == 129);
            if (newsCategoryModel == null) return PartialView();

            ViewBag.newsCategoryName = newsCategoryModel.Title;
            ViewBag.newsCategoryId = newsCategoryId;

            var newsModel = _newsService.GetMany(n => n.NewsCategoryId == newsCategoryId && n.Status == 0)
                                            .Select(n => n.NewsMappings.Where(nm => nm.LanguageId == 129)).Take(6);
            return PartialView("_Footer_News",newsModel);
        }

        [ChildActionOnly]
        public PartialViewResult _Footer_Construction()
        {
            var constructionModel = _newsService.GetMany(n => n.ProjectId == 16 && n.Status == 0)
                                                .Select(m => m.NewsMappings.Where(nm => nm.LanguageId == 129 && nm.Status == 0))
                                                .Take(6);
            return PartialView("_Footer_Construction",constructionModel);
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