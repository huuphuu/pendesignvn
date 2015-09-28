using PenDesign.Common.Utils;
using PenDesign.Core.Interface.Service.BasicServiceInterface;
using PenDesign.WebUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PenDesign.WebUI.Infrastructure;

namespace PenDesign.WebUI.Controllers
{
    public class ConstructionController : Controller
    {
        private IProjectService _projectService;
        private IProjectMappingService _projectMappingService;

        private IProjectImageService _projectImageService;
        private IProjectImageMappingService _projectImageMappingService;

        private INewsService _newsService;
        private INewsMappingService _newsMappingService;

        private int ItemPerPage;
        private IOtherPageSEOService _otherPageSeoService;
        private int LanguageId;


        public ConstructionController(IProjectService projectService, IProjectMappingService projectMappingService,
                                 IProjectImageService projectImageService, IProjectImageMappingService projectImageMappingService,
                                    INewsService newsService, INewsMappingService newsMappingService,
                                    IOtherPageSEOService otherPageSeoService)
        {
            this._projectService = projectService;
            this._projectMappingService = projectMappingService;

            this._projectImageService = projectImageService;
            this._projectImageMappingService = projectImageMappingService;

            this._newsService = newsService;
            this._newsMappingService = newsMappingService;

            this._otherPageSeoService = otherPageSeoService;

            ItemPerPage = AppSettings.ItemsPerPage;

            LanguageId = int.Parse(Cookies.ReadCookie("PenDesign:Language", "129"));
        }
        // GET: Construction
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult List(int page = 1)
        {
            var otherPageSEOModel = _otherPageSeoService.Get(o => o.Page == "Construction");
            ViewBag.Keyword = otherPageSEOModel.Keyword;
            ViewBag.Description = otherPageSEOModel.Description;
            ViewBag.MetaData = otherPageSEOModel.MetaData;

            var ConstructionVM = new ConstructionVM();
            ConstructionVM.PagingItems = _newsService.Page(n => n.ProjectId == 16 && n.Status == 0, n => n.ZOrder, page, ItemPerPage, true);
            ConstructionVM.News = ConstructionVM.PagingItems.Entities;
            return View(ConstructionVM);
        }

        public ActionResult Detail(int id)
        {
            var newsModel = _newsService.Get(n => n.Id == id && n.Status == 0)
                                        .NewsMappings
                                        .SingleOrDefault(nm => nm.LanguageId == LanguageId && nm.Status == 0);

            return View(newsModel);
        }
    }
}