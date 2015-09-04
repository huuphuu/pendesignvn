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

        public BannerController(IBannerService bannerService, IBannerMappingService bannerMappingService)
        {
            this._bannerService = bannerService;
            this._bannerMappingService = bannerMappingService;
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

        // GET: api/Banner/5
        //public string Get(int id)
        //{
        //    return "value";
        //}

        // POST: api/Banner
        //public HttpResponseMessage Post([FromBody] Banner banner)
        //{
        //    try
        //    {
        //        if (banner.MediaUrl.ToString() != "")
        //            banner.MediaUrl = "/Content/UploadFiles/images/images/" + banner.MediaUrl;
        //        else
        //            banner.MediaUrl = "/Content/images/No_image_available.png";

        //        banner.Status = 0; // 0 hien, 1 an, 2 xoa database
        //        //using (var db = new DBContext())
        //        //{
        //        //    db.BannerSliders.Add(banner);
        //        //    db.SaveChanges();
        //        //}

        //        var responseMessage = new { message = "Thêm thành công!" };
        //        return Request.CreateResponse(HttpStatusCode.OK, responseMessage);
        //    }
        //    catch (Exception)
        //    {
        //        var responseMessage = new { message = "Lỗi! Vui lòng thử lại sau!" };
        //        return Request.CreateResponse(HttpStatusCode.InternalServerError, responseMessage);
        //        throw;
        //    }
        //}

        // PUT: api/Banner/5
        //public HttpResponseMessage Put([FromBody] Banner banner)
        //{
        //    try
        //    {
        //        if (banner.ImageUrl.ToString() != "")
        //        {
        //            if (banner.ImageUrl.ToString().Contains("/Content"))
        //                banner.ImageUrl = banner.ImageUrl;
        //            else
        //                banner.ImageUrl = "/Content/UploadFiles/images/images/" + banner.ImageUrl;
        //        }
        //        else
        //            banner.ImageUrl = "/Content/images/No_image_available.png";

        //        banner.Available = true;

        //        using (var db = new DBContext())
        //        {
        //            db.Entry(banner).State = System.Data.Entity.EntityState.Modified;
        //            db.SaveChanges();
        //        }
        //        var responseMessage = new { message = "Chỉnh sửa thành công!" };
        //        return Request.CreateResponse(HttpStatusCode.OK, responseMessage);
        //    }
        //    catch (Exception)
        //    {
        //        var responseMessage = new { message = "Lỗi! Vui lòng thử lại sau!" };
        //        return Request.CreateResponse(HttpStatusCode.InternalServerError, responseMessage);
        //        throw;
        //    }
        //}

        // DELETE: api/Banner/5
        //public HttpResponseMessage Delete(int id)
        //{
        //    try
        //    {
        //        var banner = db.BannerSliders.Find(id);
        //        db.BannerSliders.Remove(banner);
        //        db.SaveChanges();
        //        var responseMessage = new { message = "Xóa thành công!" };
        //        return Request.CreateResponse(HttpStatusCode.OK, responseMessage);
        //    }
        //    catch (Exception)
        //    {
        //        var responseMessage = new { message = "Lỗi! Vui lòng thử lại sau!" };
        //        return Request.CreateResponse(HttpStatusCode.OK, responseMessage);
        //        throw;
        //    }
        //}
    }
}
