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
    public class NewsMappingController : ApiController
    {
        private INewsMappingService _newsMappingService;
        private UserFactory _userFactory;
        private string _userId;

        public NewsMappingController(INewsMappingService newsMappingService, UserFactory userFactory)
        {
            this._newsMappingService = newsMappingService;

            this._userFactory = userFactory;
            this._userId = _userFactory.GetUserId(User.Identity.Name);
        }

        public HttpResponseMessage Put(int id, [FromBody]NewsMapping newsMappingModel)
        {
            try
            {
                if (newsMappingModel.ThumbUrl.ToString() != "")
                {
                    if (newsMappingModel.ThumbUrl.ToString().Contains("/Content"))
                        newsMappingModel.ThumbUrl = newsMappingModel.ThumbUrl;
                    else
                    {
                        newsMappingModel.ThumbUrl = "/Content/UploadFiles/images/images/thumb_" + newsMappingModel.ThumbUrl;
                        newsMappingModel.ThumbUrl = "/Content/UploadFiles/images/images/" + newsMappingModel.ThumbUrl;
                    }

                }
                else
                    newsMappingModel.ThumbUrl = "/Content/images/No_image_available.png";

                newsMappingModel.Status = true;
                newsMappingModel.ModifiedById = _userId;

                _newsMappingService.Update(newsMappingModel);

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
