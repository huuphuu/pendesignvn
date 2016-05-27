using PenDesign.Core.Interface.Service.BasicServiceInterface;
using System;
using System.Data.Entity;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PenDesign.Common.Utils;


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
        private IOtherPageSEOService _otherPageSeoService;
        private int LanguageId;

        public HomeController(IConfigService configService, IBannerService bannerService,
                                IBannerMappingService bannerMappingService, IGroupControlService groupControlService, IControlService controlService,
                            IProjectService projectService, INewsService newsService, INewsCategoryService newsCategoryService,
                            IOtherPageSEOService otherPageSeoService)
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
            this._otherPageSeoService = otherPageSeoService;

            this.LanguageId = int.Parse(Cookies.ReadCookie("PenDesign:Language", "129"));
        }

        // ---------------------------------------------------
        // Index Page
        // ---------------------------------------------------
        public ActionResult Index()
        {
            ViewBag.logo = configModel.LogoUrl;
            ViewBag.companyName = configModel.CompanyName;

            var homepageSEOModel = _otherPageSeoService.Get(o => o.Page == "HomePage");
            ViewBag.Keyword = homepageSEOModel.Keyword;
            ViewBag.Description = homepageSEOModel.Description;
            ViewBag.MetaData = homepageSEOModel.MetaData;

            return View();
        }

        [ChildActionOnly]
        public PartialViewResult _Slogan()
        {
            ViewBag.slogan = configModel.Slogan;
            return PartialView("_Slogan");
        }

        [ChildActionOnly]
        public PartialViewResult _Banner()
        {
            var bannerModelTemp = _bannerService.GetMany(b => b.Status == true && b.Deleted == false)
                                                .OrderBy(b => b.ZOrder).AsQueryable();

            if (bannerModelTemp.Count() == 0) return PartialView("_Banner");

            var bannerModel = bannerModelTemp
                                .Select(m => m.BannerMappings.Where(bm => bm.LanguageId == LanguageId));

            return PartialView("_Banner", bannerModel);
        }

        // ---------------------------------------------------
        // HomeLayout
        // ---------------------------------------------------

        [ChildActionOnly]
        public PartialViewResult _Socials(string type)
        {
            var socialsModel = configModel;

            if (type == "Sidebar")
                return PartialView("_SidebarSocial", socialsModel);
            else if (type == "Footer")
                return PartialView("_FooterSocial", socialsModel);

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
            var menuModel = _controlService.GetMany(c => c.GroupControl.Type == "Menu" && c.Status == true && c.Deleted == false)
                //.Include(c => c.ControlMappings)
                                            .Select(c => c.ControlMappings.Where(cm => cm.LanguageId == LanguageId));
            return PartialView("_Header", menuModel);
        }

//        [ChildActionOnly]
        public PartialViewResult _Construction()
        {
            var constructionModel = _newsService.GetMany(n => n.ProjectId == 16 && n.Status == true && n.Deleted == false).Take(24);
            return PartialView("_Construction", constructionModel);
        }

        [ChildActionOnly]
        public PartialViewResult _Footer_Video()
        {
            var videoModel = _projectService.Where(p => p.Id == 15).SingleOrDefault()
                                                                    .ProjectImages.Where(n => n.Status == true && n.Deleted == false)
                                                                    .OrderBy(n => n.ZOrder)
                                                                    .First();
            return PartialView("_Footer_Video", videoModel);
        }

        [ChildActionOnly]
        public PartialViewResult _Footer_News(int newsCategoryId)
        {
            var newsCategoryModel = _newsCategoryService.Get(n => n.Id == newsCategoryId)
                                                            .NewsCategoryMappings
                                                            .SingleOrDefault(nm => nm.LanguageId == LanguageId);
            if (newsCategoryModel == null) return PartialView();

            ViewBag.newsCategoryName = newsCategoryModel.Title;
            ViewBag.newsCategoryId = newsCategoryId;

            var newsModel = _newsService.GetMany(n => n.NewsCategoryId == newsCategoryId && n.Status == true && n.Deleted == false)
                                            .Select(n => n.NewsMappings.Where(nm => nm.LanguageId == LanguageId)).Take(6);
            return PartialView("_Footer_News", newsModel);
        }

        [ChildActionOnly]
        public PartialViewResult _Footer_Construction()
        {
            var constructionModel = _newsService.GetMany(n => n.ProjectId == 16 && n.Status == true && n.Deleted == false)
                                                .Select(m => m.NewsMappings.Where(nm => nm.LanguageId == LanguageId && nm.Status == true && nm.Deleted == false))
                                                .Take(6);
            return PartialView("_Footer_Construction", constructionModel);
        }
        public ActionResult About()
        {
            var homepageSEOModel = _otherPageSeoService.Get(o => o.Page == "About");
            ViewBag.Keyword = homepageSEOModel.Keyword;
            ViewBag.Description = homepageSEOModel.Description;
            ViewBag.MetaData = homepageSEOModel.MetaData;

            ViewBag.About = _configService.GetAll().FirstOrDefault().About;
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
            var homepageSEOModel = _otherPageSeoService.Get(o => o.Page == "Contact");
            ViewBag.Keyword = homepageSEOModel.Keyword;
            ViewBag.Description = homepageSEOModel.Description;
            ViewBag.MetaData = homepageSEOModel.MetaData;

            ViewBag.contactForm = configModel.ContactForm;
            ViewBag.googleMap = configModel.GoogleMap;
            return View();
        }
    }
}