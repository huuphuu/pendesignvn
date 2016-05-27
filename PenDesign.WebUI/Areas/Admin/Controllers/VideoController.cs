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
    public class VideoController : ApiController
    {

        private UserFactory _userFactory;
        private string _userId;
        private IProjectService _projectService;
        private IProjectMappingService _projectMappingService;
        private IProjectImageService _projectImageService;
        private IProjectImageMappingService _projectImageMappingService;



        public VideoController(IProjectService projectService, IProjectMappingService projectMappingService, UserFactory userFactory,
                                IProjectImageService projectImageService, IProjectImageMappingService projectImageMappingService)
        {
            this._projectService = projectService;
            this._projectMappingService = projectMappingService;

            this._projectImageService = projectImageService;
            this._projectImageMappingService = projectImageMappingService;

            this._userFactory = userFactory;
            this._userId = _userFactory.GetUserId(User.Identity.Name);
        }

        // GET: api/Banner
        public IQueryable<AdminVideoVM> Get()
        {
            try
            {
                var adminProjectVM = new List<AdminVideoVM>();

                var videoModels = _projectImageService.GetMany(pi => pi.Type == 2 && pi.Deleted == false).AsQueryable();
                foreach (var bm in videoModels)
                {
                    var model = new AdminVideoVM()
                    {
                        ProjectImages = bm,
                        EditedUser = _userFactory.GetUserNameById(bm.ModifiedById)
                    };
                    adminProjectVM.Add(model);
                }

                return adminProjectVM.AsQueryable();
            }
            catch (Exception)
            {
                throw new Exception("Lỗi! Không thể tải dữ liệu!");
            }
        }


        // POST: api/Banner
        public HttpResponseMessage Post([FromBody] AdminVideoVMInput adminVideoVMInput)
        {
            try
            {
                if (adminVideoVMInput.Thumbnail.ToString() != "")
                {
                    if (adminVideoVMInput.Thumbnail.ToString().Contains("/Content"))
                    {
                        adminVideoVMInput.Thumbnail = adminVideoVMInput.Thumbnail;
                    }
                    else
                    {
                        adminVideoVMInput.Thumbnail = "/Content/UploadFiles/images/images/thumb_" + adminVideoVMInput.Thumbnail;
                    }

                }
                else
                    adminVideoVMInput.Thumbnail = "/Content/images/No_image_available.png";

                var name = adminVideoVMInput.Name;
                var description = adminVideoVMInput.Description;
                var thumbnail = adminVideoVMInput.Thumbnail;
                var resourceUrl = adminVideoVMInput.ResourceUrl;

                var maxOrder =  _projectImageService.Entities.Where(p => p.Deleted == false && p.Type == 2).Max(p => p.ZOrder);

                var projectImageModel = new ProjectImage()
                {
                    ProjectId = 15,
                    Thumbnail = thumbnail,
                    ResourceUrl = resourceUrl,
                    Deleted = false,
                    Status = true,
                    CreatedById = _userId,
                    ModifiedById = _userId,
                    CreatedDateTime = DateTime.Now,
                    ModifiedDateTime = DateTime.Now,
                    Type = 2,
                    ZOrder = maxOrder + 1
                };

                _projectImageService.Add(projectImageModel);


                var justAddedImageId = _projectImageService.Entities.Where(p => p.Deleted == false && p.Type == 2).Max(p => p.Id);

                var currentVideoLanguage = adminVideoVMInput.CurrentImageMapping;

                var projectImageMappingList = new List<ProjectImageMapping>()
                {
                    new ProjectImageMapping()
                    {
                        ProjectImageId = justAddedImageId,
                        LanguageId = 129,
                        Status = true,
                        Deleted = false,
                        Name = name,
                        Description = description,
                        CreatedById = _userId,
                        CreatedDateTime = DateTime.Now,
                        ModifiedById = _userId,
                        ModifiedDateTime = DateTime.Now
                    },
                    new ProjectImageMapping()
                    {
                        ProjectImageId = justAddedImageId,
                        LanguageId = 29,
                        Status = true,
                        Deleted = false,
                        Name = name + " - EN",
                        Description = "",
                        CreatedById = _userId,
                        CreatedDateTime = DateTime.Now,
                        ModifiedById = _userId,
                        ModifiedDateTime = DateTime.Now
                    }
                };

                foreach (var pim in projectImageMappingList)
                {
                    _projectImageMappingService.Add(pim);
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

        // PUT: api/Banner/5
        public HttpResponseMessage Put([FromBody] ProjectImage projectImage)
        {
            try
            {
                if (projectImage.Thumbnail.ToString() != "")
                {
                    if (projectImage.Thumbnail.ToString().Contains("/Content"))
                        projectImage.Thumbnail = projectImage.Thumbnail;
                    else
                    {
                        projectImage.Thumbnail = "/Content/UploadFiles/images/images/thumb_" + projectImage.Thumbnail;
                    }

                }
                else
                    projectImage.Thumbnail = "/Content/images/No_image_available.png";

                _projectImageService.Update(projectImage);




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
                var projectImage = _projectImageService.GetById(id);
                _projectImageMappingService.Delete(pim => pim.ProjectImageId == projectImage.Id);
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
