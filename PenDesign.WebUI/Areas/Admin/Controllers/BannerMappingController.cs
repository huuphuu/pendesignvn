using PenDesign.Common.Utils;
using PenDesign.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using PenDesign.Core.Interface.Service.BasicServiceInterface;
using PenDesign.Data;

namespace PenDesign.WebUI.Areas.Admin.Controllers
{
    public class BannerMappingController : ApiController
    {
        private IBannerMappingService _bannerMappingService;
        private UserFactory _userFactory;
        private string _userId;

        public BannerMappingController(IBannerMappingService bannerMappingService, UserFactory userFactory)
        {
            this._bannerMappingService = bannerMappingService;

            this._userFactory = userFactory;
            this._userId = _userFactory.GetUserId(User.Identity.Name);

        }
        // PUT: api/BannerMapping/5
        public HttpResponseMessage Put(int id, [FromBody]BannerMapping bannerMappingModel)
        {
            try
            {
                if (bannerMappingModel.MediaUrl.ToString() != "")
                {
                    if (bannerMappingModel.MediaUrl.ToString().Contains("/Content"))
                        bannerMappingModel.MediaUrl = bannerMappingModel.MediaUrl;
                    else
                    {
                        //if (WebTools.CreateThumbnail(bannerMappingModel.MediaUrl, "/Content/UploadFiles/images/images/", 78, 56, true, null))
                        //{
                            bannerMappingModel.MediaThumbUrl = "/Content/UploadFiles/images/images/thumb_" + bannerMappingModel.MediaUrl;
                            bannerMappingModel.MediaUrl = "/Content/UploadFiles/images/images/" + bannerMappingModel.MediaUrl;
                        //}
                    }

                }
                else
                    bannerMappingModel.MediaUrl = "/Content/images/No_image_available.png";

                bannerMappingModel.Status = true;
                bannerMappingModel.ModifiedDateTime = DateTime.Now;
                bannerMappingModel.ModifiedById = _userId;

                _bannerMappingService.Update(bannerMappingModel);

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


       
    }
}
