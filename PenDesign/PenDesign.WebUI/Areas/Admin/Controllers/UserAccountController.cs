using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

using System.Web.Security;
using System.Data.Entity;

using Newtonsoft.Json;
using System.Threading.Tasks;
using PenDesign.WebUI.Authencation;
using PenDesign.Core.Models;
using PenDesign.Core.Model;
using PenDesign.Core.ViewModel;
using PenDesign.Data;
using PenDesign.Core.Interface.Service.BasicServiceInterface;

namespace PenDesign.WebUI.Areas.Admin.Controllers
{
    [Authorize]
    public class UserAccountController : ApiController
    {
        ApplicationDbContext context = new ApplicationDbContext();

        //DBContext db = new DBContext();

        //protected UserManager<ApplicationUser> UserManager { get; set; }
        protected UserManager<ApplicationUser> UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));
        protected RoleManager<IdentityRole> RoleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(new ApplicationDbContext()));
        private INewsDraftService _newDraftService;

        public UserAccountController(INewsDraftService newsDraftService)
        {
            this._newDraftService = newsDraftService;
        }

        //CRUD USER
        //Mange User Roles

        [HttpGet]
        [Route("api/userAccount/CheckUserPermission")]
        [AllowAnonymous]
        public HttpResponseMessage CheckUserPermission() //lay thong tin user dang dang nhap va cac thong bao can duyet
        {
            try
            {
                if (User.Identity.IsAuthenticated)
                {
                    return Request.CreateResponse(HttpStatusCode.OK);
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.BadRequest);
                }
            }
            catch
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
        }

        [HttpPost]
        [Route("api/userAccount/DeleteUser")]
        [CustomAuthorize(Roles = "Admin")]
        public async Task<HttpResponseMessage> DeleteUser(ApplicationUser user) //xoa user
        {
            try
            {
                if (User.Identity.IsAuthenticated)
                {
                    var result = await UserManager.DeleteAsync(user);
                    if (result.Succeeded)
                        return Request.CreateResponse(HttpStatusCode.OK);
                    else
                        return Request.CreateResponse(HttpStatusCode.InternalServerError);
                }
                else
                    return Request.CreateResponse(HttpStatusCode.Unauthorized);
            }
            catch
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError);
            }
        }

        [HttpGet]
        [Route("api/userAccount/getUserInfo")]
        [CustomAuthorize(Roles = "Admin")]
        public HttpResponseMessage getUserInfo() //lay thong tin user dang dang nhap va cac thong bao can duyet
        {
            try
            {
                if (User.Identity.IsAuthenticated)
                {
                    var userId = User.Identity.GetUserId();
                    //var userModel = db.Users.Single(m => m.Id == userId);
                    //var userModelInfo = db.Users.Where(m => m.Id == userId).Select(m => m.UserInfo).SingleOrDefault();

                    var userModel = context.Users.Single(m => m.Id == userId);
                    var userModelInfo = context.Users.Where(m => m.Id == userId).Select(m => m.UserInfo).SingleOrDefault();

                    var userNotificationList = new List<UserNotificationViewModel>();
                    var userNotificationItem = new UserNotificationViewModel()
                    {
                        NewsDrafts = _newDraftService.GetAll().ToList() //db.NewsDrafts.ToList()
                    };
                    userNotificationList.Add(userNotificationItem);

                    var user = new UserInfoViewModel()
                    {
                        Id = userId,
                        UserName = userModel.UserName,
                        UserInfo = userModelInfo,
                        Email = userModel.Email,
                        UserNotifications = userNotificationList
                    };
                    return Request.CreateResponse(HttpStatusCode.OK, user);
                }
                else
                    return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
            catch
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
        }

        [HttpGet]
        [Route("api/userAccount/GetAllUsers")]
        [CustomAuthorize(Roles = "Admin")]
        public HttpResponseMessage GetAllUsers() //lay tat ca user
        {
            try
            {
                var userList = new List<ManagerUserViewModel>();
                foreach (var user in context.Users.ToList())
                {
                    var userRolesId = user.Roles.Select(m => m.RoleId).ToList();
                    var userId = user.Id;
                    var userModelInfo = context.Users.Where(m => m.Id == userId).Select(m => m.UserInfo).SingleOrDefault();
                    var model = new ManagerUserViewModel()
                    {
                        UserId = user.Id,
                        UserName = user.UserName,
                        Password = user.PasswordHash,
                        SecurityStamp = Guid.NewGuid().ToString(),
                        Email = user.Email,
                        UserInfo = userModelInfo,
                        Roles = context.Roles.Where(r => userRolesId.Contains(r.Id))
                                        .OrderBy(r => r.Name)
                                        .ToList()
                    };
                    userList.Add(model);
                }

                //var output = JsonConvert.SerializeObject(userList);
                return Request.CreateResponse(HttpStatusCode.OK, userList);
            }
            catch
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
        }

        [HttpPost]
        [Route("api/userAccount/UpdateUser")]
        [CustomAuthorize(Roles = "Admin")]
        public HttpResponseMessage UpdateUser(ManagerUserViewModel user) //cap nhat user
        {
            try
            {
                //update user at aspNetUser Table
                var ApplicationUserUpdate = new ApplicationUser
                {
                    Id = user.UserId,
                    EmailConfirmed = false,
                    PasswordHash = UserManager.PasswordHasher.HashPassword(user.Password),
                    SecurityStamp = user.SecurityStamp,
                    UserName = user.UserName,
                    Email = user.Email,
                };
                context.Entry(ApplicationUserUpdate).State = System.Data.Entity.EntityState.Modified;
                context.SaveChanges();

                //update userinfo at userinfo table
                UserInfo userInfoModel = new UserInfo();
                //check avatar
                if (user.UserInfo.Avatar != "")
                    user.UserInfo.Avatar = "/Content/UploadFiles/images/" + user.UserInfo.Avatar;
                else
                    user.UserInfo.Avatar = "/Content/UploadFiles/images/No_image_available.png";
                userInfoModel = user.UserInfo;
                using (var ncontext = new ApplicationDbContext())
                {
                    ncontext.Entry(userInfoModel).State = System.Data.Entity.EntityState.Modified;
                    ncontext.SaveChanges();
                };

                //update userRoles at aspNetRoles table
                ApplicationUser userFromDb = context.Users.Where(u => u.UserName.Equals(user.UserName, StringComparison.CurrentCultureIgnoreCase)).FirstOrDefault();
                var userRoles = UserManager.GetRoles(user.UserId);
                if (userRoles.Count() > 0)
                {
                    //remove user from current roles
                    foreach (var role in userRoles)
                    {
                        UserManager.RemoveFromRole(userFromDb.Id, role);
                    }
                    //add user to new roles
                    UserManager.AddToRole(user.UserId, user.Roles.SingleOrDefault().Name);
                }



                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
        }

        //CRUD Role
        [HttpGet]
        [Route("api/userAccount/GetAllRoles")]
        [CustomAuthorize(Roles = "Admin")]
        public HttpResponseMessage GetAllRoles()
        {
            try
            {
                var roles = context.Roles.ToList();
                return Request.CreateResponse(HttpStatusCode.OK, roles);
            }
            catch
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
        }


        [HttpGet]
        [Route("api/userAccount/GetRole")]
        [CustomAuthorize(Roles = "Admin")]
        public HttpResponseMessage GetRole(string roleName)
        {
            try
            {
                var thisRole = context.Roles.Where(r => r.Name.Equals(roleName, StringComparison.CurrentCultureIgnoreCase)).FirstOrDefault();
                return Request.CreateResponse(HttpStatusCode.OK, thisRole);
            }
            catch
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
        }

        [HttpPost]
        [Route("api/userAccount/CreateRole")]
        [CustomAuthorize(Roles = "Admin")]
        public HttpResponseMessage CreateRole(string RoleName)
        {
            try
            {
                context.Roles.Add(new Microsoft.AspNet.Identity.EntityFramework.IdentityRole()
                {
                    Name = RoleName
                });
                context.SaveChanges();
                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
        }

        [HttpPost]
        [Route("api/userAccount/DeleteRole")]
        [CustomAuthorize(Roles = "Admin")]
        public HttpResponseMessage DeleteRole(string RoleName)
        {
            try
            {
                var thisRole = context.Roles.Where(r => r.Name.Equals(RoleName, StringComparison.CurrentCultureIgnoreCase)).FirstOrDefault();
                context.Roles.Remove(thisRole);
                context.SaveChanges();
                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
        }


        [HttpPut]
        [Route("api/userAccount/EditRole")]
        [CustomAuthorize(Roles = "Admin")]
        public HttpResponseMessage EditRole(IdentityRole role)
        {
            try
            {
                context.Entry(role).State = System.Data.Entity.EntityState.Modified;
                context.SaveChanges();
                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
        }

        [HttpPost]
        [Route("api/userAccount/RoleAddToUser")]
        [CustomAuthorize(Roles = "Admin")]
        public HttpResponseMessage RoleAddToUser(string UserName, string RoleName) //them quyen cho user
        {
            try
            {
                ApplicationUser user = context.Users.Where(u => u.UserName.Equals(UserName, StringComparison.CurrentCultureIgnoreCase)).FirstOrDefault();
                UserManager.AddToRole(user.Id, RoleName);
                //add success!
                //response the role list to client
                var roleList = context.Roles.OrderBy(r => r.Name).ToList();

                return Request.CreateResponse(HttpStatusCode.OK, roleList);
            }
            catch
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
        }

        [HttpGet]
        [Route("api/userAccount/GetUserRoles")]
        [CustomAuthorize(Roles = "Admin")]
        public HttpResponseMessage GetUserRoles(string UserName) // tra ve nhung quyen cua user
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(UserName))
                {
                    ApplicationUser user = context.Users.Where(u => u.UserName.Equals(UserName, StringComparison.CurrentCultureIgnoreCase)).FirstOrDefault();
                    //var account = new AccountController();
                    //var UserRoles = account.UserManager.GetRoles(user.Id);
                    var UserRoles = UserManager.GetRoles(user.Id);

                    return Request.CreateResponse(HttpStatusCode.OK, UserRoles);
                }
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
            catch
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
        }


        [HttpPost]
        [Route("api/userAccount/DeleteRoleForUser")]
        [CustomAuthorize(Roles = "Admin")]
        public HttpResponseMessage DeleteRoleForUser(string UserName, string RoleName) // xoa quyen cua user
        {
            try
            {
                //var account = new AccountController();
                var ResultMessage = "";
                ApplicationUser user = context.Users.Where(u => u.UserName.Equals(UserName, StringComparison.CurrentCultureIgnoreCase)).FirstOrDefault();

                //if (account.UserManager.IsInRole(user.Id, RoleName))
                if (UserManager.IsInRole(user.Id, RoleName))
                {
                    //account.UserManager.RemoveFromRole(user.Id, RoleName);
                    UserManager.RemoveFromRole(user.Id, RoleName);
                    ResultMessage = "Role removed from this user successfully !";
                }
                else
                {
                    ResultMessage = "This user doesn't belong to selected role.";
                }

                return Request.CreateResponse(HttpStatusCode.BadRequest, ResultMessage);
            }
            catch
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
        }


        [HttpPost]
        [Route("api/userAccount/ChangePassword")]
        [CustomAuthorize(Roles = "Admin")]
        public HttpResponseMessage ChangePassword(ManagerUserViewModel user) //cap nhat user
        {
            try
            {
                //update user at aspNetUser Table
                //var ApplicationUserUpdate = new ApplicationUser
                //{
                //    Id = user.UserId,
                //    EmailConfirmed = false,
                //    PasswordHash = UserManager.PasswordHasher.HashPassword(user.Password),
                //    SecurityStamp = user.SecurityStamp,
                //    UserName = user.UserName,
                //    Email = user.Email,
                //};
                //context.Entry(ApplicationUserUpdate).State = System.Data.Entity.EntityState.Modified;
                //context.SaveChanges();

                //update userinfo at userinfo table
                //UserInfo userInfoModel = new UserInfo();
                ////check avatar
                //if (user.UserInfo.Avatar != "")
                //    user.UserInfo.Avatar = "/Content/UploadFiles/images/" + user.UserInfo.Avatar;
                //else
                //    user.UserInfo.Avatar = "/Content/UploadFiles/images/No_image_available.png";
                //userInfoModel = user.UserInfo;
                //using (var ncontext = new ApplicationDbContext())
                //{
                //    ncontext.Entry(userInfoModel).State = System.Data.Entity.EntityState.Modified;
                //    ncontext.SaveChanges();
                //};

                ////update userRoles at aspNetRoles table
                //ApplicationUser userFromDb = context.Users.Where(u => u.UserName.Equals(user.UserName, StringComparison.CurrentCultureIgnoreCase)).FirstOrDefault();
                //var userRoles = UserManager.GetRoles(user.UserId);
                //if (userRoles.Count() > 0)
                //{
                //    //remove user from current roles
                //    foreach (var role in userRoles)
                //    {
                //        UserManager.RemoveFromRole(userFromDb.Id, role);
                //    }
                //    //add user to new roles
                //    UserManager.AddToRole(user.UserId, user.Roles.SingleOrDefault().Name);
                //}
                ApplicationUser userFromDb = context.Users.Where(u => u.Id == user.UserId).FirstOrDefault();
                var responseMessage = new { title = "Lỗi đổi mật khẩu", message = "Error", isSuccess = false };
                if (UserManager.CheckPassword(userFromDb, user.Password))
                {
                    IdentityResult result= UserManager.ChangePassword(user.UserId, user.Password, user.NewPassword);
                    context.SaveChanges();
                    if(result.Succeeded==true)
                        responseMessage = new { title = "Thành công", message = "Đổi mật khẩu thành công", isSuccess = true };
                    else
                     responseMessage = new { title = "Lỗi đổi mật khẩu", message = "Mật khẩu phải bao gồm cả số, chữ thường và chữ in hoa", isSuccess = true };
                   


                }
                else
                {responseMessage = new { title = "Lỗi đổi mật khẩu", message = "Mật khẩu cũ không đúng", isSuccess = false };
                   

                }
                return Request.CreateResponse(HttpStatusCode.OK, responseMessage);
            }
            catch
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
        }

    }
}
