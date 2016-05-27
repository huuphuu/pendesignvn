using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using PenDesign.Core.Interface.Service.BasicServiceInterface;
using PenDesign.Data;
using PenDesign.Core.Model;
using PenDesign.WebUI.Areas.Admin.Models;

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

        public HttpResponseMessage Post([FromBody]AdminProjectImageVMInput AdminProjectImageVMInput)
        {
            try
            {
                if (AdminProjectImageVMInput.ResourceUrl.ToString() != "")
                {
                    if (AdminProjectImageVMInput.ResourceUrl.ToString().Contains("/Content"))
                    {
                        AdminProjectImageVMInput.ResourceUrl = AdminProjectImageVMInput.ResourceUrl;
                        AdminProjectImageVMInput.Thumbnail = AdminProjectImageVMInput.Thumbnail;
                    }
                    else
                    {
                        AdminProjectImageVMInput.Thumbnail = "/Content/UploadFiles/images/images/thumb_" + AdminProjectImageVMInput.ResourceUrl;
                        AdminProjectImageVMInput.ResourceUrl = "/Content/UploadFiles/images/images/" + AdminProjectImageVMInput.ResourceUrl;
                    }       
                }
                else
                {
                    AdminProjectImageVMInput.ResourceUrl = "/Content/images/No_image_available.png";
                    AdminProjectImageVMInput.Thumbnail = "/Content/images/No_image_available.png";
                }

                var maxZOrder = _projectImageService.Entities.Where(b => b.ProjectId == AdminProjectImageVMInput.ProjectId).Max(b => b.ZOrder);

                var projectImageModel = new ProjectImage();
                projectImageModel.ProjectId = AdminProjectImageVMInput.ProjectId;
                projectImageModel.ResourceUrl = AdminProjectImageVMInput.ResourceUrl;
                projectImageModel.Thumbnail = AdminProjectImageVMInput.Thumbnail;
                projectImageModel.ZOrder = maxZOrder;
                projectImageModel.Status = true;
                projectImageModel.CreatedById = _userId;
                projectImageModel.CreatedDateTime = DateTime.Now;
                projectImageModel.ModifiedDateTime = DateTime.Now;
                projectImageModel.ModifiedById = _userId;

                _projectImageService.Add(projectImageModel);

                var justAddedProjectImageId = _projectImageService.Entities.Max(b => b.Id);
                var projectImageMappingsList = new List<ProjectImageMapping>()
                {
                    new ProjectImageMapping()
                    {
                        ProjectImageId = justAddedProjectImageId, LanguageId = 129, Status = true, Deleted = false,
                        Name = AdminProjectImageVMInput.Name, Description = AdminProjectImageVMInput.Description,
                        CreatedById = _userId, ModifiedById = _userId,
                        CreatedDateTime = DateTime.Now, ModifiedDateTime = DateTime.Now
                    },
                    new ProjectImageMapping()
                    {
                        ProjectImageId = justAddedProjectImageId, LanguageId = 29, Status = true, Deleted = false,
                        Name = "", Description = "",
                        CreatedById = _userId, ModifiedById = _userId,
                        CreatedDateTime = DateTime.Now, ModifiedDateTime = DateTime.Now
                    }
                };
                foreach (var im in projectImageMappingsList)
                {
                    _projectImageMappingService.Add(im);
                }



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

        public HttpResponseMessage Put(int id, [FromBody]ProjectImage projectImageModel)
        {
            try
            {
                if (projectImageModel.ResourceUrl.ToString() != "")
                {
                    if (projectImageModel.ResourceUrl.ToString().Contains("/Content") &&
                        projectImageModel.Thumbnail.ToString().Contains("/Content"))
                    {
                        projectImageModel.ResourceUrl = projectImageModel.ResourceUrl;
                        projectImageModel.Thumbnail = projectImageModel.Thumbnail;
                    }
                    else
                    {
                        projectImageModel.ResourceUrl = "/Content/UploadFiles/images/images/" +
                                                        projectImageModel.ResourceUrl;
                        projectImageModel.Thumbnail = "/Content/UploadFiles/images/images/thumb_" +
                                                      projectImageModel.ResourceUrl;
                    }

                }
                else
                {
                    projectImageModel.ResourceUrl = "/Content/images/No_image_available.png";
                    projectImageModel.Thumbnail = "/Content/images/No_image_available.png";
                }
                    

                projectImageModel.Status = true;
                projectImageModel.ModifiedDateTime = DateTime.Now;
                projectImageModel.ModifiedById = _userId;

                _projectImageService.Update(projectImageModel);


                foreach (var projectImageMapping in projectImageModel.ProjectImageMappings)
                {
                    _projectImageMappingService.Update(projectImageMapping);
                }

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

        public HttpResponseMessage Delete(int id)
        {
            try
            {
                var projectImage = _projectImageService.GetById(id);
                _projectImageMappingService.Delete(pm => pm.ProjectImageId == projectImage.Id);
                _projectImageService.Delete(projectImage);

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
