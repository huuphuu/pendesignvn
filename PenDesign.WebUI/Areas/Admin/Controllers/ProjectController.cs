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



        public ProjectController(IProjectService projectService, IProjectMappingService projectMappingService, UserFactory userFactory)
        {
            this._projectService = projectService;
            this._projectMappingService = projectMappingService;
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
                //var thumbUrl = "/Content/UploadFiles/images/images/" + AdminNewsVMInput.ThumbUrl;
                //var newsName = AdminNewsVMInput.Name;
                //var intro = AdminNewsVMInput.Intro;
                //var keyword = AdminNewsVMInput.Keyword;
                //var description = AdminNewsVMInput.Description;
                //var metaData = AdminNewsVMInput.MetaData;
                //var zOrder = AdminNewsVMInput.ZOrder;
                //var detail = AdminNewsVMInput.Detail;
                //var newsCategoryId = AdminNewsVMInput.NewsCategoryId;

                //var maxOrder = _newsService.Entities.Where(n => n.NewsCategoryId == newsCategoryId).Max(b => b.ZOrder);

                //var news = new News();
                //news.NewsCategoryId = newsCategoryId;
                //news.Name = newsName;
                //news.ZOrder = maxOrder + 1;
                //news.Status = true;
                //news.Deleted = false;
                //news.CreatedById = _userId;
                //news.ModifiedById = _userId;
                //_newsService.Add(news);


                //var justAddedNewsId = _newsService.Entities.Max(b => b.Id);
                //var newsMappingsModels = new List<NewsMapping>()
                //{
                //    new NewsMapping()
                //    {
                //        NewsId = justAddedNewsId, LanguageId = 129, Status = true, Deleted = false,
                //        Title = news.Name, Intro = intro, Keyword = keyword, Description = description, MetaData = metaData,
                //        ThumbUrl = thumbUrl, Detail = detail, 
                //        CreatedById = _userId, ModifiedById = _userId,
                //    },
                //    new NewsMapping()
                //    {
                //        NewsId = justAddedNewsId, LanguageId = 29, Status = true,
                //        Title = "", Intro = "", Keyword = "", Description = "", Deleted = false,
                //        ThumbUrl = thumbUrl, Detail = "", MetaData = "",
                //        CreatedById = _userId, ModifiedById = _userId
                //    }
                //};
                //foreach (var nm in newsMappingsModels)
                //{
                //    _newsMappingService.Add(nm);
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

        // PUT: api/Banner/5
        public HttpResponseMessage Put([FromBody] Project project)
        {
            try
            {
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
