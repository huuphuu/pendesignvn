using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Diagnostics;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

using PenDesign.Common.Utils;
using System.Linq;
using System.Collections.Generic;
using PenDesign.WebUI.Models;

namespace PenDesign.WebUI.Authencation
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    public sealed class CustomAuthorize : ActionFilterAttribute, IExceptionFilter
    {
        public string Roles { get; set; }

        //ApplicationDbContext ApplicationDbContext { get; set; }

        //public override void OnActionExecuting(HttpActionContext filterContext)
        //{
        //    var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));
        //    var RoleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(new ApplicationDbContext()));
        //    var controller = filterContext.ActionDescriptor.ControllerDescriptor.ControllerName;
        //    var action = filterContext.ActionDescriptor.ActionName;
        //    var request = filterContext.Request;
        //    var headers = request.Headers;
        //    var RolesList = new List<String>();
        //    if (Roles != null)
        //    {
        //        var RolesArray = Roles.Split(',');
        //        for (var i = 0; i < RolesArray.Length; i++)
        //        {
        //            RolesList.Add(RolesArray[i]);
        //        }
        //    }
        //    else
        //        return;


        //    DBContext db = new DBContext();

        //    var currentRequestControllerAction = string.Format("{0}-{1}", controller, action);
        //    //var userRoles = UserManager.GetRoles(HttpContext.Current.User.Identity.GetUserId());
        //    //foreach (var role in db.Roles.ToList())
        //    //{
        //    //    List<string> roleIds = db.Roles.Select(m => m.Id).ToList();

        //    //    List<RolePermission> rolePermissions = db.RolePermissions
        //    //                                                    .Where(m => roleIds.Contains(m.ApplicationRoleId))
        //    //                                                    .ToList();
        //    //}
        //    foreach (var role in db.Roles.ToList())
        //    {
        //        List<string> roleIds = db.Roles.Select(m => m.Id).ToList();

        //        List<RolePermission> rolePermissions = db.RolePermissions
        //                                                        .Where(m => roleIds.Contains(m.ApplicationRoleId))
        //                                                        .ToList();
        //    }
        //    //var userRolePermission = "";

        //    TransactionalInformation tran = new TransactionalInformation();

        //    if (!headers.Contains("X-Requested-With") || headers.GetValues("X-Requested-With").FirstOrDefault() != "XMLHttpRequest")
        //    {
        //        tran.ReturnMessage.Add("Truy cập bị từ chối.");
        //        tran.ReturnStatus = false;

        //        filterContext.Response = request.CreateResponse<TransactionalInformation>
        //                                 (HttpStatusCode.BadRequest, tran);
        //    }
        //    else
        //    {
        //        HttpContext ctx = default(HttpContext);
        //        ctx = HttpContext.Current;

        //        if (HttpContext.Current.Request.IsAuthenticated)
        //        {
        //            var userRoles = UserManager.GetRoles(HttpContext.Current.User.Identity.GetUserId());
        //            if (Roles == null)
        //                return;

        //            if (userRoles.Count() > 0)
        //            {
        //                foreach (var userRole in userRoles)
        //                {
        //                    foreach (var role in RolesList)
        //                    {
        //                        if (userRole.ToString().Trim() == role.ToString().Trim())
        //                            return;
        //                    }
        //                }

        //                tran.ReturnMessage.Add("Bạn không có quyền thực hiện lệnh này.");
        //                tran.ReturnStatus = false;
        //                filterContext.Response = request.CreateResponse<TransactionalInformation>
        //                    (HttpStatusCode.Unauthorized, tran);

        //            }
        //            else
        //            {
        //                tran.ReturnMessage.Add("Bạn không có quyền thực hiện lệnh này.");
        //                tran.ReturnStatus = false;
        //                filterContext.Response = request.CreateResponse<TransactionalInformation>
        //                                                (HttpStatusCode.Unauthorized, tran);
        //            }

        //            //if (Roles == null && roles.Count > 0 || Roles != null && userRoles.Contains(Roles))
        //            //    return;  
        //        }
        //        else
        //        {
        //            tran.ReturnMessage.Add("Phiên làm việc của bạn đã kết thúc, vui lòng đăng nhập lại.");
        //            tran.ReturnStatus = false;

        //            filterContext.Response = request.CreateResponse<TransactionalInformation>
        //                                     (HttpStatusCode.Unauthorized, tran);
        //        }
        //    }
        //}


        public System.Threading.Tasks.Task ExecuteExceptionFilterAsync(HttpActionExecutedContext actionExecutedContext, System.Threading.CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}