using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

using System.Web.Security;
using PenDesign.WebUI.Authencation;
using PenDesign.Core.ViewModel;
using PenDesign.Core.Model;
using PenDesign.Data;
using PenDesign.Core.Models;
using PenDesign.Core.Interface.Service.BasicServiceInterface;

namespace PenDesign.WebUI.Admin.Controllers
{
    [CustomAuthorize(Roles = "Admin, Users")]
    public class AdminMenuController : ApiController
    {
        //private DBContext db = new DBContext();

        private readonly IAdminMenuService _adminMenuService;
        public AdminMenuController(IAdminMenuService adminMenuService)
        {
            this._adminMenuService = adminMenuService;
        }

        protected UserManager<ApplicationUser> UserManager = new UserManager<ApplicationUser>(
                                                                new UserStore<ApplicationUser>(new ApplicationDbContext()));

        // GET: api/AdminMenu
        public HttpResponseMessage GetMenus()
        {
            try
            {
                if (User.Identity.IsAuthenticated)
                {
                    var userId = User.Identity.GetUserId();
                    var menuResultList = new List<AdminMenuViewModel>();
                    var parentMenuList = new List<AdminMenu>();

                    if (UserManager.IsInRole(userId, "Admin"))
                        parentMenuList = _adminMenuService.GetMany(m => m.Parent == 0
                                                                && m.Available == true)
                                                                .OrderBy(m => m.Order).ToList(); 
                    else
                        parentMenuList = _adminMenuService.GetMany(m => m.Parent == 0
                                                                && m.IsAdmin == false
                                                                && m.Available == true)
                                                                .OrderBy(m => m.Order).ToList(); 

                    foreach (var menu in parentMenuList)
                    {
                        var menuResult = new AdminMenuViewModel()
                        {
                            Name = menu.Name,
                            Url = menu.Url,
                            CssIcon = menu.CssIcon,
                            LabelCss = menu.LabelCss,
                            Type = menu.Type,
                            Parent = menu.Parent,
                            Order = menu.Order,
                            Available = menu.Available,
                            Childs = GetChilds(menu.Id)
                        };
                        menuResultList.Add(menuResult);
                    }

                    return Request.CreateResponse(HttpStatusCode.OK, menuResultList);
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.InternalServerError);
                }
            }
            catch (Exception)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError);
                throw;
            }
            


        }

        public List<AdminMenuViewModel> GetChilds(int ParentId)
        {
            var childsList = _adminMenuService.GetMany(m => m.Parent == ParentId).ToList();
            var childsListVM = new List<AdminMenuViewModel>();
            foreach (var menu in childsList)
            {
                var menuResult = new AdminMenuViewModel()
                {
                    Name = menu.Name,
                    Url = menu.Url,
                    CssIcon = menu.CssIcon,
                    LabelCss = menu.LabelCss,
                    Type = menu.Type,
                    Parent = menu.Parent,
                    Order = menu.Order,
                    Available = menu.Available,
                    Childs = GetChilds(menu.Id)   
                };
                childsListVM.Add(menuResult);
            }
            return childsListVM;
        }

        //protected override void Dispose(bool disposing)
        //{
        //    if (disposing)
        //    {
        //        db.Dispose();
        //    }
        //    base.Dispose(disposing);
        //}
    }
}
