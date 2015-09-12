using PenDesign.Common.Utils;
using PenDesign.Core.Interface.Data;
using PenDesign.Core.Interface.Service.BasicServiceInterface;
using PenDesign.Core.Model;
using PenDesign.Core.ViewModel.BannerViewModel;
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
    public class BannerController : ApiController
    {
        private IBannerService _bannerService;
        private IBannerMappingService _bannerMappingService;
        private IUnitOfWork _unitOfWork;

        public BannerController(IBannerService bannerService, IBannerMappingService bannerMappingService, IUnitOfWork unitOfWork)
        {
            this._bannerService = bannerService;
            this._bannerMappingService = bannerMappingService;
            this._unitOfWork = unitOfWork;
        }

        // GET: api/Banner
        public IQueryable<BannerVM> Get()
        {
            try
            {

                var bannerList = new List<BannerVM>();
                var model = _bannerService.Entities
                                            .Join(_bannerMappingService.Entities.Where(m => m.LanguageId == 129),
                                                b => b.Id,
                                                bm => bm.BannerId,
                                                (b, bm) => new BannerVM()
                                                {
                                                    Id = b.Id,
                                                    LanguageId = bm.LanguageId,
                                                    Name = bm.Name,
                                                    Type = b.Type,
                                                    Position = b.Position,
                                                    MediaType = b.MediaType,
                                                    MediaUrl = b.MediaUrl,
                                                    ZOrder = b.ZOrder
                                                }).AsQueryable();

                return model;
            }
            catch (Exception)
            {
                throw new Exception("Lỗi! Không thể tải dữ liệu!");
            }
        }


        // POST: api/Banner
        public HttpResponseMessage Post([FromBody] Banner banner)
        {
            try
            {
                var mediaUrlName = banner.MediaUrl;
                if (banner.MediaUrl != null)
                    banner.MediaUrl = "/Content/UploadFiles/images/images/" + banner.MediaUrl;
                else
                    banner.MediaUrl = "/Content/UploadFiles/images/images/" + banner.MediaUrl;

                banner.MediaThumbUrl = "/Content/UploadFiles/images/images/thumb_" + mediaUrlName;
                banner.Status = 0; // 0 hien, 1 an, 2 xoa database

                _bannerService.Add(banner);
                //_unitOfWork.Commit();


                //banner.BannerMappings = new BannerMapping()
                //{

                //};
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

        // PUT: api/Banner/5
        public HttpResponseMessage Put([FromBody] Banner banner)
        {
            try
            {
                if (banner.MediaUrl.ToString() != "")
                {
                    if (banner.MediaUrl.ToString().Contains("/Content"))
                        banner.MediaUrl = banner.MediaUrl;
                    else
                    {
                        if (WebTools.CreateThumbnail(banner.MediaUrl, "/Content/UploadFiles/images/images/", 78, 56, true, null))
                        {
                            banner.MediaThumbUrl = "/Content/UploadFiles/images/images/thumb_" + banner.MediaUrl;
                            banner.MediaUrl = "/Content/UploadFiles/images/images/" + banner.MediaUrl;
                        }
                    }

                }
                else
                    banner.MediaUrl = "/Content/images/No_image_available.png";

                banner.Status = 0;

                //using (var db = new DBContext())
                //{
                //    db.Entry(banner).State = System.Data.Entity.EntityState.Modified;
                //    db.SaveChanges();
                //}
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
                var banner = _bannerService.GetById(id);
                //var banner = db.BannerSliders.Find(id);
                //db.BannerSliders.Remove(banner);
                //db.SaveChanges();
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
