using PenDesign.Core.Interface.Service.BasicServiceInterface;
using PenDesign.Core.ViewModel;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PenDesign.WebUI.Models;
using PenDesign.Common.Utils;
using PenDesign.WebUI.Infrastructure;

namespace PenDesign.WebUI.Controllers
{
    public class ProjectController : Controller
    {
        private IProjectService _projectService;
        private IProjectMappingService _projectMappingService;

        private IProjectImageService _projectImageService;
        private IProjectImageMappingService _projectImageMappingService;
        private IOtherPageSEOService _otherPageSeoService;
        private int LanguageId;

        public int ItemPerPage { get; set; }


        public ProjectController(IProjectService projectService, IProjectMappingService projectMappingService,
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

        // GET: Project
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult List(int page = 1)
        {
            var otherPageSEOModel = _otherPageSeoService.Get(o => o.Page == "Project");
            ViewBag.Keyword = otherPageSEOModel.Keyword;
            ViewBag.Description = otherPageSEOModel.Description;
            ViewBag.MetaData = otherPageSEOModel.MetaData;

            var ProjectVM = new ProjectVM();
            ProjectVM.PagingItems = _projectService.Page(p => p.Status == true && p.Deleted == false && p.Type == 1, p => p.ZOrder, page, ItemPerPage, true);
            ProjectVM.Project =  ProjectVM.PagingItems.Entities;
            return View(ProjectVM);
        }

        public ActionResult Detail(int id)
        {
            var projectModel = _projectService.Get(p => p.Id == id);
            if(projectModel == null)  return View();

            ViewBag.projectName = projectModel.ProjectMappings.SingleOrDefault(pm => pm.LanguageId == LanguageId && pm.Status == true && pm.Deleted == false).Title;

            var newsModel = projectModel.News.SingleOrDefault(n => n.ProjectId == id).NewsMappings.Where(nm => nm.LanguageId == LanguageId).SingleOrDefault();

            return View(newsModel);
        }

    }
}