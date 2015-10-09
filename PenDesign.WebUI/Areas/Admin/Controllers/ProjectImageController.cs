using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using PenDesign.Core.Interface.Service.BasicServiceInterface;
using PenDesign.Data;
using PenDesign.Core.Model;

namespace PenDesign.WebUI.Areas.Admin.Controllers
{
    public class ProjectImageController : ApiController
    {
        private IProjectImageService _projectImageService;
        private IProjectImageMappingService _projectImageMappingService;
        private UserFactory _userFactory;
        private string _userId;
        
        public ProjectImageController(IProjectImageService projectImageService, IProjectImageMappingService projectImageMappingService, UserFactory userFactory)
        {
            this._projectImageService = projectImageService;
            this._projectImageMappingService = projectImageMappingService;

            this._userFactory = userFactory;
            this._userId = _userFactory.GetUserId(User.Identity.Name);
        }

        public HttpResponseMessage Put(int id, [FromBody]ProjectImage projectImageModel)
        {
            try
            {
                if (projectImageModel.ResourceUrl.ToString() != "")
                {
                    if (projectImageModel.ResourceUrl.ToString().Contains("/Content"))
                        projectImageModel.ResourceUrl = projectImageModel.ResourceUrl;
                    else
                    {
                        projectImageModel.ResourceUrl = "/Content/UploadFiles/images/images/" + projectImageModel.ResourceUrl;
                    }

                }
                else
                    projectImageModel.ResourceUrl = "/Content/images/No_image_available.png";

                projectImageModel.Status = true;
                projectImageModel.ModifiedDateTime = DateTime.Now;
                projectImageModel.ModifiedById = _userId;

                _projectImageService.Update(projectImageModel);

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
