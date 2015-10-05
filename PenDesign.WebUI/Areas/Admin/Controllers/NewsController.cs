using PenDesign.Common.Utils;
using PenDesign.Core.Interface.Service.BasicServiceInterface;
using PenDesign.Core.Model;
using PenDesign.Data;
using PenDesign.WebUI.Areas.Admin.Models;
using PenDesign.WebUI.Authencation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace PenDesign.WebUI.Areas.Admin.Controllers
{
    [CustomAuthorize(Roles = "Admin, Users")]
    public class NewsController : ApiController
    {

        private UserFactory _userFactory;
        private string _userId;
        private INewsService _newsService;
        private INewsMappingService _newsMappingService;



        public NewsController(INewsService newsService, INewsMappingService newsMappingService, UserFactory userFactory)
        {
            this._newsService = newsService;
            this._newsMappingService = newsMappingService;
            this._userFactory = userFactory;
            this._userId = _userFactory.GetUserId(User.Identity.Name);
        }

        // GET: api/News
        public IQueryable<AdminNewsVM> Get(int id)
        {
            try
            {
                var adminNewsVM = new List<AdminNewsVM>();

                var newsModel = _newsService.GetMany(n => n.NewsCategoryId == id && n.Deleted == false).AsQueryable();
                foreach (var bm in newsModel)
                {
                    var model = new AdminNewsVM()
                    {
                        Newses = bm,
                        EditedUser = _userFactory.GetUserNameById(bm.ModifiedById)
                    };
                    adminNewsVM.Add(model);
                }

                return adminNewsVM.AsQueryable();
            }
            catch (Exception)
            {
                throw new Exception("Lỗi! Không thể tải dữ liệu!");
            }
        }


        // POST: api/News
        public HttpResponseMessage Post([FromBody] News news)
        {
            try
            {
                //var mediaUrlName = news.MediaUrl;
                //if (news.MediaUrl != null)
                //    news.MediaUrl = "/Content/UploadFiles/images/images/" + news.MediaUrl;
                //else
                //    news.MediaUrl = "/Content/UploadFiles/images/images/" + news.MediaUrl;

                //news.MediaThumbUrl = "/Content/UploadFiles/images/images/thumb_" + mediaUrlName;

                //news.Status = 0; // 0 hien, 1 an, 2 xoa database
                //news.CreatedById = _userId;
                //news.CreatedDateTime = DateTime.Now;
                //news.ModifiedById = _userId;
                //news.ModifiedDateTime = DateTime.Now;

                //_newsService.Add(news);

                //var justAddedBannerId = _newsService.Entities.Max(b => b.Id);
                //var bannerMappingsModels = new List<BannerMapping>()
                //{
                //    new BannerMapping()
                //            {
                //                BannerId = justAddedBannerId, LanguageId = 129, Status = 0,
                //                Name = news.Name, Description = "",
                //                CreatedById = _userId, CreatedDateTime = DateTime.Now,
                //                ModifiedById = _userId, ModifiedDateTime = DateTime.Now
                //            },
                //            new BannerMapping()
                //            {
                //                BannerId = justAddedBannerId, LanguageId = 29, Status = 0,
                //                Name = news.Name + "-en", Description = "",
                //                CreatedById = _userId, CreatedDateTime = DateTime.Now,
                //                ModifiedById = _userId, ModifiedDateTime = DateTime.Now
                //            }
                //};
                //foreach (var bm in bannerMappingsModels)
                //{
                //    _newsMappingService.Add(bm);
                //}

                var responseMessage = new { message = "Thêm thành công!" };
                return Request.CreateResponse(HttpStatusCode.OK, responseMessage);
            }
            catch (Exception)
            {
                var responseMessage = new { message = "Lỗi! Vui lòng thử lại sau!" };
                return Request.CreateResponse(HttpStatusCode.InternalServerError, responseMessage);
                throw;
            }
        }

        // PUT: api/News/5
        public HttpResponseMessage Put([FromBody] News news)
        {
            try
            {
                //if (news.MediaUrl.ToString() != "")
                //{
                //    if (news.MediaUrl.ToString().Contains("/Content"))
                //        news.MediaUrl = news.MediaUrl;
                //    else
                //    {
                //        if (WebTools.CreateThumbnail(news.MediaUrl, "/Content/UploadFiles/images/images/", 78, 56, true, null))
                //        {
                //            news.MediaThumbUrl = "/Content/UploadFiles/images/images/thumb_" + news.MediaUrl;
                //            news.MediaUrl = "/Content/UploadFiles/images/images/" + news.MediaUrl;
                //        }
                //    }

                //}
                //else
                //    news.MediaUrl = "/Content/images/No_image_available.png";

                //news.Status = 0;
                //_newsService.Update(news);
                var responseMessage = new { message = "Chỉnh sửa thành công!" };
                return Request.CreateResponse(HttpStatusCode.OK, responseMessage);
            }
            catch (Exception)
            {
                var responseMessage = new { message = "Lỗi! Vui lòng thử lại sau!" };
                return Request.CreateResponse(HttpStatusCode.InternalServerError, responseMessage);
                throw;
            }
        }

        // DELETE: api/Banner/5
        public HttpResponseMessage Delete(int id)
        {
            try
            {
                //var banner = _newsService.GetById(id);
                //_newsMappingService.Delete(bm => bm.BannerId == banner.Id);
                //_newService.Delete(banner);

                var responseMessage = new { message = "Xóa thành công!" };
                return Request.CreateResponse(HttpStatusCode.OK, responseMessage);
            }
            catch (Exception)
            {
                var responseMessage = new { message = "Lỗi! Vui lòng thử lại sau!" };
                return Request.CreateResponse(HttpStatusCode.OK, responseMessage);
                throw;
            }
        }
    }
}
