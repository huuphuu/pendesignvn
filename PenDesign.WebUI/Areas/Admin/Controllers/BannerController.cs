using PenDesign.Common.Utils;
using PenDesign.Core.Interface.Data;
using PenDesign.Core.Interface.Service.BasicServiceInterface;
using PenDesign.Core.Model;
using PenDesign.Core.Models;
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
    public class BannerController : ApiController
    {
        private IBannerService _bannerService;
        private IBannerMappingService _bannerMappingService;
        private UserFactory _userFactory;
        private string _userId;



        public BannerController(IBannerService bannerService, IBannerMappingService bannerMappingService, UserFactory userFactory)
        {
            this._bannerService = bannerService;
            this._bannerMappingService = bannerMappingService;
            //this.userId = User.i
            this._userFactory = userFactory;
            this._userId = _userFactory.GetUserId(User.Identity.Name);
        }

        // GET: api/Banner
        //public IQueryable<BannerVM> Get()
        public IQueryable<AdminBannerVM> Get()
        {
            try
            {

                //var bannerList = new List<BannerVM>();
                //var model = _bannerService.Entities.Where(b => b.Status == 0 || b.Status == 1)
                //                            .Join(_bannerMappingService.Entities,
                //                                b => b.Id,
                //                                bm => bm.BannerId,
                //                                (b, bm) => new BannerVM()
                //                                {
                //                                    Id = b.Id,
                //                                    LanguageId = bm.LanguageId,
                //                                    Name = b.Name,
                //                                    Type = b.Type,
                //                                    Position = b.Position,
                //                                    MediaType = b.MediaType,
                //                                    MediaUrl = b.MediaUrl,
                //                                    LinkUrl = b.LinkUrl,
                //                                    ZOrder = b.ZOrder
                //                                }).AsQueryable();
                var adminBannerVM = new List<AdminBannerVM>();

                var bannerModel = _bannerService.GetMany(b => b.Deleted == false).AsQueryable();
                foreach (var bm in bannerModel)
                {
                    var model = new AdminBannerVM()
                    {
                        Banners = bm,
                        EditedUser = _userFactory.GetUserNameById(bm.ModifiedById)
                    };
                    adminBannerVM.Add(model);
                }

                return adminBannerVM.AsQueryable();
            }
            catch (Exception)
            {
                throw new Exception("Lỗi! Không thể tải dữ liệu!");
            }
        }


        // POST: api/Banner
        public HttpResponseMessage Post([FromBody] AdminBannerVMInput AdminBannerVMInput)
        {
            try
            {
                //var mediaUrlName = banner.MediaUrl;
                //if (banner.MediaUrl != null)
                //    banner.MediaUrl = "/Content/UploadFiles/images/images/" + banner.MediaUrl;
                //else
                //    banner.MediaUrl = "/Content/UploadFiles/images/images/" + banner.MediaUrl;

                //banner.MediaThumbUrl = "/Content/UploadFiles/images/images/thumb_" + mediaUrlName;

                var mediaUrl = "/Content/UploadFiles/images/images/" + AdminBannerVMInput.MediaUrl;
                var mediaThumbUrl = "/Content/UploadFiles/images/images/thumb_" + AdminBannerVMInput.MediaUrl;
                var linkUrl = AdminBannerVMInput.LinkUrl;
                var description = AdminBannerVMInput.Description;
                var zOrder = AdminBannerVMInput.ZOrder;
                var bannerName = AdminBannerVMInput.Name;

                var maxOrder = _bannerService.Entities.Where(b => b.Deleted == false).Max(b => b.ZOrder);

                var banner = new Banner();
                banner.Name = bannerName;
                banner.ZOrder = maxOrder + 1;
                banner.Status = true;
                banner.Deleted = false;
                banner.CreatedById = _userId;
                banner.ModifiedById = _userId;

                _bannerService.Add(banner);


                var justAddedBannerId = _bannerService.Entities.Max(b => b.Id);
                var bannerMappingsModels = new List<BannerMapping>()
                {
                    new BannerMapping()
                            {
                                BannerId = justAddedBannerId, LanguageId = 129, Status = true,
                                Name = banner.Name, Description = description, Deleted = false,
                                MediaUrl = mediaUrl, MediaThumbUrl = mediaThumbUrl, LinkUrl = linkUrl,
                                CreatedById = _userId, ModifiedById = _userId,
                            },
                            new BannerMapping()
                            {
                                BannerId = justAddedBannerId, LanguageId = 29, Status = true,
                                Name = "", Description = "", Deleted = false,
                                MediaUrl = mediaUrl, MediaThumbUrl = mediaThumbUrl, LinkUrl = linkUrl,
                                CreatedById = _userId, ModifiedById = _userId
                            }
                };
                foreach (var bm in bannerMappingsModels)
                {
                    _bannerMappingService.Add(bm);
                }

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
                //if (banner.MediaUrl.ToString() != "")
                //{
                //    if (banner.MediaUrl.ToString().Contains("/Content"))
                //        banner.MediaUrl = banner.MediaUrl;
                //    else
                //    {
                //        if (WebTools.CreateThumbnail(banner.MediaUrl, "/Content/UploadFiles/images/images/", 78, 56, true, null))
                //        {
                //            banner.MediaThumbUrl = "/Content/UploadFiles/images/images/thumb_" + banner.MediaUrl;
                //            banner.MediaUrl = "/Content/UploadFiles/images/images/" + banner.MediaUrl;
                //        }
                //    }

                //}
                //else
                //    banner.MediaUrl = "/Content/images/No_image_available.png";

                //banner.Status = true;

                _bannerService.Update(banner);

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
                _bannerMappingService.Delete(bm => bm.BannerId == banner.Id);
                _bannerService.Delete(banner);

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
