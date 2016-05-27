using PenDesign.Core.Interface.Service.BasicServiceInterface;
using PenDesign.Core.Model;
using PenDesign.Data;
using PenDesign.WebUI.Authencation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using PenDesign.WebUI.Areas.Admin.Models;

namespace PenDesign.WebUI.Areas.Admin.Controllers
{
    [CustomAuthorize(Roles = "Admin, Users")]
    public class ProjectController : ApiController
    {

        private UserFactory _userFactory;
        private string _userId;
        private IProjectService _projectService;
        private IProjectMappingService _projectMappingService;
        private IProjectImageService _projectImageService;
        private INewsService _newsService;
        private INewsMappingService _newsMappingService;
        private IProjectImageMappingService _projectImageMappingService;



        public ProjectController(IProjectService projectService, IProjectMappingService projectMappingService,
                                    IProjectImageService projectImageService, IProjectImageMappingService projectImageMappingService,
                                        INewsService newsService, INewsMappingService newsMappingService, UserFactory userFactory)
        {
            this._projectService = projectService;
            this._projectMappingService = projectMappingService;

            this._projectImageService = projectImageService;
            this._projectImageMappingService = projectImageMappingService;

            this._newsService = newsService;
            this._newsMappingService = newsMappingService;

            this._userFactory = userFactory;
            this._userId = _userFactory.GetUserId(User.Identity.Name);
        }

        // GET: api/Banner
        public IQueryable<AdminProjectVM> Get()
        {
            try
            {
                var adminProjectVM = new List<AdminProjectVM>();

                var projectModel = _projectService.GetMany(n => n.Type == 1 && n.Deleted == false).AsQueryable();

                foreach (var bm in projectModel)
                {
                    foreach (var pi in bm.ProjectImages.ToList())
                    {
                        if (pi.Deleted)
                            bm.ProjectImages.Remove(pi);
                    }
                    var model = new AdminProjectVM()
                    {
                        Projects = bm,
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
        public HttpResponseMessage Post([FromBody] AdminProjectVMInput AdminProjectVMInput)
        {
            try
            {
                if (AdminProjectVMInput.ResourceUrl.ToString() != "")
                {
                    if (AdminProjectVMInput.ResourceUrl.ToString().Contains("/Content"))
                    {
                        AdminProjectVMInput.ResourceUrl = AdminProjectVMInput.ResourceUrl;
                        AdminProjectVMInput.Thumbnail = AdminProjectVMInput.Thumbnail;
                    }
                    else
                    {
                        AdminProjectVMInput.Thumbnail = "/Content/UploadFiles/images/images/thumb_" + AdminProjectVMInput.ResourceUrl;
                        AdminProjectVMInput.ResourceUrl = "/Content/UploadFiles/images/images/" + AdminProjectVMInput.ResourceUrl;
                    }
                }
                else
                {
                    AdminProjectVMInput.ResourceUrl = "/Content/images/No_image_available.png";
                    AdminProjectVMInput.Thumbnail = "/Content/images/No_image_available.png";
                }

                var name = AdminProjectVMInput.Name;
                var intro = AdminProjectVMInput.Intro;
                var keyword = AdminProjectVMInput.Keyword;
                var description = AdminProjectVMInput.Description;
                var detail = AdminProjectVMInput.Detail;
                var metaData = AdminProjectVMInput.MetaData;
                var thumbnail = AdminProjectVMInput.Thumbnail;
                var resourceUrl = AdminProjectVMInput.ResourceUrl;

                var maxOrder = _projectService.Entities.Where(p => p.Type == 1 && p.Deleted == false).Max(p => p.ZOrder);

                var projectModel = new Project()
                {
                    Name = name, Type = 1, ZOrder = maxOrder + 1, Status = true, Deleted = false,
                    CreatedById = _userId, ModifiedById = _userId
                };
                _projectService.Add(projectModel);

                var justAddedProjectId = _projectService.Entities.Max(b => b.Id);

                /*
                 add project image
                 */

                var projectImage = new ProjectImage()
                {
                    ProjectId = justAddedProjectId, Thumbnail = thumbnail, ResourceUrl = resourceUrl,
                    Type = 1, ZOrder = 1, Status = true, Deleted = false,
                    CreatedById = _userId, ModifiedById = _userId
                };
                _projectImageService.Add(projectImage);

                var justAddedProjectImageId = _projectImageService.Entities.Max(b => b.Id);
                var projectImageMappingList = new List<ProjectImageMapping>()
                {
                    new ProjectImageMapping()
                    {
                        ProjectImageId = justAddedProjectImageId, LanguageId = 129, Deleted = false, Status = true,
                        Name = name, Description = "",
                        CreatedById = _userId, ModifiedById = _userId
                    },
                    new ProjectImageMapping()
                    {
                        ProjectImageId = justAddedProjectImageId, LanguageId = 29, Deleted = false, Status = true,
                        Name = name + " - EN", Description = "",
                        CreatedById = _userId, ModifiedById = _userId
                    }
                };
                foreach (var pim in projectImageMappingList)
                {
                    _projectImageMappingService.Add(pim);
                }



                /*
                 Add News
                 */

                var news = new News();
                news.ProjectId = justAddedProjectId;
                news.Name = AdminProjectVMInput.Name;
                news.CreatedById = _userId;
                news.ModifiedById = _userId;
                news.Deleted = false;
                news.Status = true;
                news.ZOrder = 1;
                _newsService.Add(news);

                var justAddedNewsId = _newsService.Entities.Max(b => b.Id);
                var newsMappingsModels = new List<NewsMapping>()
                {
                    new NewsMapping()
                    {
                        NewsId = justAddedNewsId, LanguageId = 129, Status = true, Deleted = false,
                        Title = news.Name, Intro = intro, Keyword = keyword, Description = description, MetaData = metaData, Detail = detail, 
                        CreatedById = _userId, ModifiedById = _userId,
                    },
                    new NewsMapping()
                    {
                        NewsId = justAddedNewsId, LanguageId = 29, Status = true,
                        Title = "", Intro = "", Keyword = "", Description = "", Deleted = false, Detail = "", MetaData = "",
                        CreatedById = _userId, ModifiedById = _userId
                    }
                };
                foreach (var nm in newsMappingsModels)
                {
                    _newsMappingService.Add(nm);
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
        public HttpResponseMessage Put([FromBody] Project project)
        {
            try
            {
                project.ModifiedById = _userId;
                project.ModifiedDateTime = DateTime.Now;

                if (project.ProjectImages.Count() > 0)
                {
                    foreach (var projectImageModel in project.ProjectImages)
                    {
                        projectImageModel.ModifiedById = _userId;
                        projectImageModel.ModifiedDateTime = DateTime.Now;
                        _projectImageService.Update(projectImageModel);
                    }
                }

                if (project.News.Count() > 0)
                {
                    foreach (var projectNew in project.News)
                    {
                        projectNew.ModifiedById = _userId;
                        projectNew.ModifiedDateTime = DateTime.Now;
                        _newsService.Update(projectNew);

                        foreach (var projectNewsMapping in projectNew.NewsMappings)
                        {
                            projectNewsMapping.ModifiedById = _userId;
                            projectNewsMapping.ModifiedDateTime = DateTime.Now;
                            _newsMappingService.Update(projectNewsMapping);
                        }
                    }
                }

                _projectService.Update(project);
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
                var project = _projectService.GetById(id);
                _projectMappingService.Delete(pm => pm.ProjectId == project.Id);
                _projectImageService.Delete(pi => pi.ProjectId == project.Id);
                _newsService.Delete(n => n.ProjectId == project.Id);

                _projectService.Delete(project);

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
