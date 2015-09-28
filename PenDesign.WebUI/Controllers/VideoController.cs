using PenDesign.Common.Utils;
using PenDesign.Core.Interface.Service.BasicServiceInterface;
using PenDesign.WebUI.Infrastructure;
using PenDesign.WebUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PenDesign.WebUI.Controllers
{
    public class VideoController : Controller
    {
        private IProjectService _projectService;
        private IProjectMappingService _projectMappingService;
        private IProjectImageService _projectImageService;
        private IProjectImageMappingService _projectImageMappingService;
        private IOtherPageSEOService _otherPageSeoService;
        private int LanguageId;
        public int ItemPerPage { get; set; }

        public VideoController(IProjectService projectService, IProjectMappingService projectMappingService,
                                 IProjectImageService projectImageService, IProjectImageMappingService projectImageMappingService,
                                IOtherPageSEOService otherPageSeoService)
        {
            this._projectService = projectService;
            this._projectMappingService = projectMappingService;

            this._projectImageService = projectImageService;
            this._projectImageMappingService = projectImageMappingService;

            this._otherPageSeoService = otherPageSeoService;

            this.LanguageId = int.Parse(Cookies.ReadCookie("PenDesign:Language", "129"));

            ItemPerPage = AppSettings.ItemsPerPage;
        }
        // GET: Video
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult List(int page = 1)
        {
            var otherPageSEOModel = _otherPageSeoService.Get(o => o.Page == "Video");
            ViewBag.Keyword = otherPageSEOModel.Keyword;
            ViewBag.Description = otherPageSEOModel.Description;
            ViewBag.MetaData = otherPageSEOModel.MetaData;

            var VideoVM = new VideoVM();
            VideoVM.PagingItems = _projectImageService.Page(p => p.Status == 0 && p.Type == 2, p => p.ZOrder, page, ItemPerPage, true);
            VideoVM.ProjectImages = VideoVM.PagingItems.Entities;
            return View(VideoVM);
        }


    }
}