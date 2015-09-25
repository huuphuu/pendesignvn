using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PenDesign.Core.Interface.Service.BasicServiceInterface;
using PenDesign.WebUI.Models;

namespace PenDesign.WebUI.Controllers
{
    public class NewsController : Controller
    {
        private INewsCategoryService _newsCategoryService;
        private INewsService _newsService;
        private int ItemPerPage;

        public NewsController(INewsCategoryService newsCategoryService, INewsService newsService
            )
        {
            this._newsCategoryService = newsCategoryService;
            this._newsService = newsService;

            ItemPerPage = 6;
        }

        // GET: News
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ListByTrends(int? newsCategoryId, int page = 1)
        {
            if (newsCategoryId == null)
                return View();

            var newsCategoryModel = _newsCategoryService.Get(n => n.Id == newsCategoryId);
            ViewBag.newsCategoryName = newsCategoryModel.NewsCategoryMappings.SingleOrDefault(n => n.LanguageId == 129).Title;

            var NewsVM = new NewsVM();
            NewsVM.PagingItems = _newsService.Page(n => n.NewsCategoryId == newsCategoryId && n.Status == 0, n => n.ZOrder, page, ItemPerPage, true);
            NewsVM.News = NewsVM.PagingItems.Entities;
            return View(NewsVM);
        }

        public ActionResult ListByCustomers(int? newsCategoryId, int page = 1)
        {
            if (newsCategoryId == null)
                return View();

            var newsCategoryModel = _newsCategoryService.Get(n => n.Id == newsCategoryId);
            ViewBag.newsCategoryName = newsCategoryModel.NewsCategoryMappings.SingleOrDefault(n => n.LanguageId == 129).Title;

            var NewsVM = new NewsVM();
            NewsVM.PagingItems = _newsService.Page(n => n.NewsCategoryId == newsCategoryId && n.Status == 0, n => n.ZOrder, page, ItemPerPage, true);
            NewsVM.News = NewsVM.PagingItems.Entities;
            return View(NewsVM);
        }

        public ActionResult Detail(int id)
        {
            var newsCategoryModel = _newsService.Get(n => n.Id == id)
                                                .NewsCategory
                                                .NewsCategoryMappings
                                                .Where(nc => nc.LanguageId == 129)
                                                .SingleOrDefault();

            if (newsCategoryModel == null) return View();

            ViewBag.newsCategoryName = newsCategoryModel.Title.Trim();

            var newsModel = _newsService.Get(n => n.Id == id && n.Status == 0)
                                        .NewsMappings
                                        .SingleOrDefault(nm => nm.LanguageId == 129 && nm.Status == 0);

            return View(newsModel);
        }

        [ChildActionOnly]
        public PartialViewResult _SidebarNews(int newsCategoryId)
        {
            var newsCategoryModel = _newsCategoryService.Get(n => n.Id == newsCategoryId && n.Status == 0);
            if (newsCategoryModel == null) return PartialView();

            ViewBag.newsCategoryName = newsCategoryModel.NewsCategoryMappings.SingleOrDefault(nm => nm.LanguageId == 129 && nm.Status == 0)
                                                                              .Title;
            ViewBag.newsCategoryId = newsCategoryModel.Id;

            var newsCategory = _newsService.GetMany(n => n.NewsCategoryId == newsCategoryId && n.Status == 0)
                                            .OrderBy(n => n.ZOrder)
                                            .Take(6);

            return PartialView("_SidebarNews", newsCategory);
        }
    }
}