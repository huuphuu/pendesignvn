using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using PenDesign.Core.Model;
using PenDesign.Core.Models;
using PenDesign.Data;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;

namespace PenDesign.Data.MigrationConfiguration
{
    public class PenDesignMigrationConfiguration : DbMigrationsConfiguration<PenDesignDbContext>
    {
        public PenDesignMigrationConfiguration()
        {
            this.AutomaticMigrationDataLossAllowed = true;
            this.AutomaticMigrationsEnabled = true; 
        }

        protected override void Seed(PenDesignDbContext context)
        {
            base.Seed(context);

#if DEBUG
            #region seed ROLES & RolesMission
            //Roles
            var UserStore = new UserStore<ApplicationUser>(context);
            var UserManager = new UserManager<ApplicationUser>(UserStore);
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(new ApplicationDbContext()));

            var userAdminId = "";
            if (context.Users.Count() == 0 && context.Roles.Count() == 0)
            {
                var userInfo = new UserInfo()
                {
                    Email = "mrthanh.testemail@gmail.com",
                    Avatar = "/Content/UploadFiles/images/No_image_available.png",
                    Address = "59 Lê Lợi, P.Bến Nghé, Q.1, TP.HCM",
                    Skype = "mrthanh.kientao",
                    Yahoo = "lonely_start10882000@yahoo.com",
                    Facebook = "https://www.facebook.com/anhlee19",
                    Available = true
                };

                var user = new ApplicationUser { UserName = "administrator", Email = "mrthanh.testemail@gmail.com" };
                user.UserInfo = userInfo;
                var result = UserManager.Create(user, "Kt123456789!");
                if (result.Succeeded)
                {
                    if (!roleManager.RoleExists("Admin"))
                    {
                        var role = new IdentityRole("Admin");
                        var roleResult = roleManager.Create(role);
                        var userId = UserManager.FindByName("Administrator").Id;
                        UserManager.AddToRole(userId, "Admin");
                        userAdminId = userId;
                    }
                    if (!roleManager.RoleExists("Users"))
                    {
                        var role = new IdentityRole("Users");
                        var roleResult = roleManager.Create(role);
                    }

                    //var adminRoleId = db.ApplicationRoles.Where(m => m.Name == "Admin").Select(m => m.Id);
                    var adminRoleId = roleManager.Roles.Where(m => m.Name == "Admin").SingleOrDefault().Id;
                    var rolePermissions = new List<RolePermission>()
                    {
                        new RolePermission()
                        {
                            Name = "UserAccount-GetAllUsers",
                            Available = true,
                            ApplicationRoleId = adminRoleId
                        }
                    };

                    rolePermissions.ForEach(m => context.RolePermissions.AddOrUpdate(p => p.Name, m));
                    try
                    {
                        context.SaveChanges();
                    }
                    catch (Exception ex)
                    {
                        var msg = ex.Message;
                    }
                }
            }
            #endregion

            #region CONFIG
            var contactForm = "<h3>CÔNG TY TNHH ĐÀO TẠO VÀ TƯ VẤN TRUYỀN THÔNG THƯƠNG HIỆU BRAND</h3>";
            contactForm += "<p><i class='fa fa-map-marker text-danger'></i>&nbsp;&nbsp;14 Hoa Huệ, Phường 7, Quận Phú Nhuận, TP. HCM</p>";
            contactForm += "<p><i class='glyphicon glyphicon-plus'></i>&nbsp;&nbsp;Yêu cầu sẽ được phản hồi trong vòng 1h từ 8h:AM - 5h:PM.</p>";
            contactForm += "<p><i class='glyphicon glyphicon-plus'></i>&nbsp;&nbsp;Yêu cầu gửi sau giờ hành chính sẽ được trả lời vào sáng ngày làm việc tiếp theo.</p>";
            contactForm += "<p><i class='glyphicon glyphicon-phone'></i>&nbsp;&nbsp;Hotline: 0917.934.141 - Mr.Toàn - Tư vấn viên</p>";
            contactForm += "<p><i class='glyphicon glyphicon-triangle-right'></i>Qúy vị có thể liên hệ qua email, vui lòng cung cấp thông tin một cách đầy đủ, chính xác.</p>";

            var footerContent = "<div class='col-xs-12 col-md-3 text-left'>";
            footerContent += "<h2>Trụ sở Chính HCM</h2>";
            footerContent += "<ul>";
            footerContent += "<li><i class='glyphicon glyphicon-home'></i>&nbsp;&nbsp;Toà nhà BRAND - Số 14 Hoa Huệ, Phường 7, Quận Phú Nhuận, TP. HCM</li>";
            footerContent += "<li><i class='glyphicon glyphicon-phone'></i>&nbsp;&nbsp;<a href='tel:0917934141' title='Gọi điện'>0917 934 141</a></li>";
            footerContent += "<li><i class='glyphicon glyphicon-phone'></i>&nbsp;&nbsp;<a href='tel:0938089926' title='Gọi điện'>0938 0899 26</a></li>";
            footerContent += "<li><i class='glyphicon glyphicon-envelope'></i>&nbsp;&nbsp;info@thapsangthuonghieu.edu.vn</li>";
            footerContent += "</ul>";
            footerContent += "</div>";
            footerContent += "<div class='col-xs-12 col-md-3 text-left'>";
            footerContent += "<h2>Văn phòng đạo tạo</h2>";
            footerContent += "<ul>";
            footerContent += "<li><i class='glyphicon glyphicon-home'></i>&nbsp;&nbsp;TÒA NHÀ VĂN HÓA SINH VIÊN LẦU 2 – 643 ĐIỆN BIÊN PHỦ – QUẬN 3 – TP HỒ CHÍ MINH</li>";
            footerContent += "<li><i class='glyphicon glyphicon-phone'></i>&nbsp;&nbsp;<a href='tel:0917934141' title='Gọi điện'>0917 934 141</a></li>";
            footerContent += "<li><i class='glyphicon glyphicon-phone'></i>&nbsp;&nbsp;<a href='tel:0938089926' title='Gọi điện'>0938 0899 26</a></li>";
            footerContent += "<li><i class='glyphicon glyphicon-envelope'></i>&nbsp;&nbsp;info@thapsangthuonghieu.edu.vn</li>";
            footerContent += "</ul></div>";

            if (context.Configs.Count() == 0)
            {
                var config = new Config()
                {
                    CompanyName = "CÔNG TY TNHH PenDesign",
                    About = "CÔNG TY TNHH PenDesign",
                    Slogan = "",
                    Email = "mrthanh.testemail@gmail.com",
                    EmailPassword = "kt123456789",
                    EmailPort = 487,
                    EmailSignature = "<b>Mr.Thanh - Web Deverloper<br />Website: www.thanhit.net<br />Phone: 099.661.4884 - 0938.039.131</b>",
                    //for SEO
                    GoogleAnalytics = "<script>var google = 'googleAnalytics';</script>",
                    FacebookRetager = "<script>facebook<script>",
                    GoogleWebMaster = "<meta name='google-site-verification' content='CqoI2eXvbapz6SW47AALzXpx4_ozI3mZb13QBP541d4' />",

                    Address = "14 Hoa Huệ, Phường 7, Quận Phú Nhuận, TP. HCM",
                    GoogleMap = "<iframe class='embed-responsive-item' src='https://www.google.com/maps/embed?pb=!1m18!1m12!1m3!1d3919.484596702444!2d106.70130999999999!3d10.774148!2m3!1f0!2f0!3f0!3m2!1i1024!2i768!4f13.1!3m3!1m2!1s0x31752f476aafeac3%3A0x706052921c500887!2zNTkgTMOqIEzhu6NpLCBC4bq_biBOZ2jDqSwgUXXhuq1uIDEsIEjhu5MgQ2jDrSBNaW5oLCBWaeG7h3QgTmFt!5e0!3m2!1svi!2s!4v1435669394151' frameborder='0' style='border:0' allowfullscreen></iframe>",
                    Phone = "0996614884",
                    BannerLogo = "/Content/images/logo.png",
                    LogoUrl = "/Content/images/Logo-Brand.png",
                    ContactForm = contactForm,
                    FooterContent = footerContent,
                    Yahoo = "lonely_start10882000@yahoo.com",
                    Skype = "mrthanh.kientao",
                    FacebookSocial = "https://www.facebook.com/anhlee19",
                    GoogleSocial = "www.google.com",
                    TwitterSocial = "www.twitter.com",
                    PicasaSocial = "",
                    Youtube = "www.youtube.com",
                    Instagram = "www.instagram.com"
                };

                context.Configs.Add(config);

                try
                {
                    context.SaveChanges();
                }
                catch (Exception ex)
                {
                    var msg = ex.Message;
                }
            }
            #endregion

            #region AdminMenu
            if (context.AdminMenus.Count() == 0)
            {
                var adminMenuList = new List<AdminMenu>()
                {
                    //Main menu
                    new AdminMenu() { 
                        Name = "Thông báo", Url = "controlPanel.notification", CssIcon =  "fa fa-bell-o", LabelCss =  "",
                        Type = "notification", Parent = 0, Order = 1, Available = true, 
                        IsAdmin = true
                    },
                    new AdminMenu() { 
                        Name = "Thư viện", Url = "#", CssIcon =  "fa fa-folder-open", LabelCss =  "fa fa-angle-left", 
                        Type = "", Parent = 0, Order = 2, Available = true,
                        IsAdmin = true
                    },
                    new AdminMenu() { 
                        Name = "Banner", Url = "controlPanel.banner", CssIcon =  "fa fa-folder-open", LabelCss =  "fa fa-angle-left", 
                        Type = "", Parent = 0, Order = 3, Available = true,
                        IsAdmin = true
                    },
                    new AdminMenu() { 
                        Name = "Nhóm/Thành viên", Url = "#", CssIcon =  "fa fa-user-md", LabelCss =  "fa fa-angle-left", 
                        Type = "", Parent = 0, Order = 4, Available = true,
                        IsAdmin = true
                    },
                    new AdminMenu() { 
                        Name = "Khóa học/Học viên", Url = "#", CssIcon =  "fa fa-folder", LabelCss =  "fa fa-angle-left", 
                        Type = "", Parent = 0, Order = 5, Available = true,
                        IsAdmin = true
                    },
                    new AdminMenu() { 
                        Name = "Dịch vụ", Url = "#", CssIcon =  "fa fa-rocket", LabelCss =  "fa fa-angle-left", 
                        Type = "", Parent = 0, Order = 6, Available = true,
                        IsAdmin = true
                    },
                    new AdminMenu() { 
                        Name = "Tin tức", Url = "#", CssIcon =  "fa fa-file-text", LabelCss =  "fa fa-angle-left", 
                        Type = "", Parent = 0, Order = 7, Available = true,
                        IsAdmin = true
                    },
                    new AdminMenu() { 
                        Name = "Sự kiện", Url = "#", CssIcon =  "fa fa-glass", LabelCss =  "fa fa-angle-left", 
                        Type = "", Parent = 0, Order = 8, Available = true,
                        IsAdmin = true
                    },
                    new AdminMenu() { 
                        Name = "Đối tác", Url = "#", CssIcon =  "fa fa-group", LabelCss =  "fa fa-angle-left", 
                        Type = "", Parent = 0, Order = 9, Available = true,
                        IsAdmin = true
                    },
                    new AdminMenu() { 
                        Name = "Email", Url = "#", CssIcon =  "fa fa-envelope", LabelCss =  "fa fa-angle-left", 
                        Type = "", Parent = 0, Order = 10, Available = true,
                        IsAdmin = true
                    },
                    new AdminMenu() { 
                        Name = "Cấu hình", Url = "controlPanel.configs", CssIcon = "fa fa-wrench", LabelCss = "", 
                        Type = "", Parent = 0, Order = 11, Available = true,
                        IsAdmin = true
                    },
                    //--------------
                    //SubMenu Thu vien
                    //--------------
                    new AdminMenu() { 
                        Name = "Quản lý hình ảnh", Url = "controlPanel.galleryList({galleryCategoryId: 1})", CssIcon = "fa fa-list-ol", LabelCss = "", 
                        Type = "", Parent = 2, Order = 1, Available = true,
                        IsAdmin = true
                    },
                    new AdminMenu() { 
                        Name = "Quản lý Video Clip", Url = "controlPanel.galleryList({galleryCategoryId: 2})", CssIcon = "fa fa-list-ol", LabelCss = "", 
                        Type = "", Parent = 2, Order = 2, Available = true,
                        IsAdmin = true
                    },
                    //--------------
                    //SubMenu Banner
                    //--------------
                    new AdminMenu() { 
                        Name = "Thêm Banner", Url = "controlPanel.addBanner", CssIcon = "fa fa-users", LabelCss = "", 
                        Type = "", Parent = 3, Order = 1, Available = true,
                        IsAdmin = true
                    },
                    new AdminMenu() { 
                        Name = "Danh sách Banner", Url = "controlPanel.bannerList", CssIcon = "fa fa-user", LabelCss = "", 
                        Type = "", Parent = 3, Order = 2, Available = true,
                        IsAdmin = true
                    },
                    //--------------
                    //SubMenu Nhom/Thanh vien
                    //--------------
                    new AdminMenu() { 
                        Name = "Danh sách Nhóm", Url = "controlPanel.userGroupsList", CssIcon = "fa fa-users", LabelCss = "", 
                        Type = "", Parent = 4, Order = 1, Available = true,
                        IsAdmin = true
                    },
                    new AdminMenu() { 
                        Name = "Danh sách Thành viên", Url = "controlPanel.userList", CssIcon = "fa fa-user", LabelCss = "", 
                        Type = "", Parent = 4, Order = 2, Available = true,
                        IsAdmin = true
                    },
                    //--------------
                    //SubMenu Khoa hoc/Hoc vien
                    //--------------
                    new AdminMenu() { 
                        Name = "Thêm khóa học", Url = "controlPanel.addCourse", CssIcon = "fa fa-plus-circle", LabelCss = "", 
                        Type = "", Parent = 5, Order = 1, Available = true,
                        IsAdmin = true
                    },
                    new AdminMenu() { 
                        Name = "Danh sách khóa học", Url = "controlPanel.coursesList", CssIcon = "fa fa-list-ol", LabelCss = "", 
                        Type = "", Parent = 5, Order = 2, Available = true,
                        IsAdmin = true
                    },
                    new AdminMenu() { 
                        Name = "Thêm học viên", Url = "controlPanel.addStudent", CssIcon = "fa fa-plus-circle", LabelCss = "", 
                        Type = "", Parent = 5, Order = 3, Available = true,
                        IsAdmin = true
                    },
                    new AdminMenu() { 
                        Name = "Danh sách học viên", Url = "controlPanel.studentsList", CssIcon = "fa fa-list-ol", LabelCss = "", 
                        Type = "", Parent = 5, Order = 4, Available = true,
                        IsAdmin = true
                    },
                    //--------------
                    //SubMenu Dich vu
                    //--------------
                    new AdminMenu() { 
                        Name = "Thêm bài mới", Url = "controlPanel.addNews({newsCategoryId: 1})", CssIcon = "fa fa-plus-circle", LabelCss = "", 
                        Type = "", Parent = 6, Order = 1, Available = true,
                        IsAdmin = true
                    },
                    new AdminMenu() { 
                        Name = "Danh sách Bài viết", Url = "controlPanel.newsList({newsCategoryId: 1})", CssIcon = "fa fa-list-ol", LabelCss = "", 
                        Type = "", Parent = 6, Order = 2, Available = true,
                        IsAdmin = true
                    },
                    //--------------
                    //SubMenu Tin tuc
                    //--------------
                    new AdminMenu() { 
                        Name = "Thêm bài mới", Url = "controlPanel.addNews({newsCategoryId: 3})", CssIcon = "fa fa-plus-circle", LabelCss = "", 
                        Type = "", Parent = 7, Order = 1, Available = true,
                        IsAdmin = true
                    },
                    new AdminMenu() { 
                        Name = "Danh sách Bài viết", Url = "controlPanel.newsList({newsCategoryId: 3})", CssIcon = "fa fa-list-ol", LabelCss = "", 
                        Type = "", Parent = 7, Order = 2, Available = true,
                        IsAdmin = true
                    },
                    //--------------
                    //SubMenu Su kien
                    //--------------
                    new AdminMenu() { 
                        Name = "Thêm sự kiện mới", Url = "controlPanel.addNews({newsCategoryId: 2})", CssIcon = "fa fa-plus-circle", LabelCss = "", 
                        Type = "", Parent = 8, Order = 1, Available = true,
                        IsAdmin = true
                    },
                    new AdminMenu() { 
                        Name = "Danh sách sự kiện", Url = "controlPanel.newsList({newsCategoryId: 2})", CssIcon = "fa fa-list-ol", LabelCss = "", 
                        Type = "", Parent = 8, Order = 2, Available = true,
                        IsAdmin = true
                    },
                    //--------------
                    //SubMenu Doi tac
                    //--------------
                    new AdminMenu() { 
                        Name = "Thêm đối tác", Url = "controlPanel.addNews({newsCategoryId: 4})", CssIcon = "fa fa-plus-circle", LabelCss = "", 
                        Type = "", Parent = 9, Order = 1, Available = true,
                        IsAdmin = true
                    },
                    new AdminMenu() { 
                        Name = "Danh sách đối tác", Url = "controlPanel.newsList({newsCategoryId: 4})", CssIcon = "fa fa-list-ol", LabelCss = "", 
                        Type = "", Parent = 9, Order = 2, Available = true,
                        IsAdmin = true
                    },
                    //--------------
                    //SubMenu Email
                    //--------------
                    new AdminMenu() { 
                        Name = "Email đăng ký", Url = "controlPanel.email", CssIcon = "fa fa-list-ol", LabelCss = "", 
                        Type = "", Parent = 10, Order = 1, Available = true,
                        IsAdmin = true
                    },
                    new AdminMenu() { 
                        Name = "Email liên hệ", Url = "controlPanel.contactEmail", CssIcon = "fa fa-list-ol", LabelCss = "", 
                        Type = "", Parent = 10, Order = 2, Available = true,
                        IsAdmin = true
                    }
                    //--------------
                    //SubMenu Cau Hinh
                    //--------------
                    //new AdminMenu() { 
                    //    Name = "Thông tin công ty 1", Url = "controlPanel.configs", CssIcon = "fa fa-plus-circle", LabelCss = "", 
                    //    Type = "", Parent = 11, Order = 1, Available = true,
                    //    IsAdmin = true
                    //},
                    //new AdminMenu() { 
                    //    Name = "Thông tin công ty 2", Url = "controlPanel.configs", CssIcon = "fa fa-list-ol", LabelCss = "", 
                    //    Type = "", Parent = 11, Order = 2, Available = true,
                    //    IsAdmin = true
                    //},
                };

                adminMenuList.ForEach(m => context.AdminMenus.AddOrUpdate(p => p.Name, m));

                try
                {
                    context.SaveChanges();
                }
                catch (Exception ex)
                {
                    var msg = ex.Message;
                }
            }
            #endregion

#endif
        }
    }
}