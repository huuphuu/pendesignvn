﻿using Microsoft.AspNet.Identity;
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
            var detail = "Tincidunt integer eu augue augue nunc elit dolor, luctus placerat scelerisque euismod, iaculis eu lacus nunc mi elit, vehicula ut laoreet ac, aliquam sit amet justo nunc tempor, metus vel.<br/>";
            detail += "Laoreet ac, aliquam sit amet justo nunc tempor, metus vel placerat suscipit, orci nisl iaculis eros, a tincidunt nisi odio eget lorem nulla condimentum tempor mattis ut vitae feugiat augue cras ut metus a risus iaculis scelerisque eu ac ante fusce non varius purus aenean nec magna felis fusce vestibulum.<br/>";
            detail += "Scelerisque euismod, iaculis eu lacus nunc mi elit, vehicula ut laoreet ac, aliquam sit amet justo nunc tempor, metus vel placerat suscipit, orci nisl iaculis eros, a tincidunt nisi odio eget lorem nulla condimentum tempor mattis ut vitae feugiat augue.";
            

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
//                    var adminRoleId = roleManager.Roles.Where(m => m.Name == "Admin").SingleOrDefault().Id;
//                    var rolePermissions = new List<RolePermission>()
//                    {
//                        new RolePermission()
//                        {
//                            Name = "UserAccount-GetAllUsers",
//                            Available = true,
////                            ApplicationRoleId = adminRoleId
//                        }
//                    };
//
//                    rolePermissions.ForEach(m => context.RolePermissions.AddOrUpdate(p => p.Name, m));
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

            #region Banner
            if (context.Banners.Count() == 0)
            {
                var userId = UserManager.FindByName("Administrator").Id;
                var bannerList = new List<Banner>()
                {
                    //Main menu
                    new Banner() { 
                        Position = 1, MediaType = 1, Status = true, Deleted = false, ZOrder = 1, // Type = 1, 
                        Name = "Banner 1", //MediaUrl = "/Content/images/slide.jpg", MediaThumbUrl = "/Content/images/thumb.png",
                        CreatedById = userId, CreatedDateTime = DateTime.Now,
                        ModifiedById = userId, ModifiedDateTime = DateTime.Now,
                        BannerMappings = new List<BannerMapping>()
                        {
                            new BannerMapping()
                            {
                                BannerId = 1, LanguageId = 129, Status = true, Deleted = false,
                                Name = "Tên Banner 1", Description = "Các dự án nội thất thông minh cho nhà của bạn",
                                MediaUrl = "/Content/images/slide.jpg", MediaThumbUrl = "/Content/images/thumb.png",
                                CreatedById = userId, CreatedDateTime = DateTime.Now,
                                ModifiedById = userId, ModifiedDateTime = DateTime.Now
                            },
                            new BannerMapping()
                            {
                                BannerId = 1, LanguageId = 29, Status = true, Deleted = false,
                                Name = "Banner Name 1", Description = "Clever interior projects for your home",
                                MediaUrl = "/Content/images/slide.jpg", MediaThumbUrl = "/Content/images/thumb.png",
                                CreatedById = userId, CreatedDateTime = DateTime.Now,
                                ModifiedById = userId, ModifiedDateTime = DateTime.Now
                            }
                        }
                    },
                    new Banner() { 
                        Position = 1, MediaType = 1, Status = true, Deleted = false, ZOrder = 2, //Type = 1,
                        Name = "Banner 2", // MediaUrl = "/Content/images/slide1.jpg", MediaThumbUrl = "/Content/images/thumb1.png",
                        CreatedById = userId, CreatedDateTime = DateTime.Now,
                        ModifiedById = userId, ModifiedDateTime = DateTime.Now,
                        BannerMappings = new List<BannerMapping>()
                        {
                            new BannerMapping()
                            {
                                BannerId = 2, LanguageId = 129, Status = true, Deleted = false,
                                Name = "Tên Banner 2", Description = "Ý tưởng cải tiến nhà cho bạn",
                                MediaUrl = "/Content/images/slide1.jpg", MediaThumbUrl = "/Content/images/thumb1.png",
                                CreatedById = userId, CreatedDateTime = DateTime.Now,
                                ModifiedById = userId, ModifiedDateTime = DateTime.Now
                            },
                            new BannerMapping()
                            {
                                BannerId = 2, LanguageId = 29, Status = true, Deleted = false,
                                Name = "Banner Name 2", Description = "Home improvement ideas for you",
                                MediaUrl = "/Content/images/slide1.jpg", MediaThumbUrl = "/Content/images/thumb1.png",
                                CreatedById = userId, CreatedDateTime = DateTime.Now,
                                ModifiedById = userId, ModifiedDateTime = DateTime.Now
                            }
                        }
                    },
                    new Banner() { 
                        Position = 1, MediaType = 1, Status = true, Deleted = false, ZOrder = 3, //Type = 1, 
                        Name = "Banner 3", // MediaUrl = "/Content/images/slide2.jpg", MediaThumbUrl = "/Content/images/thumb2.png",
                        CreatedById = userId, CreatedDateTime = DateTime.Now,
                        ModifiedById = userId, ModifiedDateTime = DateTime.Now,
                        BannerMappings = new List<BannerMapping>()
                        {
                            new BannerMapping()
                            {
                                BannerId = 3, LanguageId = 129, Status = true, Deleted = false,
                                Name = "Tên Banner 3", Description = "Thủ thuật thiết kế cao cấp",
                                MediaUrl = "/Content/images/slide2.jpg", MediaThumbUrl = "/Content/images/thumb2.png",
                                CreatedById = userId, CreatedDateTime = DateTime.Now,
                                ModifiedById = userId, ModifiedDateTime = DateTime.Now
                            },
                            new BannerMapping()
                            {
                                BannerId = 3, LanguageId = 29, Status = true, Deleted = false,
                                Name = "Banner Name 3", Description = "Premium design tips",
                                MediaUrl = "/Content/images/slide2.jpg", MediaThumbUrl = "/Content/images/thumb2.png",
                                CreatedById = userId, CreatedDateTime = DateTime.Now,
                                ModifiedById = userId, ModifiedDateTime = DateTime.Now
                            }
                        }
                    },
                    new Banner() { 
                        Position = 1, MediaType = 1, Status = true, Deleted = false, ZOrder = 4, //Type = 1, 
                        Name = "Banner 4",  //MediaUrl = "/Content/images/slide3.jpg", MediaThumbUrl = "/Content/images/thumb3.png",
                        CreatedById = userId, CreatedDateTime = DateTime.Now,
                        ModifiedById = userId, ModifiedDateTime = DateTime.Now,
                        BannerMappings = new List<BannerMapping>()
                        {
                            new BannerMapping()
                            {
                                BannerId = 4, LanguageId = 129, Status = true, Deleted = false,
                                Name = "Tên Banner 4", Description = "Chỉ có những ý tưởng sáng tạo",
                                MediaUrl = "/Content/images/slide3.jpg", MediaThumbUrl = "/Content/images/thumb3.png",
                                CreatedById = userId, CreatedDateTime = DateTime.Now,
                                ModifiedById = userId, ModifiedDateTime = DateTime.Now
                            },
                            new BannerMapping()
                            {
                                BannerId = 4, LanguageId = 29, Status = true, Deleted = false,
                                Name = "Banner Name 4", Description = "Only creative ideas",
                                MediaUrl = "/Content/images/slide3.jpg", MediaThumbUrl = "/Content/images/thumb3.png",
                                CreatedById = userId, CreatedDateTime = DateTime.Now,
                                ModifiedById = userId, ModifiedDateTime = DateTime.Now
                            }
                        }
                    }
                    
                };

                bannerList.ForEach(m => context.Banners.AddOrUpdate(p => p.Id, m));

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

            #region Project
            if (context.Projects.Count() == 0)
            {
                var userId = UserManager.FindByName("Administrator").Id;
                var projectList = new List<Project>()
                {
                    //Main menu
                    //project 1
                    new Project() { 
                        Name = "Project 1", Type = 1, Status = true, Deleted = false, ZOrder = 1,
                        CreatedById = userId, CreatedDateTime = DateTime.Now,
                        ModifiedById = userId, ModifiedDateTime = DateTime.Now,
                        News = new List<News>
                        {
                            new News()
                            {
                                Name = "Project 1 - News", Status = true, Deleted = false,  NewsCategoryId = null,  ListTagId = "",  
                                CreatedById = userId, CreatedDateTime = DateTime.Now,
                                ModifiedById = userId, ModifiedDateTime = DateTime.Now,
                                NewsMappings = new List<NewsMapping>()
                                {
                                    new NewsMapping()
                                    {
                                        NewsId = 1, LanguageId = 129, Status = true, Deleted = false,
                                        Intro = "Dự Án 1 - News - Intro", Detail = "Dự Án 1 - News - Detail", MetaData = "<meta name='author' content='thanhit.net'>",
                                        Title = "Dự Án 1 - News ", Keyword = "Dự án 1 - News Keyword", Description = "Dự án 1 - News Description",
                                        CreatedById = userId, CreatedDateTime = DateTime.Now,
                                        ModifiedById = userId, ModifiedDateTime = DateTime.Now
                                    },
                                    new NewsMapping()
                                    {
                                        NewsId = 1, LanguageId = 29, Status = true, Deleted = false,
                                        Intro = "Project 1 - News - Intro", Detail = "Project 1 - News - Detail", MetaData = "<meta name='author' content='thanhit.net'>",
                                        Title = "Project 1 - News", Keyword = "Project 1 - News Keyword", Description = "Project 1 - News Description",
                                        CreatedById = userId, CreatedDateTime = DateTime.Now,
                                        ModifiedById = userId, ModifiedDateTime = DateTime.Now
                                    }
                                }
                            }
                        },
                        
                        ProjectMappings = new List<ProjectMapping>()
                        {
                            new ProjectMapping()
                            {
                                ProjectId = 1, LanguageId = 129, Status = true, Deleted = false,
                                Title = "Tên Dự Án 1", Keyword = "Dự Án 1 Keyword", Description = "Dự án 1 Description", MetaData = "<meta name='author' content='thanhit.net'>",
                                Intro = "Dự Án 1 - Intro", Detail = "Dự Án 1 Detail",
                                CreatedById = userId, CreatedDateTime = DateTime.Now,
                                ModifiedById = userId, ModifiedDateTime = DateTime.Now
                            },
                            new ProjectMapping()
                            {
                                ProjectId = 1, LanguageId = 29, Status = true, Deleted = false,
                                Title = "Project Name 1", Keyword = "Project 1 Keyword", Description = "Project 1 Description", MetaData = "<meta name='author' content='thanhit.net'>",
                                Intro = "Project 1 - Intro", Detail = "Project 1 Detail",
                                CreatedById = userId, CreatedDateTime = DateTime.Now,
                                ModifiedById = userId, ModifiedDateTime = DateTime.Now
                            }
                        },
                        ProjectImages = new List<ProjectImage>()
                        {
                            new ProjectImage()
                            {
                                ProjectId = 1, Type = 1, Status = true, Deleted = false, ZOrder = 1,
                                ResourceUrl = "/Content/images/slide.jpg", Thumbnail = "/Content/images/page2_img1.jpg",
                                CreatedById = userId, CreatedDateTime = DateTime.Now,
                                ModifiedById = userId, ModifiedDateTime = DateTime.Now,
                                ProjectImageMappings = new List<ProjectImageMapping>()
                                {
                                    new ProjectImageMapping()
                                    {
                                        ProjectImageId = 1, LanguageId = 129, Status = true, Deleted = false,
                                        Name = "Tên ảnh 1-  Dự Án 1", Description = "Dự án 1 - ảnh 1 Description",
                                        CreatedById = userId, CreatedDateTime = DateTime.Now,
                                        ModifiedById = userId, ModifiedDateTime = DateTime.Now  
                                    },
                                    new ProjectImageMapping()
                                    {
                                        ProjectImageId = 1, LanguageId = 29, Status = true, Deleted = false,
                                        Name = "Project Name 2 - image 1", Description = "Project 1 - image 1 Description",
                                        CreatedById = userId, CreatedDateTime = DateTime.Now,
                                        ModifiedById = userId, ModifiedDateTime = DateTime.Now 
                                    }
                                }
                            },
                            new ProjectImage()
                            {
                                ProjectId = 1, Type = 1, Status = true, Deleted = false, ZOrder = 2,
                                ResourceUrl = "/Content/images/slide1.jpg", Thumbnail = "/Content/images/page2_img2.jpg",
                                CreatedById = userId, CreatedDateTime = DateTime.Now,
                                ModifiedById = userId, ModifiedDateTime = DateTime.Now,
                                ProjectImageMappings = new List<ProjectImageMapping>()
                                {
                                    new ProjectImageMapping()
                                    {
                                        ProjectImageId = 2, LanguageId = 129, Status = true, Deleted = false,
                                        Name = "Tên ảnh 2-  Dự Án 1", Description = "Dự án 1 - ảnh 2 Description",
                                        CreatedById = userId, CreatedDateTime = DateTime.Now,
                                        ModifiedById = userId, ModifiedDateTime = DateTime.Now  
                                    },
                                    new ProjectImageMapping()
                                    {
                                        ProjectImageId = 2, LanguageId = 29, Status = true, Deleted = false,
                                        Name = "Project Name 1 - image 2", Description = "Project 1 - image 2 Description",
                                        CreatedById = userId, CreatedDateTime = DateTime.Now,
                                        ModifiedById = userId, ModifiedDateTime = DateTime.Now 
                                    }
                                }
                            },
                            new ProjectImage()
                            {
                                ProjectId = 1, Type = 1, Status = true, Deleted = false, ZOrder = 3,
                                ResourceUrl = "/Content/images/slide2.jpg", Thumbnail = "/Content/images/page2_img3.jpg",
                                CreatedById = userId, CreatedDateTime = DateTime.Now,
                                ModifiedById = userId, ModifiedDateTime = DateTime.Now,
                                ProjectImageMappings = new List<ProjectImageMapping>()
                                {
                                    new ProjectImageMapping()
                                    {
                                        ProjectImageId = 3, LanguageId = 129, Status = true, Deleted = false,
                                        Name = "Tên ảnh 3-  Dự Án 1", Description = "Dự án 1 - ảnh 3 Description",
                                        CreatedById = userId, CreatedDateTime = DateTime.Now,
                                        ModifiedById = userId, ModifiedDateTime = DateTime.Now  
                                    },
                                    new ProjectImageMapping()
                                    {
                                        ProjectImageId = 3, LanguageId = 29, Status = true, Deleted = false,
                                        Name = "Project Name 1 - image 3", Description = "Project 1 - image 3 Description",
                                        CreatedById = userId, CreatedDateTime = DateTime.Now,
                                        ModifiedById = userId, ModifiedDateTime = DateTime.Now 
                                    }
                                }
                            }
                        }
                    },
                    //project 2
                    new Project() { 
                        Name = "Project 2", Type = 1, Status = true, Deleted = false, ZOrder = 2,
                        CreatedById = userId, CreatedDateTime = DateTime.Now,
                        ModifiedById = userId, ModifiedDateTime = DateTime.Now,
                        News = new List<News>
                        {
                            new News()
                            {
                                Name = "Project 2 - News", Status = true, Deleted = false,  NewsCategoryId = null,  ListTagId = "", 
                                CreatedById = userId, CreatedDateTime = DateTime.Now,
                                ModifiedById = userId, ModifiedDateTime = DateTime.Now,
                                NewsMappings = new List<NewsMapping>()
                                {
                                    new NewsMapping()
                                    {
                                        NewsId = 2, LanguageId = 129, Status = true, Deleted = false,
                                        Intro = "Dự Án 2 - News - Intro", Detail = "Dự Án 2 - News - Detail", MetaData = "<meta name='author' content='thanhit.net'>",
                                        Title = "Dự Án 2 - News ", Keyword = "Dự án 2 - News Keyword", Description = "Dự án 2 - News Description",
                                        CreatedById = userId, CreatedDateTime = DateTime.Now,
                                        ModifiedById = userId, ModifiedDateTime = DateTime.Now
                                    },
                                    new NewsMapping()
                                    {
                                        NewsId = 1, LanguageId = 29, Status = true, Deleted = false,
                                        Intro = "Project 2 - News - Intro", Detail = "Project 2- News - Detail", MetaData = "<meta name='author' content='thanhit.net'>",
                                        Title = "Project 2 - News", Keyword = "Project 2 - News Keyword", Description = "Project 2 - News Description",
                                        CreatedById = userId, CreatedDateTime = DateTime.Now,
                                        ModifiedById = userId, ModifiedDateTime = DateTime.Now
                                    }
                                }
                            }
                        },
                        ProjectMappings = new List<ProjectMapping>()
                        {
                            new ProjectMapping()
                            {
                                ProjectId = 2, LanguageId = 129, Status = true, Deleted = false,
                                Title = "Tên Dự Án 2", Keyword = "Dự Án 2 Keyword", Description = "Dự án 2 Description", MetaData = "<meta name='author' content='thanhit.net'>",
                                Intro = "Dự Án 2 - Intro", Detail = "Dự Án 2 Detail",
                                CreatedById = userId, CreatedDateTime = DateTime.Now,
                                ModifiedById = userId, ModifiedDateTime = DateTime.Now
                            },
                            new ProjectMapping()
                            {
                                ProjectId = 2, LanguageId = 29, Status = true, Deleted = false,
                                Title = "Project Name 2", Keyword = "Project 2 Keyword", Description = "Project 2 Description", MetaData = "<meta name='author' content='thanhit.net'>",
                                Intro = "Project 2 - Intro", Detail = "Project 2 Detail",
                                CreatedById = userId, CreatedDateTime = DateTime.Now,
                                ModifiedById = userId, ModifiedDateTime = DateTime.Now
                            }
                        },
                        ProjectImages = new List<ProjectImage>()
                        {
                            new ProjectImage()
                            {
                                ProjectId = 2, Type = 1, Status = true, Deleted = false, ZOrder = 1,
                                ResourceUrl = "/Content/images/slide.jpg", Thumbnail = "/Content/images/page2_img1.jpg",
                                CreatedById = userId, CreatedDateTime = DateTime.Now,
                                ModifiedById = userId, ModifiedDateTime = DateTime.Now,
                                ProjectImageMappings = new List<ProjectImageMapping>()
                                {
                                    new ProjectImageMapping()
                                    {
                                        ProjectImageId = 4, LanguageId = 129, Status = true, Deleted = false,
                                        Name = "Tên ảnh 1-  Dự Án 2", Description = "Dự án 2 - ảnh 1 Description",
                                        CreatedById = userId, CreatedDateTime = DateTime.Now,
                                        ModifiedById = userId, ModifiedDateTime = DateTime.Now  
                                    },
                                    new ProjectImageMapping()
                                    {
                                        ProjectImageId = 4, LanguageId = 29, Status = true, Deleted = false,
                                        Name = "Project Name 2 - image 1", Description = "Project 2 - image 1 Description",
                                        CreatedById = userId, CreatedDateTime = DateTime.Now,
                                        ModifiedById = userId, ModifiedDateTime = DateTime.Now 
                                    }
                                }
                            },
                            new ProjectImage()
                            {
                                ProjectId = 2, Type = 1, Status = true, Deleted = false, ZOrder = 2,
                                ResourceUrl = "/Content/images/slide1.jpg", Thumbnail = "/Content/images/page2_img2.jpg",
                                CreatedById = userId, CreatedDateTime = DateTime.Now,
                                ModifiedById = userId, ModifiedDateTime = DateTime.Now,
                                ProjectImageMappings = new List<ProjectImageMapping>()
                                {
                                    new ProjectImageMapping()
                                    {
                                        ProjectImageId = 5, LanguageId = 129, Status = true, Deleted = false,
                                        Name = "Tên ảnh 2-  Dự Án 2", Description = "Dự án 2 - ảnh 2 Description",
                                        CreatedById = userId, CreatedDateTime = DateTime.Now,
                                        ModifiedById = userId, ModifiedDateTime = DateTime.Now  
                                    },
                                    new ProjectImageMapping()
                                    {
                                        ProjectImageId = 5, LanguageId = 29, Status = true, Deleted = false,
                                        Name = "Project Name 2 - image 2", Description = "Project 2 - image 2 Description",
                                        CreatedById = userId, CreatedDateTime = DateTime.Now,
                                        ModifiedById = userId, ModifiedDateTime = DateTime.Now 
                                    }
                                }
                            },
                            new ProjectImage()
                            {
                                ProjectId = 2, Type = 1, Status = true, Deleted = false, ZOrder = 3,
                                ResourceUrl = "/Content/images/slide2.jpg", Thumbnail = "/Content/images/page2_img3.jpg",
                                CreatedById = userId, CreatedDateTime = DateTime.Now,
                                ModifiedById = userId, ModifiedDateTime = DateTime.Now,
                                ProjectImageMappings = new List<ProjectImageMapping>()
                                {
                                    new ProjectImageMapping()
                                    {
                                        ProjectImageId = 6, LanguageId = 129, Status = true, Deleted = false,
                                        Name = "Tên ảnh 3-  Dự Án 2", Description = "Dự án 2 - ảnh 3 Description",
                                        CreatedById = userId, CreatedDateTime = DateTime.Now,
                                        ModifiedById = userId, ModifiedDateTime = DateTime.Now  
                                    },
                                    new ProjectImageMapping()
                                    {
                                        ProjectImageId = 6, LanguageId = 29, Status = true, Deleted = false,
                                        Name = "Project Name 2 - image 3", Description = "Project 2 - image 3 Description",
                                        CreatedById = userId, CreatedDateTime = DateTime.Now,
                                        ModifiedById = userId, ModifiedDateTime = DateTime.Now 
                                    }
                                }
                            }
                        }
                    },
                    // project 3
                    new Project() { 
                        Name = "Project 3", Type = 1, Status = true, Deleted = false, ZOrder = 3,
                        CreatedById = userId, CreatedDateTime = DateTime.Now,
                        ModifiedById = userId, ModifiedDateTime = DateTime.Now,
                        News = new List<News>
                        {
                            new News()
                            {
                                Name = "Project 3 - News", Status = true, Deleted = false,  NewsCategoryId = null,  ListTagId = "", 
                                CreatedById = userId, CreatedDateTime = DateTime.Now,
                                ModifiedById = userId, ModifiedDateTime = DateTime.Now,
                                NewsMappings = new List<NewsMapping>()
                                {
                                    new NewsMapping()
                                    {
                                        NewsId = 3, LanguageId = 129, Status = true, Deleted = false,
                                        Intro = "Dự Án 3 - News - Intro", Detail = "Dự Án 3 - News - Detail", MetaData = "<meta name='author' content='thanhit.net'>",
                                        Title = "Dự Án 3 - News ", Keyword = "Dự án 3 - News Keyword", Description = "Dự án 3 - News Description",
                                        CreatedById = userId, CreatedDateTime = DateTime.Now,
                                        ModifiedById = userId, ModifiedDateTime = DateTime.Now
                                    },
                                    new NewsMapping()
                                    {
                                        NewsId = 3, LanguageId = 29, Status = true, Deleted = false,
                                        Intro = "Project 3 - News - Intro", Detail = "Project 3- News - Detail", MetaData = "<meta name='author' content='thanhit.net'>",
                                        Title = "Project 3 - News", Keyword = "Project 3 - News Keyword", Description = "Project 3 - News Description",
                                        CreatedById = userId, CreatedDateTime = DateTime.Now,
                                        ModifiedById = userId, ModifiedDateTime = DateTime.Now
                                    }
                                }
                            }
                        },
                        ProjectMappings = new List<ProjectMapping>()
                        {
                            new ProjectMapping()
                            {
                                ProjectId = 3, LanguageId = 129, Status = true, Deleted = false,
                                Title = "Tên Dự Án 3", Keyword = "Dự Án 3 Keyword", Description = "Dự án 3 Description", MetaData = "<meta name='author' content='thanhit.net'>",
                                Intro = "Dự Án 3 - Intro", Detail = "Dự Án 3 Detail",
                                CreatedById = userId, CreatedDateTime = DateTime.Now,
                                ModifiedById = userId, ModifiedDateTime = DateTime.Now
                            },
                            new ProjectMapping()
                            {
                                ProjectId = 3, LanguageId = 29, Status = true, Deleted = false,
                                Title = "Project Name 3", Keyword = "Project 3 Keyword", Description = "Project 3 Description", MetaData = "<meta name='author' content='thanhit.net'>",
                                Intro = "Project 3 - Intro", Detail = "Project 3 Detail",
                                CreatedById = userId, CreatedDateTime = DateTime.Now,
                                ModifiedById = userId, ModifiedDateTime = DateTime.Now
                            }
                        },
                        ProjectImages = new List<ProjectImage>()
                        {
                            new ProjectImage()
                            {
                                ProjectId = 3, Type = 1, Status = true, Deleted = false, ZOrder = 1,
                                ResourceUrl = "/Content/images/slide.jpg", Thumbnail = "/Content/images/page2_img1.jpg",
                                CreatedById = userId, CreatedDateTime = DateTime.Now,
                                ModifiedById = userId, ModifiedDateTime = DateTime.Now,
                                ProjectImageMappings = new List<ProjectImageMapping>()
                                {
                                    new ProjectImageMapping()
                                    {
                                        ProjectImageId = 7, LanguageId = 129, Status = true, Deleted = false,
                                        Name = "Tên ảnh 1-  Dự Án 3", Description = "Dự án 3 - ảnh 1 Description",
                                        CreatedById = userId, CreatedDateTime = DateTime.Now,
                                        ModifiedById = userId, ModifiedDateTime = DateTime.Now  
                                    },
                                    new ProjectImageMapping()
                                    {
                                        ProjectImageId = 7, LanguageId = 29, Status = true, Deleted = false,
                                        Name = "Project Name 3 - image 1", Description = "Project 3 - image 1 Description",
                                        CreatedById = userId, CreatedDateTime = DateTime.Now,
                                        ModifiedById = userId, ModifiedDateTime = DateTime.Now 
                                    }
                                }
                            },
                            new ProjectImage()
                            {
                                ProjectId = 3, Type = 1, Status = true, Deleted = false, ZOrder = 2,
                                ResourceUrl = "/Content/images/slide1.jpg", Thumbnail = "/Content/images/page2_img2.jpg",
                                CreatedById = userId, CreatedDateTime = DateTime.Now,
                                ModifiedById = userId, ModifiedDateTime = DateTime.Now,
                                ProjectImageMappings = new List<ProjectImageMapping>()
                                {
                                    new ProjectImageMapping()
                                    {
                                        ProjectImageId = 8, LanguageId = 129, Status = true, Deleted = false,
                                        Name = "Tên ảnh 2-  Dự Án 3", Description = "Dự án 3 - ảnh 2 Description",
                                        CreatedById = userId, CreatedDateTime = DateTime.Now,
                                        ModifiedById = userId, ModifiedDateTime = DateTime.Now  
                                    },
                                    new ProjectImageMapping()
                                    {
                                        ProjectImageId = 8, LanguageId = 29, Status = true, Deleted = false,
                                        Name = "Project Name 3 - image 2", Description = "Project 3 - image 2 Description",
                                        CreatedById = userId, CreatedDateTime = DateTime.Now,
                                        ModifiedById = userId, ModifiedDateTime = DateTime.Now 
                                    }
                                }
                            },
                            new ProjectImage()
                            {
                                ProjectId = 3, Type = 1, Status = true, Deleted = false, ZOrder = 3,
                                ResourceUrl = "/Content/images/slide2.jpg", Thumbnail = "/Content/images/page2_img3.jpg",
                                CreatedById = userId, CreatedDateTime = DateTime.Now,
                                ModifiedById = userId, ModifiedDateTime = DateTime.Now,
                                ProjectImageMappings = new List<ProjectImageMapping>()
                                {
                                    new ProjectImageMapping()
                                    {
                                        ProjectImageId = 9, LanguageId = 129, Status = true, Deleted = false,
                                        Name = "Tên ảnh 3-  Dự Án 3", Description = "Dự án 3 - ảnh 3 Description",
                                        CreatedById = userId, CreatedDateTime = DateTime.Now,
                                        ModifiedById = userId, ModifiedDateTime = DateTime.Now  
                                    },
                                    new ProjectImageMapping()
                                    {
                                        ProjectImageId = 9, LanguageId = 29, Status = true, Deleted = false,
                                        Name = "Project Name 3 - image 3", Description = "Project 3 - image 3 Description",
                                        CreatedById = userId, CreatedDateTime = DateTime.Now,
                                        ModifiedById = userId, ModifiedDateTime = DateTime.Now 
                                    }
                                }
                            }
                        }
                    },
                    // project 4
                    new Project() { 
                        Name = "Project 4", Type = 1, Status = true, Deleted = false, ZOrder = 4,
                        CreatedById = userId, CreatedDateTime = DateTime.Now,
                        ModifiedById = userId, ModifiedDateTime = DateTime.Now,
                        News = new List<News>
                        {
                            new News()
                            {
                                Name = "Project 4 - News", Status = true, Deleted = false,  NewsCategoryId = null,  ListTagId = "", 
                                CreatedById = userId, CreatedDateTime = DateTime.Now,
                                ModifiedById = userId, ModifiedDateTime = DateTime.Now,
                                NewsMappings = new List<NewsMapping>()
                                {
                                    new NewsMapping()
                                    {
                                        NewsId = 4, LanguageId = 129, Status = true, Deleted = false,
                                        Intro = "Dự Án 4 - News - Intro", Detail = "Dự Án 4 - News - Detail", MetaData = "<meta name='author' content='thanhit.net'>",
                                        Title = "Dự Án 4 - News ", Keyword = "Dự án 4 - News Keyword", Description = "Dự án 4 - News Description",
                                        CreatedById = userId, CreatedDateTime = DateTime.Now,
                                        ModifiedById = userId, ModifiedDateTime = DateTime.Now
                                    },
                                    new NewsMapping()
                                    {
                                        NewsId = 4, LanguageId = 29, Status = true, Deleted = false,
                                        Intro = "Project 4 - News - Intro", Detail = "Project 4- News - Detail", MetaData = "<meta name='author' content='thanhit.net'>",
                                        Title = "Project 4 - News", Keyword = "Project 4 - News Keyword", Description = "Project 4 - News Description",
                                        CreatedById = userId, CreatedDateTime = DateTime.Now,
                                        ModifiedById = userId, ModifiedDateTime = DateTime.Now
                                    }
                                }
                            }
                        },
                        ProjectMappings = new List<ProjectMapping>()
                        {
                            new ProjectMapping()
                            {
                                ProjectId = 4, LanguageId = 129, Status = true, Deleted = false,
                                Title = "Tên Dự Án 4", Keyword = "Dự Án 4 Keyword", Description = "Dự án 4 Description", MetaData = "<meta name='author' content='thanhit.net'>",
                                Intro = "Dự Án 4 - Intro", Detail = "Dự Án 4 Detail",
                                CreatedById = userId, CreatedDateTime = DateTime.Now,
                                ModifiedById = userId, ModifiedDateTime = DateTime.Now
                            },
                            new ProjectMapping()
                            {
                                ProjectId = 4, LanguageId = 29, Status = true, Deleted = false,
                                Title = "Project Name 4", Keyword = "Project 4 Keyword", Description = "Project 4 Description", MetaData = "<meta name='author' content='thanhit.net'>",
                                Intro = "Project 4 - Intro", Detail = "Project 4 Detail",
                                CreatedById = userId, CreatedDateTime = DateTime.Now,
                                ModifiedById = userId, ModifiedDateTime = DateTime.Now
                            }
                        },
                        ProjectImages = new List<ProjectImage>()
                        {
                            new ProjectImage()
                            {
                                ProjectId = 4, Type = 1, Status = true, Deleted = false, ZOrder = 1,
                                ResourceUrl = "/Content/images/slide.jpg", Thumbnail = "/Content/images/page2_img1.jpg",
                                CreatedById = userId, CreatedDateTime = DateTime.Now,
                                ModifiedById = userId, ModifiedDateTime = DateTime.Now,
                                ProjectImageMappings = new List<ProjectImageMapping>()
                                {
                                    new ProjectImageMapping()
                                    {
                                        ProjectImageId = 10, LanguageId = 129, Status = true, Deleted = false,
                                        Name = "Tên ảnh 1-  Dự Án 4", Description = "Dự án 4 - ảnh 1 Description",
                                        CreatedById = userId, CreatedDateTime = DateTime.Now,
                                        ModifiedById = userId, ModifiedDateTime = DateTime.Now  
                                    },
                                    new ProjectImageMapping()
                                    {
                                        ProjectImageId = 10, LanguageId = 29, Status = true, Deleted = false,
                                        Name = "Project Name 4 - image 1", Description = "Project 4 - image 1 Description",
                                        CreatedById = userId, CreatedDateTime = DateTime.Now,
                                        ModifiedById = userId, ModifiedDateTime = DateTime.Now 
                                    }
                                }
                            },
                            new ProjectImage()
                            {
                                ProjectId = 4, Type = 1, Status = true, Deleted = false, ZOrder = 2,
                                ResourceUrl = "/Content/images/slide1.jpg", Thumbnail = "/Content/images/page2_img2.jpg",
                                CreatedById = userId, CreatedDateTime = DateTime.Now,
                                ModifiedById = userId, ModifiedDateTime = DateTime.Now,
                                ProjectImageMappings = new List<ProjectImageMapping>()
                                {
                                    new ProjectImageMapping()
                                    {
                                        ProjectImageId = 11, LanguageId = 129, Status = true, Deleted = false,
                                        Name = "Tên ảnh 2-  Dự Án 4", Description = "Dự án 4 - ảnh 2 Description",
                                        CreatedById = userId, CreatedDateTime = DateTime.Now,
                                        ModifiedById = userId, ModifiedDateTime = DateTime.Now  
                                    },
                                    new ProjectImageMapping()
                                    {
                                        ProjectImageId = 11, LanguageId = 29, Status = true, Deleted = false,
                                        Name = "Project Name 4 - image 2", Description = "Project 4 - image 2 Description",
                                        CreatedById = userId, CreatedDateTime = DateTime.Now,
                                        ModifiedById = userId, ModifiedDateTime = DateTime.Now 
                                    }
                                }
                            },
                            new ProjectImage()
                            {
                                ProjectId = 4, Type = 1, Status = true, Deleted = false, ZOrder = 3,
                                ResourceUrl = "/Content/images/slide2.jpg", Thumbnail = "/Content/images/page2_img3.jpg",
                                CreatedById = userId, CreatedDateTime = DateTime.Now,
                                ModifiedById = userId, ModifiedDateTime = DateTime.Now,
                                ProjectImageMappings = new List<ProjectImageMapping>()
                                {
                                    new ProjectImageMapping()
                                    {
                                        ProjectImageId = 12, LanguageId = 129, Status = true, Deleted = false,
                                        Name = "Tên ảnh 3-  Dự Án 4", Description = "Dự án 4 - ảnh 3 Description",
                                        CreatedById = userId, CreatedDateTime = DateTime.Now,
                                        ModifiedById = userId, ModifiedDateTime = DateTime.Now  
                                    },
                                    new ProjectImageMapping()
                                    {
                                        ProjectImageId = 12, LanguageId = 29, Status = true, Deleted = false,
                                        Name = "Project Name 4 - image 3", Description = "Project 4 - image 3 Description",
                                        CreatedById = userId, CreatedDateTime = DateTime.Now,
                                        ModifiedById = userId, ModifiedDateTime = DateTime.Now 
                                    }
                                }
                            }
                        }
                    },
                    // project 5
                    new Project() { 
                        Name = "Project 5", Type = 1, Status = true, Deleted = false, ZOrder = 5,
                        CreatedById = userId, CreatedDateTime = DateTime.Now,
                        ModifiedById = userId, ModifiedDateTime = DateTime.Now,
                        News = new List<News>
                        {
                            new News()
                            {
                                Name = "Project 5 - News", Status = true, Deleted = false,  NewsCategoryId = null,  ListTagId = "", 
                                CreatedById = userId, CreatedDateTime = DateTime.Now,
                                ModifiedById = userId, ModifiedDateTime = DateTime.Now,
                                NewsMappings = new List<NewsMapping>()
                                {
                                    new NewsMapping()
                                    {
                                        NewsId = 5, LanguageId = 129, Status = true, Deleted = false,
                                        Intro = "Dự Án 5 - News - Intro", Detail = "Dự Án 5 - News - Detail", MetaData = "<meta name='author' content='thanhit.net'>",
                                        Title = "Dự Án 5 - News ", Keyword = "Dự án 5 - News Keyword", Description = "Dự án 5 - News Description",
                                        CreatedById = userId, CreatedDateTime = DateTime.Now,
                                        ModifiedById = userId, ModifiedDateTime = DateTime.Now
                                    },
                                    new NewsMapping()
                                    {
                                        NewsId = 5, LanguageId = 29, Status = true, Deleted = false,
                                        Intro = "Project 5 - News - Intro", Detail = "Project 5 - News - Detail", MetaData = "<meta name='author' content='thanhit.net'>",
                                        Title = "Project 5 - News", Keyword = "Project 5 - News Keyword", Description = "Project 5 - News Description",
                                        CreatedById = userId, CreatedDateTime = DateTime.Now,
                                        ModifiedById = userId, ModifiedDateTime = DateTime.Now
                                    }
                                }
                            }
                        },
                        ProjectMappings = new List<ProjectMapping>()
                        {
                            new ProjectMapping()
                            {
                                ProjectId = 5, LanguageId = 129, Status = true, Deleted = false,
                                Title = "Tên Dự Án 5", Keyword = "Dự Án 5 Keyword", Description = "Dự án 5 Description", MetaData = "<meta name='author' content='thanhit.net'>",
                                Intro = "Dự Án 5 - Intro", Detail = "Dự Án 5 Detail",
                                CreatedById = userId, CreatedDateTime = DateTime.Now,
                                ModifiedById = userId, ModifiedDateTime = DateTime.Now
                            },
                            new ProjectMapping()
                            {
                                ProjectId = 5, LanguageId = 29, Status = true, Deleted = false,
                                Title = "Project Name 5", Keyword = "Project 5 Keyword", Description = "Project 5 Description", MetaData = "<meta name='author' content='thanhit.net'>",
                                Intro = "Project 5 - Intro", Detail = "Project 5 Detail",
                                CreatedById = userId, CreatedDateTime = DateTime.Now,
                                ModifiedById = userId, ModifiedDateTime = DateTime.Now
                            }
                        },
                        ProjectImages = new List<ProjectImage>()
                        {
                            new ProjectImage()
                            {
                                ProjectId = 5, Type = 1, Status = true, Deleted = false, ZOrder = 1,
                                ResourceUrl = "/Content/images/slide.jpg", Thumbnail = "/Content/images/page2_img1.jpg",
                                CreatedById = userId, CreatedDateTime = DateTime.Now,
                                ModifiedById = userId, ModifiedDateTime = DateTime.Now,
                                ProjectImageMappings = new List<ProjectImageMapping>()
                                {
                                    new ProjectImageMapping()
                                    {
                                        ProjectImageId = 13, LanguageId = 129, Status = true, Deleted = false,
                                        Name = "Tên ảnh 1-  Dự Án 5", Description = "Dự án 5 - ảnh 1 Description",
                                        CreatedById = userId, CreatedDateTime = DateTime.Now,
                                        ModifiedById = userId, ModifiedDateTime = DateTime.Now  
                                    },
                                    new ProjectImageMapping()
                                    {
                                        ProjectImageId = 13, LanguageId = 29, Status = true, Deleted = false,
                                        Name = "Project Name 5 - image 1", Description = "Project 5 - image 1 Description",
                                        CreatedById = userId, CreatedDateTime = DateTime.Now,
                                        ModifiedById = userId, ModifiedDateTime = DateTime.Now 
                                    }
                                }
                            },
                            new ProjectImage()
                            {
                                ProjectId = 5, Type = 1, Status = true, Deleted = false, ZOrder = 2,
                                ResourceUrl = "/Content/images/slide1.jpg", Thumbnail = "/Content/images/page2_img2.jpg",
                                CreatedById = userId, CreatedDateTime = DateTime.Now,
                                ModifiedById = userId, ModifiedDateTime = DateTime.Now,
                                ProjectImageMappings = new List<ProjectImageMapping>()
                                {
                                    new ProjectImageMapping()
                                    {
                                        ProjectImageId = 14, LanguageId = 129, Status = true, Deleted = false,
                                        Name = "Tên ảnh 2-  Dự Án 5", Description = "Dự án 5 - ảnh 2 Description",
                                        CreatedById = userId, CreatedDateTime = DateTime.Now,
                                        ModifiedById = userId, ModifiedDateTime = DateTime.Now  
                                    },
                                    new ProjectImageMapping()
                                    {
                                        ProjectImageId = 14, LanguageId = 29, Status = true, Deleted = false,
                                        Name = "Project Name 5 - image 2", Description = "Project 5 - image 2 Description",
                                        CreatedById = userId, CreatedDateTime = DateTime.Now,
                                        ModifiedById = userId, ModifiedDateTime = DateTime.Now 
                                    }
                                }
                            },
                            new ProjectImage()
                            {
                                ProjectId = 5, Type = 1, Status = true, Deleted = false, ZOrder = 3,
                                ResourceUrl = "/Content/images/slide2.jpg", Thumbnail = "/Content/images/page2_img3.jpg",
                                CreatedById = userId, CreatedDateTime = DateTime.Now,
                                ModifiedById = userId, ModifiedDateTime = DateTime.Now,
                                ProjectImageMappings = new List<ProjectImageMapping>()
                                {
                                    new ProjectImageMapping()
                                    {
                                        ProjectImageId = 15, LanguageId = 129, Status = true, Deleted = false,
                                        Name = "Tên ảnh 3-  Dự Án 5", Description = "Dự án 5 - ảnh 3 Description",
                                        CreatedById = userId, CreatedDateTime = DateTime.Now,
                                        ModifiedById = userId, ModifiedDateTime = DateTime.Now  
                                    },
                                    new ProjectImageMapping()
                                    {
                                        ProjectImageId = 15, LanguageId = 29, Status = true, Deleted = false,
                                        Name = "Project Name 5 - image 3", Description = "Project 5 - image 3 Description",
                                        CreatedById = userId, CreatedDateTime = DateTime.Now,
                                        ModifiedById = userId, ModifiedDateTime = DateTime.Now 
                                    }
                                }
                            }
                        }
                    },
                    // project 6
                    new Project() { 
                        Name = "Project 6", Type = 1, Status = true, Deleted = false, ZOrder = 6,
                        CreatedById = userId, CreatedDateTime = DateTime.Now,
                        ModifiedById = userId, ModifiedDateTime = DateTime.Now,
                        News = new List<News>
                        {
                            new News()
                            {
                                Name = "Project 6 - News", Status = true, Deleted = false,  NewsCategoryId = null,  ListTagId = "", 
                                CreatedById = userId, CreatedDateTime = DateTime.Now,
                                ModifiedById = userId, ModifiedDateTime = DateTime.Now,
                                NewsMappings = new List<NewsMapping>()
                                {
                                    new NewsMapping()
                                    {
                                        NewsId = 6, LanguageId = 129, Status = true, Deleted = false,
                                        Intro = "Dự Án 6 - News - Intro", Detail = "Dự Án 6 - News - Detail", MetaData = "<meta name='author' content='thanhit.net'>",
                                        Title = "Dự Án 6 - News ", Keyword = "Dự án 6 - News Keyword", Description = "Dự án 6 - News Description",
                                        CreatedById = userId, CreatedDateTime = DateTime.Now,
                                        ModifiedById = userId, ModifiedDateTime = DateTime.Now
                                    },
                                    new NewsMapping()
                                    {
                                        NewsId = 6, LanguageId = 29, Status = true, Deleted = false,
                                        Intro = "Project 6 - News - Intro", Detail = "Project 6- News - Detail", MetaData = "<meta name='author' content='thanhit.net'>",
                                        Title = "Project 6 - News", Keyword = "Project 6 - News Keyword", Description = "Project 6 - News Description",
                                        CreatedById = userId, CreatedDateTime = DateTime.Now,
                                        ModifiedById = userId, ModifiedDateTime = DateTime.Now
                                    }
                                }
                            }
                        },
                        ProjectMappings = new List<ProjectMapping>()
                        {
                            new ProjectMapping()
                            {
                                ProjectId = 6, LanguageId = 129, Status = true, Deleted = false,
                                Title = "Tên Dự Án 6", Keyword = "Dự Án 6 Keyword", Description = "Dự án 6 Description", MetaData = "<meta name='author' content='thanhit.net'>",
                                Intro = "Dự Án 6 - Intro", Detail = "Dự Án 6 Detail",
                                CreatedById = userId, CreatedDateTime = DateTime.Now,
                                ModifiedById = userId, ModifiedDateTime = DateTime.Now
                            },
                            new ProjectMapping()
                            {
                                ProjectId = 6, LanguageId = 29, Status = true, Deleted = false,
                                Title = "Project Name 6", Keyword = "Project 6 Keyword", Description = "Project 6 Description", MetaData = "<meta name='author' content='thanhit.net'>",
                                Intro = "Project 6 - Intro", Detail = "Project 6 Detail",
                                CreatedById = userId, CreatedDateTime = DateTime.Now,
                                ModifiedById = userId, ModifiedDateTime = DateTime.Now
                            }
                        },
                        ProjectImages = new List<ProjectImage>()
                        {
                            new ProjectImage()
                            {
                                ProjectId = 6, Type = 1, Status = true, Deleted = false, ZOrder = 1,
                                ResourceUrl = "/Content/images/slide.jpg", Thumbnail = "/Content/images/page2_img1.jpg",
                                CreatedById = userId, CreatedDateTime = DateTime.Now,
                                ModifiedById = userId, ModifiedDateTime = DateTime.Now,
                                ProjectImageMappings = new List<ProjectImageMapping>()
                                {
                                    new ProjectImageMapping()
                                    {
                                        ProjectImageId = 16, LanguageId = 129, Status = true, Deleted = false,
                                        Name = "Tên ảnh 1-  Dự Án 6", Description = "Dự án 6 - ảnh 1 Description",
                                        CreatedById = userId, CreatedDateTime = DateTime.Now,
                                        ModifiedById = userId, ModifiedDateTime = DateTime.Now  
                                    },
                                    new ProjectImageMapping()
                                    {
                                        ProjectImageId = 16, LanguageId = 29, Status = true, Deleted = false,
                                        Name = "Project Name 6 - image 1", Description = "Project 6 - image 1 Description",
                                        CreatedById = userId, CreatedDateTime = DateTime.Now,
                                        ModifiedById = userId, ModifiedDateTime = DateTime.Now 
                                    }
                                }
                            },
                            new ProjectImage()
                            {
                                ProjectId = 6, Type = 1, Status = true, Deleted = false, ZOrder = 2,
                                ResourceUrl = "/Content/images/slide1.jpg", Thumbnail = "/Content/images/page2_img2.jpg",
                                CreatedById = userId, CreatedDateTime = DateTime.Now,
                                ModifiedById = userId, ModifiedDateTime = DateTime.Now,
                                ProjectImageMappings = new List<ProjectImageMapping>()
                                {
                                    new ProjectImageMapping()
                                    {
                                        ProjectImageId = 17, LanguageId = 129, Status = true, Deleted = false,
                                        Name = "Tên ảnh 2-  Dự Án 6", Description = "Dự án 6 - ảnh 2 Description",
                                        CreatedById = userId, CreatedDateTime = DateTime.Now,
                                        ModifiedById = userId, ModifiedDateTime = DateTime.Now  
                                    },
                                    new ProjectImageMapping()
                                    {
                                        ProjectImageId = 17, LanguageId = 29, Status = true, Deleted = false,
                                        Name = "Project Name 6 - image 2", Description = "Project 6 - image 2 Description",
                                        CreatedById = userId, CreatedDateTime = DateTime.Now,
                                        ModifiedById = userId, ModifiedDateTime = DateTime.Now 
                                    }
                                }
                            },
                            new ProjectImage()
                            {
                                ProjectId = 6, Type = 1, Status = true, Deleted = false, ZOrder = 3,
                                ResourceUrl = "/Content/images/slide2.jpg", Thumbnail = "/Content/images/page2_img3.jpg",
                                CreatedById = userId, CreatedDateTime = DateTime.Now,
                                ModifiedById = userId, ModifiedDateTime = DateTime.Now,
                                ProjectImageMappings = new List<ProjectImageMapping>()
                                {
                                    new ProjectImageMapping()
                                    {
                                        ProjectImageId = 18, LanguageId = 129, Status = true, Deleted = false,
                                        Name = "Tên ảnh 3-  Dự Án 6", Description = "Dự án 6 - ảnh 3 Description",
                                        CreatedById = userId, CreatedDateTime = DateTime.Now,
                                        ModifiedById = userId, ModifiedDateTime = DateTime.Now  
                                    },
                                    new ProjectImageMapping()
                                    {
                                        ProjectImageId = 18, LanguageId = 29, Status = true, Deleted = false,
                                        Name = "Project Name 6 - image 3", Description = "Project 6 - image 3 Description",
                                        CreatedById = userId, CreatedDateTime = DateTime.Now,
                                        ModifiedById = userId, ModifiedDateTime = DateTime.Now 
                                    }
                                }
                            }
                        }
                    },
                    // project 7
                    new Project() { 
                        Name = "Project 7", Type = 1, Status = true, Deleted = false, ZOrder = 7,
                        CreatedById = userId, CreatedDateTime = DateTime.Now,
                        ModifiedById = userId, ModifiedDateTime = DateTime.Now,
                        News = new List<News>
                        {
                            new News()
                            {
                                Name = "Project 7 - News", Status = true, Deleted = false,  NewsCategoryId = null,  ListTagId = "", 
                                CreatedById = userId, CreatedDateTime = DateTime.Now,
                                ModifiedById = userId, ModifiedDateTime = DateTime.Now,
                                NewsMappings = new List<NewsMapping>()
                                {
                                    new NewsMapping()
                                    {
                                        NewsId = 7, LanguageId = 129, Status = true, Deleted = false,
                                        Intro = "Dự án 7 - News - Intro", Detail = "Dự án 7 - News - Detail", MetaData = "<meta name='author' content='thanhit.net'>",
                                        Title = "Dự án 7 - News ", Keyword = "Dự án 7 - News Keyword", Description = "Dự án 7 - News Description",
                                        CreatedById = userId, CreatedDateTime = DateTime.Now,
                                        ModifiedById = userId, ModifiedDateTime = DateTime.Now
                                    },
                                    new NewsMapping()
                                    {
                                        NewsId = 7, LanguageId = 29, Status = true, Deleted = false,
                                        Intro = "Project 7 - News - Intro", Detail = "Project 7- News - Detail", MetaData = "<meta name='author' content='thanhit.net'>",
                                        Title = "Project 7 - News", Keyword = "Project 7 - News Keyword", Description = "Project 7 - News Description",
                                        CreatedById = userId, CreatedDateTime = DateTime.Now,
                                        ModifiedById = userId, ModifiedDateTime = DateTime.Now
                                    }
                                }
                            }
                        },
                        ProjectMappings = new List<ProjectMapping>()
                        {
                            new ProjectMapping()
                            {
                                ProjectId = 7, LanguageId = 129, Status = true, Deleted = false,
                                Title = "Tên Dự Án 7", Keyword = "Dự Án 7 Keyword", Description = "Dự án 7 Description", MetaData = "<meta name='author' content='thanhit.net'>",
                                Intro = "Dự Án 7 - Intro", Detail = "Dự Án 7 Detail",
                                CreatedById = userId, CreatedDateTime = DateTime.Now,
                                ModifiedById = userId, ModifiedDateTime = DateTime.Now
                            },
                            new ProjectMapping()
                            {
                                ProjectId = 7, LanguageId = 29, Status = true, Deleted = false,
                                Title = "Project Name 7", Keyword = "Project 7 Keyword", Description = "Project 7 Description", MetaData = "<meta name='author' content='thanhit.net'>",
                                Intro = "Project 7 - Intro", Detail = "Project 7 Detail",
                                CreatedById = userId, CreatedDateTime = DateTime.Now,
                                ModifiedById = userId, ModifiedDateTime = DateTime.Now
                            }
                        },
                        ProjectImages = new List<ProjectImage>()
                        {
                            new ProjectImage()
                            {
                                ProjectId = 7, Type = 1, Status = true, Deleted = false, ZOrder = 1,
                                ResourceUrl = "/Content/images/slide.jpg", Thumbnail = "/Content/images/page2_img1.jpg",
                                CreatedById = userId, CreatedDateTime = DateTime.Now,
                                ModifiedById = userId, ModifiedDateTime = DateTime.Now,
                                ProjectImageMappings = new List<ProjectImageMapping>()
                                {
                                    new ProjectImageMapping()
                                    {
                                        ProjectImageId = 19, LanguageId = 129, Status = true, Deleted = false,
                                        Name = "Tên ảnh 1-  Dự Án 7", Description = "Dự án 7 - ảnh 1 Description",
                                        CreatedById = userId, CreatedDateTime = DateTime.Now,
                                        ModifiedById = userId, ModifiedDateTime = DateTime.Now  
                                    },
                                    new ProjectImageMapping()
                                    {
                                        ProjectImageId = 19, LanguageId = 29, Status = true, Deleted = false,
                                        Name = "Project Name 7 - image 1", Description = "Project 7 - image 1 Description",
                                        CreatedById = userId, CreatedDateTime = DateTime.Now,
                                        ModifiedById = userId, ModifiedDateTime = DateTime.Now 
                                    }
                                }
                            },
                            new ProjectImage()
                            {
                                ProjectId = 7, Type = 1, Status = true, Deleted = false, ZOrder = 2,
                                ResourceUrl = "/Content/images/slide1.jpg", Thumbnail = "/Content/images/page2_img2.jpg",
                                CreatedById = userId, CreatedDateTime = DateTime.Now,
                                ModifiedById = userId, ModifiedDateTime = DateTime.Now,
                                ProjectImageMappings = new List<ProjectImageMapping>()
                                {
                                    new ProjectImageMapping()
                                    {
                                        ProjectImageId = 20, LanguageId = 129, Status = true, Deleted = false,
                                        Name = "Tên ảnh 2-  Dự Án 7", Description = "Dự án 7 - ảnh 2 Description",
                                        CreatedById = userId, CreatedDateTime = DateTime.Now,
                                        ModifiedById = userId, ModifiedDateTime = DateTime.Now  
                                    },
                                    new ProjectImageMapping()
                                    {
                                        ProjectImageId = 20, LanguageId = 29, Status = true, Deleted = false,
                                        Name = "Project Name 7 - image 2", Description = "Project 7 - image 2 Description",
                                        CreatedById = userId, CreatedDateTime = DateTime.Now,
                                        ModifiedById = userId, ModifiedDateTime = DateTime.Now 
                                    }
                                }
                            },
                            new ProjectImage()
                            {
                                ProjectId = 7, Type = 1, Status = true, Deleted = false, ZOrder = 3,
                                ResourceUrl = "/Content/images/slide2.jpg", Thumbnail = "/Content/images/page2_img3.jpg",
                                CreatedById = userId, CreatedDateTime = DateTime.Now,
                                ModifiedById = userId, ModifiedDateTime = DateTime.Now,
                                ProjectImageMappings = new List<ProjectImageMapping>()
                                {
                                    new ProjectImageMapping()
                                    {
                                        ProjectImageId = 21, LanguageId = 129, Status = true, Deleted = false,
                                        Name = "Tên ảnh 3-  Dự Án 7", Description = "Dự án 7 - ảnh 3 Description",
                                        CreatedById = userId, CreatedDateTime = DateTime.Now,
                                        ModifiedById = userId, ModifiedDateTime = DateTime.Now  
                                    },
                                    new ProjectImageMapping()
                                    {
                                        ProjectImageId = 21, LanguageId = 29, Status = true, Deleted = false,
                                        Name = "Project Name 7 - image 3", Description = "Project 7 - image 3 Description",
                                        CreatedById = userId, CreatedDateTime = DateTime.Now,
                                        ModifiedById = userId, ModifiedDateTime = DateTime.Now 
                                    }
                                }
                            }
                        }
                    },
                    // project 8
                    new Project() { 
                        Name = "Project 8", Type = 1, Status = true, Deleted = false, ZOrder = 8,
                        CreatedById = userId, CreatedDateTime = DateTime.Now,
                        ModifiedById = userId, ModifiedDateTime = DateTime.Now,
                        News = new List<News>
                        {
                            new News()
                            {
                                Name = "Project 8 - News", Status = true, Deleted = false,  NewsCategoryId = null,  ListTagId = "", 
                                CreatedById = userId, CreatedDateTime = DateTime.Now,
                                ModifiedById = userId, ModifiedDateTime = DateTime.Now,
                                NewsMappings = new List<NewsMapping>()
                                {
                                    new NewsMapping()
                                    {
                                        NewsId = 8, LanguageId = 129, Status = true, Deleted = false,
                                        Intro = "Dự Án 8 - News - Intro", Detail = "Dự Án 8 - News - Detail", MetaData = "<meta name='author' content='thanhit.net'>",
                                        Title = "Dự Án 8 - News ", Keyword = "Dự Án 8 - News Keyword", Description = "Dự Án 8 - News Description",
                                        CreatedById = userId, CreatedDateTime = DateTime.Now,
                                        ModifiedById = userId, ModifiedDateTime = DateTime.Now
                                    },
                                    new NewsMapping()
                                    {
                                        NewsId = 8, LanguageId = 29, Status = true, Deleted = false,
                                        Intro = "Project 8 - News - Intro", Detail = "Project 8- News - Detail", MetaData = "<meta name='author' content='thanhit.net'>",
                                        Title = "Project 8 - News", Keyword = "Project 8 - News Keyword", Description = "Project 8 - News Description",
                                        CreatedById = userId, CreatedDateTime = DateTime.Now,
                                        ModifiedById = userId, ModifiedDateTime = DateTime.Now
                                    }
                                }
                            }
                        },
                        ProjectMappings = new List<ProjectMapping>()
                        {
                            new ProjectMapping()
                            {
                                ProjectId = 8, LanguageId = 129, Status = true, Deleted = false,
                                Title = "Tên Dự Án 8", Keyword = "Dự Án 8 Keyword", Description = "Dự án 8 Description", MetaData = "<meta name='author' content='thanhit.net'>",
                                Intro = "Dự Án 8 - Intro", Detail = "Dự Án 8 Detail",
                                CreatedById = userId, CreatedDateTime = DateTime.Now,
                                ModifiedById = userId, ModifiedDateTime = DateTime.Now
                            },
                            new ProjectMapping()
                            {
                                ProjectId = 8, LanguageId = 29, Status = true, Deleted = false,
                                Title = "Project Name 8", Keyword = "Project 8 Keyword", Description = "Project 8 Description", MetaData = "<meta name='author' content='thanhit.net'>",
                                Intro = "Project 8 - Intro", Detail = "Project 8 Detail",
                                CreatedById = userId, CreatedDateTime = DateTime.Now,
                                ModifiedById = userId, ModifiedDateTime = DateTime.Now
                            }
                        },
                        ProjectImages = new List<ProjectImage>()
                        {
                            new ProjectImage()
                            {
                                ProjectId = 8, Type = 1, Status = true, Deleted = false, ZOrder = 1,
                                ResourceUrl = "/Content/images/slide.jpg", Thumbnail = "/Content/images/page2_img1.jpg",
                                CreatedById = userId, CreatedDateTime = DateTime.Now,
                                ModifiedById = userId, ModifiedDateTime = DateTime.Now,
                                ProjectImageMappings = new List<ProjectImageMapping>()
                                {
                                    new ProjectImageMapping()
                                    {
                                        ProjectImageId = 22, LanguageId = 129, Status = true, Deleted = false,
                                        Name = "Tên ảnh 1-  Dự Án 8", Description = "Dự Án 8 - ảnh 1 Description",
                                        CreatedById = userId, CreatedDateTime = DateTime.Now,
                                        ModifiedById = userId, ModifiedDateTime = DateTime.Now  
                                    },
                                    new ProjectImageMapping()
                                    {
                                        ProjectImageId = 22, LanguageId = 29, Status = true, Deleted = false,
                                        Name = "Project Name 8 - image 1", Description = "Project 8 - image 1 Description",
                                        CreatedById = userId, CreatedDateTime = DateTime.Now,
                                        ModifiedById = userId, ModifiedDateTime = DateTime.Now 
                                    }
                                }
                            },
                            new ProjectImage()
                            {
                                ProjectId = 8, Type = 1, Status = true, Deleted = false, ZOrder = 2,
                                ResourceUrl = "/Content/images/slide1.jpg", Thumbnail = "/Content/images/page2_img2.jpg",
                                CreatedById = userId, CreatedDateTime = DateTime.Now,
                                ModifiedById = userId, ModifiedDateTime = DateTime.Now,
                                ProjectImageMappings = new List<ProjectImageMapping>()
                                {
                                    new ProjectImageMapping()
                                    {
                                        ProjectImageId = 23, LanguageId = 129, Status = true, Deleted = false,
                                        Name = "Tên ảnh 2-  Dự Án 8", Description = "Dự Án 8 - ảnh 2 Description",
                                        CreatedById = userId, CreatedDateTime = DateTime.Now,
                                        ModifiedById = userId, ModifiedDateTime = DateTime.Now  
                                    },
                                    new ProjectImageMapping()
                                    {
                                        ProjectImageId = 23, LanguageId = 29, Status = true, Deleted = false,
                                        Name = "Project Name 8 - image 2", Description = "Project 8 - image 2 Description",
                                        CreatedById = userId, CreatedDateTime = DateTime.Now,
                                        ModifiedById = userId, ModifiedDateTime = DateTime.Now 
                                    }
                                }
                            },
                            new ProjectImage()
                            {
                                ProjectId = 8, Type = 1, Status = true, Deleted = false, ZOrder = 3,
                                ResourceUrl = "/Content/images/slide2.jpg", Thumbnail = "/Content/images/page2_img3.jpg",
                                CreatedById = userId, CreatedDateTime = DateTime.Now,
                                ModifiedById = userId, ModifiedDateTime = DateTime.Now,
                                ProjectImageMappings = new List<ProjectImageMapping>()
                                {
                                    new ProjectImageMapping()
                                    {
                                        ProjectImageId = 24, LanguageId = 129, Status = true, Deleted = false,
                                        Name = "Tên ảnh 3-  Dự Án 8", Description = "Dự Án 8 - ảnh 3 Description",
                                        CreatedById = userId, CreatedDateTime = DateTime.Now,
                                        ModifiedById = userId, ModifiedDateTime = DateTime.Now  
                                    },
                                    new ProjectImageMapping()
                                    {
                                        ProjectImageId = 24, LanguageId = 29, Status = true, Deleted = false,
                                        Name = "Project Name 8 - image 3", Description = "Project 8 - image 3 Description",
                                        CreatedById = userId, CreatedDateTime = DateTime.Now,
                                        ModifiedById = userId, ModifiedDateTime = DateTime.Now 
                                    }
                                }
                            }
                        }
                    },
                    // project 9
                    new Project() { 
                        Name = "Project 9", Type = 1, Status = true, Deleted = false, ZOrder = 9,
                        CreatedById = userId, CreatedDateTime = DateTime.Now,
                        ModifiedById = userId, ModifiedDateTime = DateTime.Now,
                        News = new List<News>
                        {
                            new News()
                            {
                                Name = "Project 9 - News", Status = true, Deleted = false,  NewsCategoryId = null,  ListTagId = "", 
                                CreatedById = userId, CreatedDateTime = DateTime.Now,
                                ModifiedById = userId, ModifiedDateTime = DateTime.Now,
                                NewsMappings = new List<NewsMapping>()
                                {
                                    new NewsMapping()
                                    {
                                        NewsId = 9, LanguageId = 129, Status = true, Deleted = false,
                                        Intro = "Dự Án 9 - News - Intro", Detail = "Dự Án 9 - News - Detail", MetaData = "<meta name='author' content='thanhit.net'>",
                                        Title = "Dự Án 9 - News ", Keyword = "Dự Án 9 - News Keyword", Description = "Dự Án 9 - News Description",
                                        CreatedById = userId, CreatedDateTime = DateTime.Now,
                                        ModifiedById = userId, ModifiedDateTime = DateTime.Now
                                    },
                                    new NewsMapping()
                                    {
                                        NewsId = 9, LanguageId = 29, Status = true, Deleted = false,
                                        Intro = "Project 9 - News - Intro", Detail = "Project 9- News - Detail", MetaData = "<meta name='author' content='thanhit.net'>",
                                        Title = "Project 9 - News", Keyword = "Project 9 - News Keyword", Description = "Project 9 - News Description",
                                        CreatedById = userId, CreatedDateTime = DateTime.Now,
                                        ModifiedById = userId, ModifiedDateTime = DateTime.Now
                                    }
                                }
                            }
                        },
                        ProjectMappings = new List<ProjectMapping>()
                        {
                            new ProjectMapping()
                            {
                                ProjectId = 9, LanguageId = 129, Status = true, Deleted = false,
                                Title = "Tên Dự Án 9", Keyword = "Dự Án 9 Keyword", Description = "Dự án 9 Description", MetaData = "<meta name='author' content='thanhit.net'>",
                                Intro = "Dự Án 9 - Intro", Detail = "Dự Án 9 Detail",
                                CreatedById = userId, CreatedDateTime = DateTime.Now,
                                ModifiedById = userId, ModifiedDateTime = DateTime.Now
                            },
                            new ProjectMapping()
                            {
                                ProjectId = 9, LanguageId = 29, Status = true, Deleted = false,
                                Title = "Project Name 9", Keyword = "Project 9 Keyword", Description = "Project 9 Description", MetaData = "<meta name='author' content='thanhit.net'>",
                                Intro = "Project 9 - Intro", Detail = "Project 9 Detail",
                                CreatedById = userId, CreatedDateTime = DateTime.Now,
                                ModifiedById = userId, ModifiedDateTime = DateTime.Now
                            }
                        },
                        ProjectImages = new List<ProjectImage>()
                        {
                            new ProjectImage()
                            {
                                ProjectId = 9, Type = 1, Status = true, Deleted = false, ZOrder = 1,
                                ResourceUrl = "/Content/images/slide.jpg", Thumbnail = "/Content/images/page2_img1.jpg",
                                CreatedById = userId, CreatedDateTime = DateTime.Now,
                                ModifiedById = userId, ModifiedDateTime = DateTime.Now,
                                ProjectImageMappings = new List<ProjectImageMapping>()
                                {
                                    new ProjectImageMapping()
                                    {
                                        ProjectImageId = 25, LanguageId = 129, Status = true, Deleted = false,
                                        Name = "Tên ảnh 1-  Dự Án 9", Description = "Dự Án 9 - ảnh 1 Description",
                                        CreatedById = userId, CreatedDateTime = DateTime.Now,
                                        ModifiedById = userId, ModifiedDateTime = DateTime.Now  
                                    },
                                    new ProjectImageMapping()
                                    {
                                        ProjectImageId = 25, LanguageId = 29, Status = true, Deleted = false,
                                        Name = "Project Name 9 - image 1", Description = "Project 9 - image 1 Description",
                                        CreatedById = userId, CreatedDateTime = DateTime.Now,
                                        ModifiedById = userId, ModifiedDateTime = DateTime.Now 
                                    }
                                }
                            },
                            new ProjectImage()
                            {
                                ProjectId = 9, Type = 1, Status = true, Deleted = false, ZOrder = 2,
                                ResourceUrl = "/Content/images/slide1.jpg", Thumbnail = "/Content/images/page2_img2.jpg",
                                CreatedById = userId, CreatedDateTime = DateTime.Now,
                                ModifiedById = userId, ModifiedDateTime = DateTime.Now,
                                ProjectImageMappings = new List<ProjectImageMapping>()
                                {
                                    new ProjectImageMapping()
                                    {
                                        ProjectImageId = 26, LanguageId = 129, Status = true, Deleted = false,
                                        Name = "Tên ảnh 2-  Dự Án 9", Description = "Dự Án 9 - ảnh 2 Description",
                                        CreatedById = userId, CreatedDateTime = DateTime.Now,
                                        ModifiedById = userId, ModifiedDateTime = DateTime.Now  
                                    },
                                    new ProjectImageMapping()
                                    {
                                        ProjectImageId = 26, LanguageId = 29, Status = true, Deleted = false,
                                        Name = "Project Name 9 - image 2", Description = "Project 9 - image 2 Description",
                                        CreatedById = userId, CreatedDateTime = DateTime.Now,
                                        ModifiedById = userId, ModifiedDateTime = DateTime.Now 
                                    }
                                }
                            },
                            new ProjectImage()
                            {
                                ProjectId = 9, Type = 1, Status = true, Deleted = false, ZOrder = 3,
                                ResourceUrl = "/Content/images/slide2.jpg", Thumbnail = "/Content/images/page2_img3.jpg",
                                CreatedById = userId, CreatedDateTime = DateTime.Now,
                                ModifiedById = userId, ModifiedDateTime = DateTime.Now,
                                ProjectImageMappings = new List<ProjectImageMapping>()
                                {
                                    new ProjectImageMapping()
                                    {
                                        ProjectImageId = 27, LanguageId = 129, Status = true, Deleted = false,
                                        Name = "Tên ảnh 3-  Dự Án 9", Description = "Dự Án 9 - ảnh 3 Description",
                                        CreatedById = userId, CreatedDateTime = DateTime.Now,
                                        ModifiedById = userId, ModifiedDateTime = DateTime.Now  
                                    },
                                    new ProjectImageMapping()
                                    {
                                        ProjectImageId = 27, LanguageId = 29, Status = true, Deleted = false,
                                        Name = "Project Name 9 - image 3", Description = "Project 9 - image 3 Description",
                                        CreatedById = userId, CreatedDateTime = DateTime.Now,
                                        ModifiedById = userId, ModifiedDateTime = DateTime.Now 
                                    }
                                }
                            }
                        }
                    },
                    // project 10
                    new Project() { 
                        Name = "Project 10", Type = 1, Status = true, Deleted = false, ZOrder = 10,
                        CreatedById = userId, CreatedDateTime = DateTime.Now,
                        ModifiedById = userId, ModifiedDateTime = DateTime.Now,
                        News = new List<News>
                        {
                            new News()
                            {
                                Name = "Project 10 - News", Status = true, Deleted = false,  NewsCategoryId = null,  ListTagId = "", 
                                CreatedById = userId, CreatedDateTime = DateTime.Now,
                                ModifiedById = userId, ModifiedDateTime = DateTime.Now,
                                NewsMappings = new List<NewsMapping>()
                                {
                                    new NewsMapping()
                                    {
                                        NewsId = 10, LanguageId = 129, Status = true, Deleted = false,
                                        Intro = "Dự Án 10 - News - Intro", Detail = "Dự Án 10 - News - Detail", MetaData = "<meta name='author' content='thanhit.net'>",
                                        Title = "Dự Án 10 - News ", Keyword = "Dự Án 10 - News Keyword", Description = "Dự Án 10 - News Description",
                                        CreatedById = userId, CreatedDateTime = DateTime.Now,
                                        ModifiedById = userId, ModifiedDateTime = DateTime.Now
                                    },
                                    new NewsMapping()
                                    {
                                        NewsId = 10, LanguageId = 29, Status = true, Deleted = false,
                                        Intro = "Project 10 - News - Intro", Detail = "Project 10- News - Detail", MetaData = "<meta name='author' content='thanhit.net'>",
                                        Title = "Project 10 - News", Keyword = "Project 10 - News Keyword", Description = "Project 10 - News Description",
                                        CreatedById = userId, CreatedDateTime = DateTime.Now,
                                        ModifiedById = userId, ModifiedDateTime = DateTime.Now
                                    }
                                }
                            }
                        },
                        ProjectMappings = new List<ProjectMapping>()
                        {
                            new ProjectMapping()
                            {
                                ProjectId = 10, LanguageId = 129, Status = true, Deleted = false,
                                Title = "Tên Dự Án 10", Keyword = "Dự Án 10 Keyword", Description = "Dự án 10 Description", MetaData = "<meta name='author' content='thanhit.net'>",
                                Intro = "Dự Án 10 - Intro", Detail = "Dự Án 10 Detail",
                                CreatedById = userId, CreatedDateTime = DateTime.Now,
                                ModifiedById = userId, ModifiedDateTime = DateTime.Now
                            },
                            new ProjectMapping()
                            {
                                ProjectId = 10, LanguageId = 29, Status = true, Deleted = false,
                                Title = "Project Name 10", Keyword = "Project 10 Keyword", Description = "Project 10 Description", MetaData = "<meta name='author' content='thanhit.net'>",
                                Intro = "Project 10 - Intro", Detail = "Project 10 Detail",
                                CreatedById = userId, CreatedDateTime = DateTime.Now,
                                ModifiedById = userId, ModifiedDateTime = DateTime.Now
                            }
                        },
                        ProjectImages = new List<ProjectImage>()
                        {
                            new ProjectImage()
                            {
                                ProjectId = 10, Type = 1, Status = true, Deleted = false, ZOrder = 1,
                                ResourceUrl = "/Content/images/slide.jpg", Thumbnail = "/Content/images/page2_img1.jpg",
                                CreatedById = userId, CreatedDateTime = DateTime.Now,
                                ModifiedById = userId, ModifiedDateTime = DateTime.Now,
                                ProjectImageMappings = new List<ProjectImageMapping>()
                                {
                                    new ProjectImageMapping()
                                    {
                                        ProjectImageId = 28, LanguageId = 129, Status = true, Deleted = false,
                                        Name = "Tên ảnh 1-  Dự Án 10", Description = "Dự Án 10 - ảnh 1 Description",
                                        CreatedById = userId, CreatedDateTime = DateTime.Now,
                                        ModifiedById = userId, ModifiedDateTime = DateTime.Now  
                                    },
                                    new ProjectImageMapping()
                                    {
                                        ProjectImageId = 28, LanguageId = 29, Status = true, Deleted = false,
                                        Name = "Project Name 10 - image 1", Description = "Project 10 - image 1 Description",
                                        CreatedById = userId, CreatedDateTime = DateTime.Now,
                                        ModifiedById = userId, ModifiedDateTime = DateTime.Now 
                                    }
                                }
                            },
                            new ProjectImage()
                            {
                                ProjectId = 10, Type = 1, Status = true, Deleted = false, ZOrder = 2,
                                ResourceUrl = "/Content/images/slide1.jpg", Thumbnail = "/Content/images/page2_img2.jpg",
                                CreatedById = userId, CreatedDateTime = DateTime.Now,
                                ModifiedById = userId, ModifiedDateTime = DateTime.Now,
                                ProjectImageMappings = new List<ProjectImageMapping>()
                                {
                                    new ProjectImageMapping()
                                    {
                                        ProjectImageId = 29, LanguageId = 129, Status = true, Deleted = false,
                                        Name = "Tên ảnh 2-  Dự Án 10", Description = "Dự Án 10 - ảnh 2 Description",
                                        CreatedById = userId, CreatedDateTime = DateTime.Now,
                                        ModifiedById = userId, ModifiedDateTime = DateTime.Now  
                                    },
                                    new ProjectImageMapping()
                                    {
                                        ProjectImageId = 29, LanguageId = 29, Status = true, Deleted = false,
                                        Name = "Project Name 10 - image 2", Description = "Project 10 - image 2 Description",
                                        CreatedById = userId, CreatedDateTime = DateTime.Now,
                                        ModifiedById = userId, ModifiedDateTime = DateTime.Now 
                                    }
                                }
                            },
                            new ProjectImage()
                            {
                                ProjectId = 10, Type = 1, Status = true, Deleted = false, ZOrder = 3,
                                ResourceUrl = "/Content/images/slide2.jpg", Thumbnail = "/Content/images/page2_img3.jpg",
                                CreatedById = userId, CreatedDateTime = DateTime.Now,
                                ModifiedById = userId, ModifiedDateTime = DateTime.Now,
                                ProjectImageMappings = new List<ProjectImageMapping>()
                                {
                                    new ProjectImageMapping()
                                    {
                                        ProjectImageId = 30, LanguageId = 129, Status = true, Deleted = false,
                                        Name = "Tên ảnh 3-  Dự Án 10", Description = "Dự Án 10 - ảnh 3 Description",
                                        CreatedById = userId, CreatedDateTime = DateTime.Now,
                                        ModifiedById = userId, ModifiedDateTime = DateTime.Now  
                                    },
                                    new ProjectImageMapping()
                                    {
                                        ProjectImageId = 30, LanguageId = 29, Status = true, Deleted = false,
                                        Name = "Project Name 10 - image 3", Description = "Project 10 - image 3 Description",
                                        CreatedById = userId, CreatedDateTime = DateTime.Now,
                                        ModifiedById = userId, ModifiedDateTime = DateTime.Now 
                                    }
                                }
                            }
                        }
                    },
                    // project 11
                    new Project() { 
                        Name = "Project 11", Type = 1, Status = true, Deleted = false, ZOrder = 11,
                        CreatedById = userId, CreatedDateTime = DateTime.Now,
                        ModifiedById = userId, ModifiedDateTime = DateTime.Now,
                        News = new List<News>
                        {
                            new News()
                            {
                                Name = "Project 11 - News", Status = true, Deleted = false,  NewsCategoryId = null,  ListTagId = "", 
                                CreatedById = userId, CreatedDateTime = DateTime.Now,
                                ModifiedById = userId, ModifiedDateTime = DateTime.Now,
                                NewsMappings = new List<NewsMapping>()
                                {
                                    new NewsMapping()
                                    {
                                        NewsId = 11, LanguageId = 129, Status = true, Deleted = false,
                                        Intro = "Dự Án 11 - News - Intro", Detail = "Dự Án 11 - News - Detail", MetaData = "<meta name='author' content='thanhit.net'>",
                                        Title = "Dự Án 11 - News ", Keyword = "Dự Án 11 - News Keyword", Description = "Dự Án 11 - News Description",
                                        CreatedById = userId, CreatedDateTime = DateTime.Now,
                                        ModifiedById = userId, ModifiedDateTime = DateTime.Now
                                    },
                                    new NewsMapping()
                                    {
                                        NewsId = 11, LanguageId = 29, Status = true, Deleted = false,
                                        Intro = "Project 11 - News - Intro", Detail = "Project 11- News - Detail", MetaData = "<meta name='author' content='thanhit.net'>",
                                        Title = "Project 11 - News", Keyword = "Project 11 - News Keyword", Description = "Project 11 - News Description",
                                        CreatedById = userId, CreatedDateTime = DateTime.Now,
                                        ModifiedById = userId, ModifiedDateTime = DateTime.Now
                                    }
                                }
                            }
                        },
                        ProjectMappings = new List<ProjectMapping>()
                        {
                            new ProjectMapping()
                            {
                                ProjectId = 11, LanguageId = 129, Status = true, Deleted = false,
                                Title = "Tên Dự Án 11", Keyword = "Dự Án 11 Keyword", Description = "Dự án 11 Description", MetaData = "<meta name='author' content='thanhit.net'>",
                                Intro = "Dự Án 11 - Intro", Detail = "Dự Án 11 Detail",
                                CreatedById = userId, CreatedDateTime = DateTime.Now,
                                ModifiedById = userId, ModifiedDateTime = DateTime.Now
                            },
                            new ProjectMapping()
                            {
                                ProjectId = 11, LanguageId = 29, Status = true, Deleted = false,
                                Title = "Project Name 11", Keyword = "Project 11 Keyword", Description = "Project 11 Description", MetaData = "<meta name='author' content='thanhit.net'>",
                                Intro = "Project 11 - Intro", Detail = "Project 11 Detail",
                                CreatedById = userId, CreatedDateTime = DateTime.Now,
                                ModifiedById = userId, ModifiedDateTime = DateTime.Now
                            }
                        },
                        ProjectImages = new List<ProjectImage>()
                        {
                            new ProjectImage()
                            {
                                ProjectId = 11, Type = 1, Status = true, Deleted = false, ZOrder = 1,
                                ResourceUrl = "/Content/images/slide.jpg", Thumbnail = "/Content/images/page2_img1.jpg",
                                CreatedById = userId, CreatedDateTime = DateTime.Now,
                                ModifiedById = userId, ModifiedDateTime = DateTime.Now,
                                ProjectImageMappings = new List<ProjectImageMapping>()
                                {
                                    new ProjectImageMapping()
                                    {
                                        ProjectImageId = 31, LanguageId = 129, Status = true, Deleted = false,
                                        Name = "Tên ảnh 1-  Dự Án 11", Description = "Dự Án 11 - ảnh 1 Description",
                                        CreatedById = userId, CreatedDateTime = DateTime.Now,
                                        ModifiedById = userId, ModifiedDateTime = DateTime.Now  
                                    },
                                    new ProjectImageMapping()
                                    {
                                        ProjectImageId = 31, LanguageId = 29, Status = true, Deleted = false,
                                        Name = "Project Name 11 - image 1", Description = "Project 11 - image 1 Description",
                                        CreatedById = userId, CreatedDateTime = DateTime.Now,
                                        ModifiedById = userId, ModifiedDateTime = DateTime.Now 
                                    }
                                }
                            },
                            new ProjectImage()
                            {
                                ProjectId = 11, Type = 1, Status = true, Deleted = false, ZOrder = 2,
                                ResourceUrl = "/Content/images/slide1.jpg", Thumbnail = "/Content/images/page2_img2.jpg",
                                CreatedById = userId, CreatedDateTime = DateTime.Now,
                                ModifiedById = userId, ModifiedDateTime = DateTime.Now,
                                ProjectImageMappings = new List<ProjectImageMapping>()
                                {
                                    new ProjectImageMapping()
                                    {
                                        ProjectImageId = 32, LanguageId = 129, Status = true, Deleted = false,
                                        Name = "Tên ảnh 2-  Dự Án 11", Description = "Dự Án 11 - ảnh 2 Description",
                                        CreatedById = userId, CreatedDateTime = DateTime.Now,
                                        ModifiedById = userId, ModifiedDateTime = DateTime.Now  
                                    },
                                    new ProjectImageMapping()
                                    {
                                        ProjectImageId = 32, LanguageId = 29, Status = true, Deleted = false,
                                        Name = "Project Name 11 - image 2", Description = "Project 11 - image 2 Description",
                                        CreatedById = userId, CreatedDateTime = DateTime.Now,
                                        ModifiedById = userId, ModifiedDateTime = DateTime.Now 
                                    }
                                }
                            },
                            new ProjectImage()
                            {
                                ProjectId = 11, Type = 1, Status = true, Deleted = false, ZOrder = 3,
                                ResourceUrl = "/Content/images/slide2.jpg", Thumbnail = "/Content/images/page2_img3.jpg",
                                CreatedById = userId, CreatedDateTime = DateTime.Now,
                                ModifiedById = userId, ModifiedDateTime = DateTime.Now,
                                ProjectImageMappings = new List<ProjectImageMapping>()
                                {
                                    new ProjectImageMapping()
                                    {
                                        ProjectImageId = 33, LanguageId = 129, Status = true, Deleted = false,
                                        Name = "Tên ảnh 3-  Dự Án 11", Description = "Dự Án 11 - ảnh 3 Description",
                                        CreatedById = userId, CreatedDateTime = DateTime.Now,
                                        ModifiedById = userId, ModifiedDateTime = DateTime.Now  
                                    },
                                    new ProjectImageMapping()
                                    {
                                        ProjectImageId = 33, LanguageId = 29, Status = true, Deleted = false,
                                        Name = "Project Name 11 - image 3", Description = "Project 11 - image 3 Description",
                                        CreatedById = userId, CreatedDateTime = DateTime.Now,
                                        ModifiedById = userId, ModifiedDateTime = DateTime.Now 
                                    }
                                }
                            }
                        }
                    },
                    // project 12
                    new Project() { 
                        Name = "Project 12", Type = 1, Status = true, Deleted = false, ZOrder = 12,
                        CreatedById = userId, CreatedDateTime = DateTime.Now,
                        ModifiedById = userId, ModifiedDateTime = DateTime.Now,
                        News = new List<News>
                        {
                            new News()
                            {
                                Name = "Project 12 - News", Status = true, Deleted = false,  NewsCategoryId = null,  ListTagId = "", 
                                CreatedById = userId, CreatedDateTime = DateTime.Now,
                                ModifiedById = userId, ModifiedDateTime = DateTime.Now,
                                NewsMappings = new List<NewsMapping>()
                                {
                                    new NewsMapping()
                                    {
                                        NewsId = 12, LanguageId = 129, Status = true, Deleted = false,
                                        Intro = "Dự Án 12 - News - Intro", Detail = "Dự Án 12 - News - Detail", MetaData = "Dự Án 12 - News - MetaData",
                                        Title = "Dự Án 12 - News ", Keyword = "Dự Án 12 - News Keyword", Description = "Dự Án 12 - News Description",
                                        CreatedById = userId, CreatedDateTime = DateTime.Now,
                                        ModifiedById = userId, ModifiedDateTime = DateTime.Now
                                    },
                                    new NewsMapping()
                                    {
                                        NewsId = 12, LanguageId = 29, Status = true, Deleted = false,
                                        Intro = "Project 12 - News - Intro", Detail = "Project 12- News - Detail", MetaData = "Project 12 - News - MetaData",
                                        Title = "Project 12 - News", Keyword = "Project 12 - News Keyword", Description = "Project 12 - News Description",
                                        CreatedById = userId, CreatedDateTime = DateTime.Now,
                                        ModifiedById = userId, ModifiedDateTime = DateTime.Now
                                    }
                                }
                            }
                        },
                        ProjectMappings = new List<ProjectMapping>()
                        {
                            new ProjectMapping()
                            {
                                ProjectId = 12, LanguageId = 129, Status = true, Deleted = false,
                                Title = "Tên Dự Án 12", Keyword = "Dự Án 12 Keyword", Description = "Dự án 12 Description", MetaData = "<meta name='author' content='thanhit.net'>",
                                Intro = "Dự Án 12 - Intro", Detail = "Dự Án 12 Detail",
                                CreatedById = userId, CreatedDateTime = DateTime.Now,
                                ModifiedById = userId, ModifiedDateTime = DateTime.Now
                            },
                            new ProjectMapping()
                            {
                                ProjectId = 12, LanguageId = 29, Status = true, Deleted = false,
                                Title = "Project Name 12", Keyword = "Project 12 Keyword", Description = "Project 12 Description", MetaData = "<meta name='author' content='thanhit.net'>",
                                Intro = "Project 12 - Intro", Detail = "Project 12 Detail",
                                CreatedById = userId, CreatedDateTime = DateTime.Now,
                                ModifiedById = userId, ModifiedDateTime = DateTime.Now
                            }
                        },
                        ProjectImages = new List<ProjectImage>()
                        {
                            new ProjectImage()
                            {
                                ProjectId = 12, Type = 1, Status = true, Deleted = false, ZOrder = 1,
                                ResourceUrl = "/Content/images/slide.jpg", Thumbnail = "/Content/images/page2_img1.jpg",
                                CreatedById = userId, CreatedDateTime = DateTime.Now,
                                ModifiedById = userId, ModifiedDateTime = DateTime.Now,
                                ProjectImageMappings = new List<ProjectImageMapping>()
                                {
                                    new ProjectImageMapping()
                                    {
                                        ProjectImageId = 34, LanguageId = 129, Status = true, Deleted = false,
                                        Name = "Tên ảnh 1-  Dự Án 12", Description = "Dự Án 12 - ảnh 1 Description",
                                        CreatedById = userId, CreatedDateTime = DateTime.Now,
                                        ModifiedById = userId, ModifiedDateTime = DateTime.Now  
                                    },
                                    new ProjectImageMapping()
                                    {
                                        ProjectImageId = 34, LanguageId = 29, Status = true, Deleted = false,
                                        Name = "Project Name 12 - image 1", Description = "Project 12 - image 1 Description",
                                        CreatedById = userId, CreatedDateTime = DateTime.Now,
                                        ModifiedById = userId, ModifiedDateTime = DateTime.Now 
                                    }
                                }
                            },
                            new ProjectImage()
                            {
                                ProjectId = 12, Type = 1, Status = true, Deleted = false, ZOrder = 2,
                                ResourceUrl = "/Content/images/slide1.jpg", Thumbnail = "/Content/images/page2_img2.jpg",
                                CreatedById = userId, CreatedDateTime = DateTime.Now,
                                ModifiedById = userId, ModifiedDateTime = DateTime.Now,
                                ProjectImageMappings = new List<ProjectImageMapping>()
                                {
                                    new ProjectImageMapping()
                                    {
                                        ProjectImageId = 35, LanguageId = 129, Status = true, Deleted = false,
                                        Name = "Tên ảnh 2-  Dự Án 12", Description = "Dự Án 12 - ảnh 2 Description",
                                        CreatedById = userId, CreatedDateTime = DateTime.Now,
                                        ModifiedById = userId, ModifiedDateTime = DateTime.Now  
                                    },
                                    new ProjectImageMapping()
                                    {
                                        ProjectImageId = 35, LanguageId = 29, Status = true, Deleted = false,
                                        Name = "Project Name 12 - image 2", Description = "Project 12 - image 2 Description",
                                        CreatedById = userId, CreatedDateTime = DateTime.Now,
                                        ModifiedById = userId, ModifiedDateTime = DateTime.Now 
                                    }
                                }
                            },
                            new ProjectImage()
                            {
                                ProjectId = 12, Type = 1, Status = true, Deleted = false, ZOrder = 3,
                                ResourceUrl = "/Content/images/slide2.jpg", Thumbnail = "/Content/images/page2_img3.jpg",
                                CreatedById = userId, CreatedDateTime = DateTime.Now,
                                ModifiedById = userId, ModifiedDateTime = DateTime.Now,
                                ProjectImageMappings = new List<ProjectImageMapping>()
                                {
                                    new ProjectImageMapping()
                                    {
                                        ProjectImageId = 36, LanguageId = 129, Status = true, Deleted = false,
                                        Name = "Tên ảnh 3-  Dự Án 12", Description = "Dự Án 12 - ảnh 3 Description",
                                        CreatedById = userId, CreatedDateTime = DateTime.Now,
                                        ModifiedById = userId, ModifiedDateTime = DateTime.Now  
                                    },
                                    new ProjectImageMapping()
                                    {
                                        ProjectImageId = 36, LanguageId = 29, Status = true, Deleted = false,
                                        Name = "Project Name 12 - image 3", Description = "Project 12 - image 3 Description",
                                        CreatedById = userId, CreatedDateTime = DateTime.Now,
                                        ModifiedById = userId, ModifiedDateTime = DateTime.Now 
                                    }
                                }
                            }
                        }
                    },
                    // project 13
                    new Project() { 
                        Name = "Project 13", Type = 1, Status = true, Deleted = false, ZOrder = 13,
                        CreatedById = userId, CreatedDateTime = DateTime.Now,
                        ModifiedById = userId, ModifiedDateTime = DateTime.Now,
                        News = new List<News>
                        {
                            new News()
                            {
                                Name = "Project 13 - News", Status = true, Deleted = false,  NewsCategoryId = null,  ListTagId = "", 
                                CreatedById = userId, CreatedDateTime = DateTime.Now,
                                ModifiedById = userId, ModifiedDateTime = DateTime.Now,
                                NewsMappings = new List<NewsMapping>()
                                {
                                    new NewsMapping()
                                    {
                                        NewsId = 13, LanguageId = 129, Status = true, Deleted = false,
                                        Intro = "Dự Án 13 - News - Intro", Detail = "Dự Án 13 - News - Detail", MetaData = "<meta name='author' content='thanhit.net'>",
                                        Title = "Dự Án 13 - News ", Keyword = "Dự Án 13 - News Keyword", Description = "Dự Án 13 - News Description",
                                        CreatedById = userId, CreatedDateTime = DateTime.Now,
                                        ModifiedById = userId, ModifiedDateTime = DateTime.Now
                                    },
                                    new NewsMapping()
                                    {
                                        NewsId = 13, LanguageId = 29, Status = true, Deleted = false,
                                        Intro = "Project 13 - News - Intro", Detail = "Project 13- News - Detail", MetaData = "<meta name='author' content='thanhit.net'>",
                                        Title = "Project 13 - News", Keyword = "Project 13 - News Keyword", Description = "Project 13 - News Description",
                                        CreatedById = userId, CreatedDateTime = DateTime.Now,
                                        ModifiedById = userId, ModifiedDateTime = DateTime.Now
                                    }
                                }
                            }
                        },
                        ProjectMappings = new List<ProjectMapping>()
                        {
                            new ProjectMapping()
                            {
                                ProjectId = 13, LanguageId = 129, Status = true, Deleted = false,
                                Title = "Tên Dự Án 13", Keyword = "Dự Án 13 Keyword", Description = "Dự án 13 Description", MetaData = "<meta name='author' content='thanhit.net'>",
                                Intro = "Dự Án 13 - Intro", Detail = "Dự Án 13 Detail",
                                CreatedById = userId, CreatedDateTime = DateTime.Now,
                                ModifiedById = userId, ModifiedDateTime = DateTime.Now
                            },
                            new ProjectMapping()
                            {
                                ProjectId = 13, LanguageId = 29, Status = true, Deleted = false,
                                Title = "Project Name 13", Keyword = "Project 13 Keyword", Description = "Project 13 Description", MetaData = "<meta name='author' content='thanhit.net'>",
                                Intro = "Project 13 - Intro", Detail = "Project 13 Detail",
                                CreatedById = userId, CreatedDateTime = DateTime.Now,
                                ModifiedById = userId, ModifiedDateTime = DateTime.Now
                            }
                        },
                        ProjectImages = new List<ProjectImage>()
                        {
                            new ProjectImage()
                            {
                                ProjectId = 13, Type = 1, Status = true, Deleted = false, ZOrder = 1,
                                ResourceUrl = "/Content/images/slide.jpg", Thumbnail = "/Content/images/page2_img1.jpg",
                                CreatedById = userId, CreatedDateTime = DateTime.Now,
                                ModifiedById = userId, ModifiedDateTime = DateTime.Now,
                                ProjectImageMappings = new List<ProjectImageMapping>()
                                {
                                    new ProjectImageMapping()
                                    {
                                        ProjectImageId = 37, LanguageId = 129, Status = true, Deleted = false,
                                        Name = "Tên ảnh 1-  Dự Án 13", Description = "Dự Án 13 - ảnh 1 Description",
                                        CreatedById = userId, CreatedDateTime = DateTime.Now,
                                        ModifiedById = userId, ModifiedDateTime = DateTime.Now  
                                    },
                                    new ProjectImageMapping()
                                    {
                                        ProjectImageId = 37, LanguageId = 29, Status = true, Deleted = false,
                                        Name = "Project Name 13 - image 1", Description = "Project 13 - image 1 Description",
                                        CreatedById = userId, CreatedDateTime = DateTime.Now,
                                        ModifiedById = userId, ModifiedDateTime = DateTime.Now 
                                    }
                                }
                            },
                            new ProjectImage()
                            {
                                ProjectId = 13, Type = 1, Status = true, Deleted = false, ZOrder = 2,
                                ResourceUrl = "/Content/images/slide1.jpg", Thumbnail = "/Content/images/page2_img2.jpg",
                                CreatedById = userId, CreatedDateTime = DateTime.Now,
                                ModifiedById = userId, ModifiedDateTime = DateTime.Now,
                                ProjectImageMappings = new List<ProjectImageMapping>()
                                {
                                    new ProjectImageMapping()
                                    {
                                        ProjectImageId = 38, LanguageId = 129, Status = true, Deleted = false,
                                        Name = "Tên ảnh 2-  Dự Án 13", Description = "Dự Án 13 - ảnh 2 Description",
                                        CreatedById = userId, CreatedDateTime = DateTime.Now,
                                        ModifiedById = userId, ModifiedDateTime = DateTime.Now  
                                    },
                                    new ProjectImageMapping()
                                    {
                                        ProjectImageId = 38, LanguageId = 29, Status = true, Deleted = false,
                                        Name = "Project Name 13 - image 2", Description = "Project 13 - image 2 Description",
                                        CreatedById = userId, CreatedDateTime = DateTime.Now,
                                        ModifiedById = userId, ModifiedDateTime = DateTime.Now 
                                    }
                                }
                            },
                            new ProjectImage()
                            {
                                ProjectId = 13, Type = 1, Status = true, Deleted = false, ZOrder = 3,
                                ResourceUrl = "/Content/images/slide2.jpg", Thumbnail = "/Content/images/page2_img3.jpg",
                                CreatedById = userId, CreatedDateTime = DateTime.Now,
                                ModifiedById = userId, ModifiedDateTime = DateTime.Now,
                                ProjectImageMappings = new List<ProjectImageMapping>()
                                {
                                    new ProjectImageMapping()
                                    {
                                        ProjectImageId = 39, LanguageId = 129, Status = true, Deleted = false,
                                        Name = "Tên ảnh 3-  Dự Án 13", Description = "Dự Án 13 - ảnh 3 Description",
                                        CreatedById = userId, CreatedDateTime = DateTime.Now,
                                        ModifiedById = userId, ModifiedDateTime = DateTime.Now  
                                    },
                                    new ProjectImageMapping()
                                    {
                                        ProjectImageId = 39, LanguageId = 29, Status = true, Deleted = false,
                                        Name = "Project Name 13 - image 3", Description = "Project 13 - image 3 Description",
                                        CreatedById = userId, CreatedDateTime = DateTime.Now,
                                        ModifiedById = userId, ModifiedDateTime = DateTime.Now 
                                    }
                                }
                            }
                        }
                    },
                    // project 14
                    new Project() { 
                        Name = "Project 14", Type = 1, Status = true, Deleted = false, ZOrder = 14,
                        CreatedById = userId, CreatedDateTime = DateTime.Now,
                        ModifiedById = userId, ModifiedDateTime = DateTime.Now,
                        News = new List<News>
                        {
                            new News()
                            {
                                Name = "Project 14 - News", Status = true, Deleted = false,  NewsCategoryId = null,  ListTagId = "", 
                                CreatedById = userId, CreatedDateTime = DateTime.Now,
                                ModifiedById = userId, ModifiedDateTime = DateTime.Now,
                                NewsMappings = new List<NewsMapping>()
                                {
                                    new NewsMapping()
                                    {
                                        NewsId = 14, LanguageId = 129, Status = true, Deleted = false,
                                        Intro = "Dự Án 14 - News - Intro", Detail = "Dự Án 14 - News - Detail", MetaData = "<meta name='author' content='thanhit.net'>",
                                        Title = "Dự Án 14 - News ", Keyword = "Dự Án 14 - News Keyword", Description = "Dự Án 14 - News Description",
                                        CreatedById = userId, CreatedDateTime = DateTime.Now,
                                        ModifiedById = userId, ModifiedDateTime = DateTime.Now
                                    },
                                    new NewsMapping()
                                    {
                                        NewsId = 14, LanguageId = 29, Status = true, Deleted = false,
                                        Intro = "Project 14 - News - Intro", Detail = "Project 14- News - Detail", MetaData = "<meta name='author' content='thanhit.net'>",
                                        Title = "Project 14 - News", Keyword = "Project 14 - News Keyword", Description = "Project 14 - News Description",
                                        CreatedById = userId, CreatedDateTime = DateTime.Now,
                                        ModifiedById = userId, ModifiedDateTime = DateTime.Now
                                    }
                                }
                            }
                        },
                        ProjectMappings = new List<ProjectMapping>()
                        {
                            new ProjectMapping()
                            {
                                ProjectId = 14, LanguageId = 129, Status = true, Deleted = false,
                                Title = "Tên Dự Án 14", Keyword = "Dự Án 14 Keyword", Description = "Dự án 14 Description", MetaData = "<meta name='author' content='thanhit.net'>",
                                Intro = "Dự Án 14 - Intro", Detail = "Dự Án 14 Detail",
                                CreatedById = userId, CreatedDateTime = DateTime.Now,
                                ModifiedById = userId, ModifiedDateTime = DateTime.Now
                            },
                            new ProjectMapping()
                            {
                                ProjectId = 14, LanguageId = 29, Status = true, Deleted = false,
                                Title = "Project Name 14", Keyword = "Project 14 Keyword", Description = "Project 14 Description", MetaData = "<meta name='author' content='thanhit.net'>",
                                Intro = "Project 14 - Intro", Detail = "Project 14 Detail",
                                CreatedById = userId, CreatedDateTime = DateTime.Now,
                                ModifiedById = userId, ModifiedDateTime = DateTime.Now
                            }
                        },
                        ProjectImages = new List<ProjectImage>()
                        {
                            new ProjectImage()
                            {
                                ProjectId = 14, Type = 1, Status = true, Deleted = false, ZOrder = 1,
                                ResourceUrl = "/Content/images/slide.jpg", Thumbnail = "/Content/images/page2_img1.jpg",
                                CreatedById = userId, CreatedDateTime = DateTime.Now,
                                ModifiedById = userId, ModifiedDateTime = DateTime.Now,
                                ProjectImageMappings = new List<ProjectImageMapping>()
                                {
                                    new ProjectImageMapping()
                                    {
                                        ProjectImageId = 40, LanguageId = 129, Status = true, Deleted = false,
                                        Name = "Tên ảnh 1-  Dự Án 14", Description = "Dự Án 14 - ảnh 1 Description",
                                        CreatedById = userId, CreatedDateTime = DateTime.Now,
                                        ModifiedById = userId, ModifiedDateTime = DateTime.Now  
                                    },
                                    new ProjectImageMapping()
                                    {
                                        ProjectImageId = 40, LanguageId = 29, Status = true, Deleted = false,
                                        Name = "Project Name 14 - image 1", Description = "Project 14 - image 1 Description",
                                        CreatedById = userId, CreatedDateTime = DateTime.Now,
                                        ModifiedById = userId, ModifiedDateTime = DateTime.Now 
                                    }
                                }
                            },
                            new ProjectImage()
                            {
                                ProjectId = 14, Type = 1, Status = true, Deleted = false, ZOrder = 2,
                                ResourceUrl = "/Content/images/slide1.jpg", Thumbnail = "/Content/images/page2_img2.jpg",
                                CreatedById = userId, CreatedDateTime = DateTime.Now,
                                ModifiedById = userId, ModifiedDateTime = DateTime.Now,
                                ProjectImageMappings = new List<ProjectImageMapping>()
                                {
                                    new ProjectImageMapping()
                                    {
                                        ProjectImageId = 41, LanguageId = 129, Status = true, Deleted = false,
                                        Name = "Tên ảnh 2-  Dự Án 14", Description = "Dự Án 14 - ảnh 2 Description",
                                        CreatedById = userId, CreatedDateTime = DateTime.Now,
                                        ModifiedById = userId, ModifiedDateTime = DateTime.Now  
                                    },
                                    new ProjectImageMapping()
                                    {
                                        ProjectImageId = 41, LanguageId = 29, Status = true, Deleted = false,
                                        Name = "Project Name 14 - image 2", Description = "Project 14 - image 2 Description",
                                        CreatedById = userId, CreatedDateTime = DateTime.Now,
                                        ModifiedById = userId, ModifiedDateTime = DateTime.Now 
                                    }
                                }
                            },
                            new ProjectImage()
                            {
                                ProjectId = 14, Type = 1, Status = true, Deleted = false, ZOrder = 3,
                                ResourceUrl = "/Content/images/slide2.jpg", Thumbnail = "/Content/images/page2_img3.jpg",
                                CreatedById = userId, CreatedDateTime = DateTime.Now,
                                ModifiedById = userId, ModifiedDateTime = DateTime.Now,
                                ProjectImageMappings = new List<ProjectImageMapping>()
                                {
                                    new ProjectImageMapping()
                                    {
                                        ProjectImageId = 42, LanguageId = 129, Status = true, Deleted = false,
                                        Name = "Tên ảnh 3-  Dự Án 14", Description = "Dự Án 14 - ảnh 3 Description",
                                        CreatedById = userId, CreatedDateTime = DateTime.Now,
                                        ModifiedById = userId, ModifiedDateTime = DateTime.Now  
                                    },
                                    new ProjectImageMapping()
                                    {
                                        ProjectImageId = 42, LanguageId = 29, Status = true, Deleted = false,
                                        Name = "Project Name 14 - image 3", Description = "Project 14 - image 3 Description",
                                        CreatedById = userId, CreatedDateTime = DateTime.Now,
                                        ModifiedById = userId, ModifiedDateTime = DateTime.Now 
                                    }
                                }
                            }
                        }
                    },
                    // project 15
                    new Project() { 
                        Name = "Project 15 - Video Clip", Type = 2, Status = true, Deleted = false, ZOrder = 15,
                        CreatedById = userId, CreatedDateTime = DateTime.Now,
                        ModifiedById = userId, ModifiedDateTime = DateTime.Now,
                        ProjectMappings = new List<ProjectMapping>()
                        {
                            new ProjectMapping()
                            {
                                ProjectId = 15, LanguageId = 129, Status = true, Deleted = false,
                                Title = "Project 15 - Video Clip", Keyword = "Project 15 - Video Clip Keyword", Description = "Project 15 - Video Clip Description", MetaData = "<meta name='author' content='thanhit.net'>",
                                Intro = "Project 15 - Video Clip - Intro", Detail = "Project 15 - Video Clip Detail",
                                CreatedById = userId, CreatedDateTime = DateTime.Now,
                                ModifiedById = userId, ModifiedDateTime = DateTime.Now
                            },
                            new ProjectMapping()
                            {
                                ProjectId = 15, LanguageId = 29, Status = true, Deleted = false,
                                Title = "Project 15 - Video Clip", Keyword = "Project 15 - Video Clip Keyword", Description = "Project 15 - Video Clip Description", MetaData = "<meta name='author' content='thanhit.net'>",
                                Intro = "Project 15 - Video Clip - Intro", Detail = "Project 15 - Video Clip Detail",
                                CreatedById = userId, CreatedDateTime = DateTime.Now,
                                ModifiedById = userId, ModifiedDateTime = DateTime.Now
                            }
                        },
                        ProjectImages = new List<ProjectImage>()
                        {
                            new ProjectImage()
                            {
                                ProjectId = 15, Type = 2, Status = true, Deleted = false, ZOrder = 1,
                                ResourceUrl = "uE6A0G4Luog", Thumbnail = "/Content/images/page2_img1.jpg",
                                CreatedById = userId, CreatedDateTime = DateTime.Now,
                                ModifiedById = userId, ModifiedDateTime = DateTime.Now,
                                ProjectImageMappings = new List<ProjectImageMapping>()
                                {
                                    new ProjectImageMapping()
                                    {
                                        ProjectImageId = 43, LanguageId = 129, Status = true, Deleted = false,
                                        Name = "Tên Video 1 -  Dự Án 15", Description = "Dự Án 15 - Video 1 Description",
                                        CreatedById = userId, CreatedDateTime = DateTime.Now,
                                        ModifiedById = userId, ModifiedDateTime = DateTime.Now  
                                    },
                                    new ProjectImageMapping()
                                    {
                                        ProjectImageId = 43, LanguageId = 29, Status = true, Deleted = false,
                                        Name = "Project Name 15 - Video 1", Description = "Project 15 - Video 1 Description",
                                        CreatedById = userId, CreatedDateTime = DateTime.Now,
                                        ModifiedById = userId, ModifiedDateTime = DateTime.Now 
                                    }
                                }
                            },
                            new ProjectImage()
                            {
                                ProjectId = 15, Type = 2, Status = true, Deleted = false, ZOrder = 2,
                                ResourceUrl = "uE6A0G4Luog", Thumbnail = "/Content/images/page2_img2.jpg",
                                CreatedById = userId, CreatedDateTime = DateTime.Now,
                                ModifiedById = userId, ModifiedDateTime = DateTime.Now,
                                ProjectImageMappings = new List<ProjectImageMapping>()
                                {
                                    new ProjectImageMapping()
                                    {
                                        ProjectImageId = 44, LanguageId = 129, Status = true, Deleted = false,
                                        Name = "Tên Video 2 -  Dự Án 15", Description = "Dự Án 15 - Video 2 Description",
                                        CreatedById = userId, CreatedDateTime = DateTime.Now,
                                        ModifiedById = userId, ModifiedDateTime = DateTime.Now  
                                    },
                                    new ProjectImageMapping()
                                    {
                                        ProjectImageId = 44, LanguageId = 29, Status = true, Deleted = false,
                                        Name = "Project Name 15 - Video 2", Description = "Project 15 - Video 2 Description",
                                        CreatedById = userId, CreatedDateTime = DateTime.Now,
                                        ModifiedById = userId, ModifiedDateTime = DateTime.Now 
                                    }
                                }
                            },
                            new ProjectImage()
                            {
                                ProjectId = 15, Type = 2, Status = true, Deleted = false, ZOrder = 3,
                                ResourceUrl = "uE6A0G4Luog", Thumbnail = "/Content/images/page2_img3.jpg",
                                CreatedById = userId, CreatedDateTime = DateTime.Now,
                                ModifiedById = userId, ModifiedDateTime = DateTime.Now,
                                ProjectImageMappings = new List<ProjectImageMapping>()
                                {
                                    new ProjectImageMapping()
                                    {
                                        ProjectImageId = 45, LanguageId = 129, Status = true, Deleted = false,
                                        Name = "Tên Video 3 -  Dự Án 15", Description = "Dự Án 15 - Video 3 Description",
                                        CreatedById = userId, CreatedDateTime = DateTime.Now,
                                        ModifiedById = userId, ModifiedDateTime = DateTime.Now  
                                    },
                                    new ProjectImageMapping()
                                    {
                                        ProjectImageId = 45, LanguageId = 29, Status = true, Deleted = false,
                                        Name = "Project Name 15 - Video 3", Description = "Project 15 - Video 3 Description",
                                        CreatedById = userId, CreatedDateTime = DateTime.Now,
                                        ModifiedById = userId, ModifiedDateTime = DateTime.Now 
                                    }
                                }
                            },
                            new ProjectImage()
                            {
                                ProjectId = 15, Type = 2, Status = true, Deleted = false, ZOrder = 4,
                                ResourceUrl = "uE6A0G4Luog", Thumbnail = "/Content/images/page2_img1.jpg",
                                CreatedById = userId, CreatedDateTime = DateTime.Now,
                                ModifiedById = userId, ModifiedDateTime = DateTime.Now,
                                ProjectImageMappings = new List<ProjectImageMapping>()
                                {
                                    new ProjectImageMapping()
                                    {
                                        ProjectImageId = 46, LanguageId = 129, Status = true, Deleted = false,
                                        Name = "Tên Video 4 -  Dự Án 15", Description = "Dự Án 15 - Video 4 Description",
                                        CreatedById = userId, CreatedDateTime = DateTime.Now,
                                        ModifiedById = userId, ModifiedDateTime = DateTime.Now  
                                    },
                                    new ProjectImageMapping()
                                    {
                                        ProjectImageId = 46, LanguageId = 29, Status = true, Deleted = false,
                                        Name = "Project Name 15 - Video 4", Description = "Project 15 - Video 4 Description",
                                        CreatedById = userId, CreatedDateTime = DateTime.Now,
                                        ModifiedById = userId, ModifiedDateTime = DateTime.Now 
                                    }
                                }
                            },
                            new ProjectImage()
                            {
                                ProjectId = 15, Type = 2, Status = true, Deleted = false, ZOrder = 5,
                                ResourceUrl = "uE6A0G4Luog", Thumbnail = "/Content/images/page2_img2.jpg",
                                CreatedById = userId, CreatedDateTime = DateTime.Now,
                                ModifiedById = userId, ModifiedDateTime = DateTime.Now,
                                ProjectImageMappings = new List<ProjectImageMapping>()
                                {
                                    new ProjectImageMapping()
                                    {
                                        ProjectImageId = 47, LanguageId = 129, Status = true, Deleted = false,
                                        Name = "Tên Video 5 -  Dự Án 15", Description = "Dự Án 15 - Video 5 Description",
                                        CreatedById = userId, CreatedDateTime = DateTime.Now,
                                        ModifiedById = userId, ModifiedDateTime = DateTime.Now  
                                    },
                                    new ProjectImageMapping()
                                    {
                                        ProjectImageId = 47, LanguageId = 29, Status = true, Deleted = false,
                                        Name = "Project Name 15 - Video 5", Description = "Project 15 - Video 5 Description",
                                        CreatedById = userId, CreatedDateTime = DateTime.Now,
                                        ModifiedById = userId, ModifiedDateTime = DateTime.Now 
                                    }
                                }
                            },
                            new ProjectImage()
                            {
                                ProjectId = 15, Type = 2, Status = true, Deleted = false, ZOrder = 6,
                                ResourceUrl = "uE6A0G4Luog", Thumbnail = "/Content/images/page2_img3.jpg",
                                CreatedById = userId, CreatedDateTime = DateTime.Now,
                                ModifiedById = userId, ModifiedDateTime = DateTime.Now,
                                ProjectImageMappings = new List<ProjectImageMapping>()
                                {
                                    new ProjectImageMapping()
                                    {
                                        ProjectImageId = 48, LanguageId = 129, Status = true, Deleted = false,
                                        Name = "Tên Video 6 -  Dự Án 15", Description = "Dự Án 15 - Video 6 Description",
                                        CreatedById = userId, CreatedDateTime = DateTime.Now,
                                        ModifiedById = userId, ModifiedDateTime = DateTime.Now  
                                    },
                                    new ProjectImageMapping()
                                    {
                                        ProjectImageId = 48, LanguageId = 29, Status = true, Deleted = false,
                                        Name = "Project Name 15 - Video 6", Description = "Project 15 - Video 6 Description",
                                        CreatedById = userId, CreatedDateTime = DateTime.Now,
                                        ModifiedById = userId, ModifiedDateTime = DateTime.Now 
                                    }
                                }
                            },
                            new ProjectImage()
                            {
                                ProjectId = 15, Type = 2, Status = true, Deleted = false, ZOrder = 7,
                                ResourceUrl = "uE6A0G4Luog", Thumbnail = "/Content/images/page2_img3.jpg",
                                CreatedById = userId, CreatedDateTime = DateTime.Now,
                                ModifiedById = userId, ModifiedDateTime = DateTime.Now,
                                ProjectImageMappings = new List<ProjectImageMapping>()
                                {
                                    new ProjectImageMapping()
                                    {
                                        ProjectImageId = 49, LanguageId = 129, Status = true, Deleted = false,
                                        Name = "Tên Video 7 -  Dự Án 15", Description = "Dự Án 15 - Video 7 Description",
                                        CreatedById = userId, CreatedDateTime = DateTime.Now,
                                        ModifiedById = userId, ModifiedDateTime = DateTime.Now  
                                    },
                                    new ProjectImageMapping()
                                    {
                                        ProjectImageId = 49, LanguageId = 29, Status = true, Deleted = false,
                                        Name = "Project Name 15 - Video 7", Description = "Project 15 - Video 7 Description",
                                        CreatedById = userId, CreatedDateTime = DateTime.Now,
                                        ModifiedById = userId, ModifiedDateTime = DateTime.Now 
                                    }
                                }
                            }
                        }
                    },
                    // project 16
                    new Project() { 
                        Name = "Project 16 - Công trình thực tế", Type = 3, Status = true, Deleted = false, ZOrder = 16,
                        CreatedById = userId, CreatedDateTime = DateTime.Now,
                        ModifiedById = userId, ModifiedDateTime = DateTime.Now,
                        ProjectMappings = new List<ProjectMapping>()
                        {
                            new ProjectMapping()
                            {
                                ProjectId = 16, LanguageId = 129, Status = true, Deleted = false,
                                Title = "Project 16 - Công trình thực tế", Keyword = "Project 16 - Công trình thực tế Keyword", Description = "Project 16 - Công trình thực tế Description", MetaData = "<meta name='author' content='thanhit.net'>",
                                Intro = "Project 16 - Công trình thực tế - Intro", Detail = "Project 16 - Công trình thực tế Detail",
                                CreatedById = userId, CreatedDateTime = DateTime.Now,
                                ModifiedById = userId, ModifiedDateTime = DateTime.Now
                            },
                            new ProjectMapping()
                            {
                                ProjectId = 16, LanguageId = 29, Status = true, Deleted = false,
                                Title = "Project 16 - Công trình thực tế", Keyword = "Project 16 - Công trình thực tế Keyword", Description = "Project 16 - Công trình thực tế Description", MetaData = "<meta name='author' content='thanhit.net'>",
                                Intro = "Project 16 - Công trình thực tế - Intro", Detail = "Project 16 - Công trình thực tế Detail",
                                CreatedById = userId, CreatedDateTime = DateTime.Now,
                                ModifiedById = userId, ModifiedDateTime = DateTime.Now
                            }
                        },
                        News = new List<News>()
                        {
                            new News()
                            {
                                Name = "Công trình thực tế 1 - News", Status = true, Deleted = false, ZOrder = 1,  NewsCategoryId = null,  ListTagId = "",
                                CreatedById = userId, CreatedDateTime = DateTime.Now,
                                ModifiedById = userId, ModifiedDateTime = DateTime.Now,
                                NewsMappings = new List<NewsMapping>()
                                {
                                    new NewsMapping()
                                    {
                                        NewsId = 16, LanguageId = 129, Status = true, Deleted = false,
                                        Intro = "Công trình thực tế 1 - News - Intro", Detail = "Công trình thực tế 1 - News - Detail" + detail, MetaData = "<meta name='author' content='thanhit.net'>",
                                        Title = "Công trình thực tế 1 - News ", Keyword = "Công trình thực tế 1 - News Keyword", Description = "Công trình thực tế 1 - News Description",
                                        ThumbUrl = "/Content/images/feat1.jpg", 
                                        CreatedById = userId, CreatedDateTime = DateTime.Now,
                                        ModifiedById = userId, ModifiedDateTime = DateTime.Now
                                    },
                                    new NewsMapping()
                                    {
                                        NewsId = 16, LanguageId = 29, Status = true, Deleted = false,
                                        Intro = "Construction 1 - News - Intro", Detail = "Construction 1- News - Detail" + detail, MetaData = "<meta name='author' content='thanhit.net'>",
                                        Title = "Construction 1 - News", Keyword = "Construction 1 - News Keyword", Description = "Construction 1 - News Description",
                                        ThumbUrl = "/Content/images/feat1.jpg", 
                                        CreatedById = userId, CreatedDateTime = DateTime.Now,
                                        ModifiedById = userId, ModifiedDateTime = DateTime.Now
                                    }
                                }
                            },
                            new News()
                            {
                                Name = "Công trình thực tế 2 - News", Status = true, Deleted = false, ZOrder = 2,  NewsCategoryId = null,  ListTagId = "", 
                                CreatedById = userId, CreatedDateTime = DateTime.Now,
                                ModifiedById = userId, ModifiedDateTime = DateTime.Now,
                                NewsMappings = new List<NewsMapping>()
                                {
                                    new NewsMapping()
                                    {
                                        NewsId = 17, LanguageId = 129, Status = true, Deleted = false,
                                        Intro = "Công trình thực tế 2 - News - Intro", Detail = "Công trình thực tế 2 - News - Detail" + detail, MetaData = "<meta name='author' content='thanhit.net'>",
                                        Title = "Công trình thực tế 2 - News ", Keyword = "Công trình thực tế 2 - News Keyword", Description = "Công trình thực tế 2 - News Description",
                                        ThumbUrl = "/Content/images/feat2.jpg", 
                                        CreatedById = userId, CreatedDateTime = DateTime.Now,
                                        ModifiedById = userId, ModifiedDateTime = DateTime.Now
                                    },
                                    new NewsMapping()
                                    {
                                        NewsId = 17, LanguageId = 29, Status = true, Deleted = false,
                                        Intro = "Construction 2 - News - Intro", Detail = "Construction 2- News - Detail" + detail, MetaData = "<meta name='author' content='thanhit.net'>",
                                        Title = "Construction 2 - News", Keyword = "Construction 2 - News Keyword", Description = "Construction 2 - News Description",
                                        ThumbUrl = "/Content/images/feat2.jpg", 
                                        CreatedById = userId, CreatedDateTime = DateTime.Now,
                                        ModifiedById = userId, ModifiedDateTime = DateTime.Now
                                    }
                                }
                            },
                            new News()
                            {
                                Name = "Công trình thực tế 3 - News", Status = true, Deleted = false, ZOrder = 3,  NewsCategoryId = null,  ListTagId = "", 
                                CreatedById = userId, CreatedDateTime = DateTime.Now,
                                ModifiedById = userId, ModifiedDateTime = DateTime.Now,
                                NewsMappings = new List<NewsMapping>()
                                {
                                    new NewsMapping()
                                    {
                                        NewsId = 18, LanguageId = 129, Status = true, Deleted = false,
                                        Intro = "Công trình thực tế 3 - News - Intro", Detail = "Công trình thực tế 3 - News - Detail" + detail, MetaData = "<meta name='author' content='thanhit.net'>",
                                        Title = "Công trình thực tế 3 - News ", Keyword = "Công trình thực tế 3 - News Keyword", Description = "Công trình thực tế 3 - News Description",
                                        ThumbUrl = "/Content/images/feat3.jpg", 
                                        CreatedById = userId, CreatedDateTime = DateTime.Now,
                                        ModifiedById = userId, ModifiedDateTime = DateTime.Now
                                    },
                                    new NewsMapping()
                                    {
                                        NewsId = 18, LanguageId = 29, Status = true, Deleted = false,
                                        Intro = "Construction 3 - News - Intro", Detail = "Construction 3- News - Detail" + detail, MetaData = "<meta name='author' content='thanhit.net'>",
                                        Title = "Construction 3 - News", Keyword = "Construction 3 - News Keyword", Description = "Construction 3 - News Description",
                                        ThumbUrl = "/Content/images/feat3.jpg", 
                                        CreatedById = userId, CreatedDateTime = DateTime.Now,
                                        ModifiedById = userId, ModifiedDateTime = DateTime.Now
                                    }
                                }
                            },
                            new News()
                            {
                                Name = "Công trình thực tế 4 - News", Status = true, Deleted = false, ZOrder = 4,  NewsCategoryId = null,  ListTagId = "", 
                                CreatedById = userId, CreatedDateTime = DateTime.Now,
                                ModifiedById = userId, ModifiedDateTime = DateTime.Now,
                                NewsMappings = new List<NewsMapping>()
                                {
                                    new NewsMapping()
                                    {
                                        NewsId = 19, LanguageId = 129, Status = true, Deleted = false,
                                        Intro = "Công trình thực tế 4 - News - Intro", Detail = "Công trình thực tế 4 - News - Detail" + detail, MetaData = "<meta name='author' content='thanhit.net'>",
                                        Title = "Công trình thực tế 4 - News ", Keyword = "Công trình thực tế 4 - News Keyword", Description = "Công trình thực tế 4 - News Description",
                                        ThumbUrl = "/Content/images/feat4.jpg", 
                                        CreatedById = userId, CreatedDateTime = DateTime.Now,
                                        ModifiedById = userId, ModifiedDateTime = DateTime.Now
                                    },
                                    new NewsMapping()
                                    {
                                        NewsId = 19, LanguageId = 29, Status = true, Deleted = false,
                                        Intro = "Construction 4 - News - Intro", Detail = "Construction 4- News - Detail" + detail, MetaData = "<meta name='author' content='thanhit.net'>",
                                        Title = "Construction 4 - News", Keyword = "Construction 4 - News Keyword", Description = "Construction 4 - News Description",
                                        ThumbUrl = "/Content/images/feat4.jpg", 
                                        CreatedById = userId, CreatedDateTime = DateTime.Now,
                                        ModifiedById = userId, ModifiedDateTime = DateTime.Now
                                    }
                                }
                            },
                            new News()
                            {
                                Name = "Công trình thực tế 5 - News", Status = true, Deleted = false, ZOrder = 5,  NewsCategoryId = null,  ListTagId = "", 
                                CreatedById = userId, CreatedDateTime = DateTime.Now,
                                ModifiedById = userId, ModifiedDateTime = DateTime.Now,
                                NewsMappings = new List<NewsMapping>()
                                {
                                    new NewsMapping()
                                    {
                                        NewsId = 20, LanguageId = 129, Status = true, Deleted = false,
                                        Intro = "Công trình thực tế 5 - News - Intro", Detail = "Công trình thực tế 5 - News - Detail" + detail, MetaData = "<meta name='author' content='thanhit.net'>",
                                        Title = "Công trình thực tế 5 - News ", Keyword = "Công trình thực tế 5 - News Keyword", Description = "Công trình thực tế 5 - News Description",
                                        ThumbUrl = "/Content/images/feat5.jpg", 
                                        CreatedById = userId, CreatedDateTime = DateTime.Now,
                                        ModifiedById = userId, ModifiedDateTime = DateTime.Now
                                    },
                                    new NewsMapping()
                                    {
                                        NewsId = 20, LanguageId = 29, Status = true, Deleted = false,
                                        Intro = "Construction 5 - News - Intro", Detail = "Construction 5- News - Detail" + detail, MetaData = "<meta name='author' content='thanhit.net'>",
                                        Title = "Construction 5 - News", Keyword = "Construction 5 - News Keyword", Description = "Construction 5 - News Description",
                                        ThumbUrl = "/Content/images/feat5.jpg", 
                                        CreatedById = userId, CreatedDateTime = DateTime.Now,
                                        ModifiedById = userId, ModifiedDateTime = DateTime.Now
                                    }
                                }
                            },
                            new News()
                            {
                                Name = "Công trình thực tế 6 - News", Status = true, Deleted = false, ZOrder = 6,  NewsCategoryId = null,  ListTagId = "", 
                                CreatedById = userId, CreatedDateTime = DateTime.Now,
                                ModifiedById = userId, ModifiedDateTime = DateTime.Now,
                                NewsMappings = new List<NewsMapping>()
                                {
                                    new NewsMapping()
                                    {
                                        NewsId = 21, LanguageId = 129, Status = true, Deleted = false,
                                        Intro = "Công trình thực tế 6 - News - Intro", Detail = "Công trình thực tế 6 - News - Detail" + detail, MetaData = "<meta name='author' content='thanhit.net'>",
                                        Title = "Công trình thực tế 6 - News ", Keyword = "Công trình thực tế 6 - News Keyword", Description = "Công trình thực tế 6 - News Description",
                                        ThumbUrl = "/Content/images/feat6.jpg", 
                                        CreatedById = userId, CreatedDateTime = DateTime.Now,
                                        ModifiedById = userId, ModifiedDateTime = DateTime.Now
                                    },
                                    new NewsMapping()
                                    {
                                        NewsId = 21, LanguageId = 29, Status = true, Deleted = false,
                                        Intro = "Construction 6 - News - Intro", Detail = "Construction 6- News - Detail" + detail, MetaData = "<meta name='author' content='thanhit.net'>",
                                        Title = "Construction 6 - News", Keyword = "Construction 6 - News Keyword", Description = "Construction 6 - News Description",
                                        ThumbUrl = "/Content/images/feat6.jpg", 
                                        CreatedById = userId, CreatedDateTime = DateTime.Now,
                                        ModifiedById = userId, ModifiedDateTime = DateTime.Now
                                    }
                                }
                            },
                            new News()
                            {
                                Name = "Công trình thực tế 7 - News", Status = true, Deleted = false, ZOrder = 7,  NewsCategoryId = null,  ListTagId = "", 
                                CreatedById = userId, CreatedDateTime = DateTime.Now,
                                ModifiedById = userId, ModifiedDateTime = DateTime.Now,
                                NewsMappings = new List<NewsMapping>()
                                {
                                    new NewsMapping()
                                    {
                                        NewsId = 22, LanguageId = 129, Status = true, Deleted = false,
                                        Intro = "Công trình thực tế 7 - News - Intro", Detail = "Công trình thực tế 7 - News - Detail" + detail, MetaData = "<meta name='author' content='thanhit.net'>",
                                        Title = "Công trình thực tế 7 - News ", Keyword = "Công trình thực tế 7 - News Keyword", Description = "Công trình thực tế 7 - News Description",
                                        ThumbUrl = "/Content/images/feat7.jpg", 
                                        CreatedById = userId, CreatedDateTime = DateTime.Now,
                                        ModifiedById = userId, ModifiedDateTime = DateTime.Now
                                    },
                                    new NewsMapping()
                                    {
                                        NewsId = 22, LanguageId = 29, Status = true, Deleted = false,
                                        Intro = "Construction 7 - News - Intro", Detail = "Construction 7- News - Detail" + detail, MetaData = "<meta name='author' content='thanhit.net'>",
                                        Title = "Construction 7 - News", Keyword = "Construction 7 - News Keyword", Description = "Construction 7 - News Description",
                                        ThumbUrl = "/Content/images/feat7.jpg", 
                                        CreatedById = userId, CreatedDateTime = DateTime.Now,
                                        ModifiedById = userId, ModifiedDateTime = DateTime.Now
                                    }
                                }
                            },
                            new News()
                            {
                                Name = "Công trình thực tế 8 - News", Status = true, Deleted = false, ZOrder = 8,  NewsCategoryId = null,  ListTagId = "", 
                                CreatedById = userId, CreatedDateTime = DateTime.Now,
                                ModifiedById = userId, ModifiedDateTime = DateTime.Now,
                                NewsMappings = new List<NewsMapping>()
                                {
                                    new NewsMapping()
                                    {
                                        NewsId = 23, LanguageId = 129, Status = true, Deleted = false,
                                        Intro = "Công trình thực tế 8 - News - Intro", Detail = "Công trình thực tế 8 - News - Detail" + detail, MetaData = "<meta name='author' content='thanhit.net'>",
                                        Title = "Công trình thực tế 8 - News ", Keyword = "Công trình thực tế 8 - News Keyword", Description = "Công trình thực tế 8 - News Description",
                                        ThumbUrl = "/Content/images/feat8.jpg", 
                                        CreatedById = userId, CreatedDateTime = DateTime.Now,
                                        ModifiedById = userId, ModifiedDateTime = DateTime.Now
                                    },
                                    new NewsMapping()
                                    {
                                        NewsId = 23, LanguageId = 29, Status = true, Deleted = false,
                                        Intro = "Construction 8 - News - Intro", Detail = "Construction 8- News - Detail" + detail, MetaData = "<meta name='author' content='thanhit.net'>",
                                        Title = "Construction 8 - News", Keyword = "Construction 8 - News Keyword", Description = "Construction 8 - News Description",
                                        ThumbUrl = "/Content/images/feat8.jpg", 
                                        CreatedById = userId, CreatedDateTime = DateTime.Now,
                                        ModifiedById = userId, ModifiedDateTime = DateTime.Now
                                    }
                                }
                            },
                            new News()
                            {
                                Name = "Công trình thực tế 9 - News", Status = true, Deleted = false, ZOrder = 9,  NewsCategoryId = null,  ListTagId = "", 
                                CreatedById = userId, CreatedDateTime = DateTime.Now,
                                ModifiedById = userId, ModifiedDateTime = DateTime.Now,
                                NewsMappings = new List<NewsMapping>()
                                {
                                    new NewsMapping()
                                    {
                                        NewsId = 24, LanguageId = 129, Status = true, Deleted = false,
                                        Intro = "Công trình thực tế 9 - News - Intro", Detail = "Công trình thực tế 9 - News - Detail" + detail, MetaData = "<meta name='author' content='thanhit.net'>",
                                        Title = "Công trình thực tế 9 - News ", Keyword = "Công trình thực tế 9 - News Keyword", Description = "Công trình thực tế 9 - News Description",
                                        ThumbUrl = "/Content/images/feat9.jpg", 
                                        CreatedById = userId, CreatedDateTime = DateTime.Now,
                                        ModifiedById = userId, ModifiedDateTime = DateTime.Now
                                    },
                                    new NewsMapping()
                                    {
                                        NewsId = 24, LanguageId = 29, Status = true, Deleted = false,
                                        Intro = "Construction 9 - News - Intro", Detail = "Construction 9- News - Detail" + detail, MetaData = "<meta name='author' content='thanhit.net'>",
                                        Title = "Construction 9 - News", Keyword = "Construction 9 - News Keyword", Description = "Construction 9 - News Description",
                                        ThumbUrl = "/Content/images/feat9.jpg", 
                                        CreatedById = userId, CreatedDateTime = DateTime.Now,
                                        ModifiedById = userId, ModifiedDateTime = DateTime.Now
                                    }
                                }
                            },
                            new News()
                            {
                                Name = "Công trình thực tế 10 - News", Status = true, Deleted = false, ZOrder = 10,  NewsCategoryId = null,  ListTagId = "",  
                                CreatedById = userId, CreatedDateTime = DateTime.Now,
                                ModifiedById = userId, ModifiedDateTime = DateTime.Now,
                                NewsMappings = new List<NewsMapping>()
                                {
                                    new NewsMapping()
                                    {
                                        NewsId = 25, LanguageId = 129, Status = true, Deleted = false,
                                        Intro = "Công trình thực tế 10 - News - Intro", Detail = "Công trình thực tế 10 - News - Detail" + detail, MetaData = "<meta name='author' content='thanhit.net'>",
                                        Title = "Công trình thực tế 10 - News ", Keyword = "Công trình thực tế 10 - News Keyword", Description = "Công trình thực tế 10 - News Description",
                                        ThumbUrl = "/Content/images/feat10.jpg",
                                        CreatedById = userId, CreatedDateTime = DateTime.Now,
                                        ModifiedById = userId, ModifiedDateTime = DateTime.Now
                                    },
                                    new NewsMapping()
                                    {
                                        NewsId = 25, LanguageId = 29, Status = true, Deleted = false,
                                        Intro = "Construction 10 - News - Intro", Detail = "Construction 10- News - Detail" + detail, MetaData = "<meta name='author' content='thanhit.net'>",
                                        Title = "Construction 10 - News", Keyword = "Construction 10 - News Keyword", Description = "Construction 10 - News Description",
                                        ThumbUrl = "/Content/images/feat10.jpg",
                                        CreatedById = userId, CreatedDateTime = DateTime.Now,
                                        ModifiedById = userId, ModifiedDateTime = DateTime.Now
                                    }
                                }
                            },
                            new News()
                            {
                                Name = "Công trình thực tế 11 - News", Status = true, Deleted = false, ZOrder = 11,  NewsCategoryId = null,  ListTagId = "", 
                                CreatedById = userId, CreatedDateTime = DateTime.Now,
                                ModifiedById = userId, ModifiedDateTime = DateTime.Now,
                                NewsMappings = new List<NewsMapping>()
                                {
                                    new NewsMapping()
                                    {
                                        NewsId = 26, LanguageId = 129, Status = true, Deleted = false,
                                        Intro = "Công trình thực tế 11 - News - Intro", Detail = "Công trình thực tế 11 - News - Detail" + detail, MetaData = "<meta name='author' content='thanhit.net'>",
                                        Title = "Công trình thực tế 11 - News ", Keyword = "Công trình thực tế 11 - News Keyword", Description = "Công trình thực tế 11 - News Description",
                                        ThumbUrl = "/Content/images/feat11.jpg", 
                                        CreatedById = userId, CreatedDateTime = DateTime.Now,
                                        ModifiedById = userId, ModifiedDateTime = DateTime.Now
                                    },
                                    new NewsMapping()
                                    {
                                        NewsId = 26, LanguageId = 29, Status = true, Deleted = false,
                                        Intro = "Construction 11 - News - Intro", Detail = "Construction 11- News - Detail" + detail, MetaData = "<meta name='author' content='thanhit.net'>",
                                        Title = "Construction 11 - News", Keyword = "Construction 11 - News Keyword", Description = "Construction 11 - News Description",
                                        ThumbUrl = "/Content/images/feat11.jpg", 
                                        CreatedById = userId, CreatedDateTime = DateTime.Now,
                                        ModifiedById = userId, ModifiedDateTime = DateTime.Now
                                    }
                                }
                            },
                            new News()
                            {
                                Name = "Công trình thực tế 12 - News", Status = true, Deleted = false, ZOrder = 12,  NewsCategoryId = null,  ListTagId = "", 
                                CreatedById = userId, CreatedDateTime = DateTime.Now,
                                ModifiedById = userId, ModifiedDateTime = DateTime.Now,
                                NewsMappings = new List<NewsMapping>()
                                {
                                    new NewsMapping()
                                    {
                                        NewsId = 27, LanguageId = 129, Status = true, Deleted = false,
                                        Intro = "Công trình thực tế 12 - News - Intro", Detail = "Công trình thực tế 12 - News - Detail" + detail, MetaData = "<meta name='author' content='thanhit.net'>",
                                        Title = "Công trình thực tế 12 - News ", Keyword = "Công trình thực tế 12 - News Keyword", Description = "Công trình thực tế 12 - News Description",
                                        ThumbUrl = "/Content/images/feat12.jpg", 
                                        CreatedById = userId, CreatedDateTime = DateTime.Now,
                                        ModifiedById = userId, ModifiedDateTime = DateTime.Now
                                    },
                                    new NewsMapping()
                                    {
                                        NewsId = 27, LanguageId = 29, Status = true, Deleted = false,
                                        Intro = "Construction 12 - News - Intro", Detail = "Construction 12- News - Detail" + detail, MetaData = "<meta name='author' content='thanhit.net'>",
                                        Title = "Construction 12 - News", Keyword = "Construction 12 - News Keyword", Description = "Construction 12 - News Description",
                                        ThumbUrl = "/Content/images/feat12.jpg", 
                                        CreatedById = userId, CreatedDateTime = DateTime.Now,
                                        ModifiedById = userId, ModifiedDateTime = DateTime.Now
                                    }
                                }
                            },
                            new News()
                            {
                                Name = "Công trình thực tế 13 - News", Status = true, Deleted = false, ZOrder = 13,  NewsCategoryId = null,  ListTagId = "", 
                                CreatedById = userId, CreatedDateTime = DateTime.Now,
                                ModifiedById = userId, ModifiedDateTime = DateTime.Now,
                                NewsMappings = new List<NewsMapping>()
                                {
                                    new NewsMapping()
                                    {
                                        NewsId = 28, LanguageId = 129, Status = true, Deleted = false,
                                        Intro = "Công trình thực tế 13 - News - Intro", Detail = "Công trình thực tế 13 - News - Detail" + detail, MetaData = "<meta name='author' content='thanhit.net'>",
                                        Title = "Công trình thực tế 13 - News ", Keyword = "Công trình thực tế 13 - News Keyword", Description = "Công trình thực tế 13 - News Description",
                                        ThumbUrl = "/Content/images/feat12.jpg", 
                                        CreatedById = userId, CreatedDateTime = DateTime.Now,
                                        ModifiedById = userId, ModifiedDateTime = DateTime.Now
                                    },
                                    new NewsMapping()
                                    {
                                        NewsId = 28, LanguageId = 29, Status = true, Deleted = false,
                                        Intro = "Construction 13 - News - Intro", Detail = "Construction 13- News - Detail" + detail, MetaData = "<meta name='author' content='thanhit.net'>",
                                        Title = "Construction 13 - News", Keyword = "Construction 13 - News Keyword", Description = "Construction 13 - News Description",
                                        ThumbUrl = "/Content/images/feat12.jpg", 
                                        CreatedById = userId, CreatedDateTime = DateTime.Now,
                                        ModifiedById = userId, ModifiedDateTime = DateTime.Now
                                    }
                                }
                            }
                        }
                    }
                };

                projectList.ForEach(m => context.Projects.AddOrUpdate(p => p.Id, m));

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

            #region menu
            if (context.GroupControls.Count() == 0)
            {
                var userId = UserManager.FindByName("Administrator").Id;
                var groupControlList = new List<GroupControl>()
                {
                    //Main menu
                    new GroupControl() { 
                        Name = "Menu", Type = "Menu", Code = "", Parent = 0, Status = true, Deleted = false, ZOrder = 1,
                        CreatedById = userId, CreatedDateTime = DateTime.Now,
                        ModifiedById = userId, ModifiedDateTime = DateTime.Now,
                        Controls = new List<Control>()
                        {
                            new Control()
                            {
                                GroupControlId = 1, Name = "Trang chủ", Url = "/", Parent = 0, Image = "", Description = "", ZOrder = 1, Status = true, Deleted = false,
                                MenuController = "Home", MenuAction = "Index", MenuId = 0,
                                CreatedById = userId, CreatedDateTime = DateTime.Now,
                                ModifiedById = userId, ModifiedDateTime = DateTime.Now,
                                ControlMappings = new List<ControlMapping>()
                                {
                                    new ControlMapping()
                                    {
                                        ControlId = 1, LanguageId = 129, Status = true, Deleted = false, Text = "Trang chủ", 
                                        CreatedById = userId, CreatedDateTime = DateTime.Now,
                                        ModifiedById = userId, ModifiedDateTime = DateTime.Now
                                    },
                                    new ControlMapping()
                                    {
                                        ControlId = 1, LanguageId = 29, Status = true, Deleted = false, Text = "Home",
                                        CreatedById = userId, CreatedDateTime = DateTime.Now,
                                        ModifiedById = userId, ModifiedDateTime = DateTime.Now
                                    }
                                }
                            },
                            new Control()
                            {
                                GroupControlId = 1, Name = "Giới thiệu", Url = "/gioi-thieu", Parent = 0, Image = "", Description = "", ZOrder = 2, Status = true, Deleted = false,
                                MenuController = "Home", MenuAction = "About", MenuId = 0,
                                CreatedById = userId, CreatedDateTime = DateTime.Now,
                                ModifiedById = userId, ModifiedDateTime = DateTime.Now,
                                 ControlMappings = new List<ControlMapping>()
                                {
                                    new ControlMapping()
                                    {
                                        ControlId = 2, LanguageId = 129, Status = true, Deleted = false, Text = "Giới thiệu", 
                                        CreatedById = userId, CreatedDateTime = DateTime.Now,
                                        ModifiedById = userId, ModifiedDateTime = DateTime.Now
                                    },
                                    new ControlMapping()
                                    {
                                        ControlId = 2, LanguageId = 29, Status = true, Deleted = false, Text = "About us",
                                        CreatedById = userId, CreatedDateTime = DateTime.Now,
                                        ModifiedById = userId, ModifiedDateTime = DateTime.Now
                                    }
                                }
                            },
                            new Control()
                            {
                                GroupControlId = 1, Name = "Dự án", Url = "/du-an", Parent = 0, Image = "", Description = "", ZOrder = 3, Status = true, Deleted = false,
                                MenuController = "Project", MenuAction = "Index, Detail, List", MenuId = 0,
                                CreatedById = userId, CreatedDateTime = DateTime.Now,
                                ModifiedById = userId, ModifiedDateTime = DateTime.Now,
                                 ControlMappings = new List<ControlMapping>()
                                {
                                    new ControlMapping()
                                    {
                                        ControlId = 3, LanguageId = 129, Status = true, Deleted = false, Text = "Dự án", 
                                        CreatedById = userId, CreatedDateTime = DateTime.Now,
                                        ModifiedById = userId, ModifiedDateTime = DateTime.Now
                                    },
                                    new ControlMapping()
                                    {
                                        ControlId = 3, LanguageId = 29, Status = true, Deleted = false, Text = "Projects",
                                        CreatedById = userId, CreatedDateTime = DateTime.Now,
                                        ModifiedById = userId, ModifiedDateTime = DateTime.Now
                                    }
                                }
                            },
                            new Control()
                            {
                                GroupControlId = 1, Name = "Video", Url = "/video-clip", Parent = 0, Image = "", Description = "", ZOrder = 4, Status = true, Deleted = false,
                                MenuController = "Video", MenuAction = "Index, List", MenuId = 0,
                                CreatedById = userId, CreatedDateTime = DateTime.Now,
                                ModifiedById = userId, ModifiedDateTime = DateTime.Now,
                                 ControlMappings = new List<ControlMapping>()
                                {
                                    new ControlMapping()
                                    {
                                        ControlId = 4, LanguageId = 129, Status = true, Deleted = false, Text = "Video", 
                                        CreatedById = userId, CreatedDateTime = DateTime.Now,
                                        ModifiedById = userId, ModifiedDateTime = DateTime.Now
                                    },
                                    new ControlMapping()
                                    {
                                        ControlId = 4, LanguageId = 29, Status = true, Deleted = false, Text = "Video",
                                        CreatedById = userId, CreatedDateTime = DateTime.Now,
                                        ModifiedById = userId, ModifiedDateTime = DateTime.Now
                                    }
                                }
                            },
                            new Control()
                            {
                                GroupControlId = 1, Name = "Công trình thực tế", Url = "/cong-trinh-thuc-te", Parent = 0, Image = "", Description = "", ZOrder = 5, Status = true, Deleted = false,
                                MenuController = "Construction", MenuAction = "Index, Detail, List", MenuId = 0,
                                CreatedById = userId, CreatedDateTime = DateTime.Now,
                                ModifiedById = userId, ModifiedDateTime = DateTime.Now,
                                 ControlMappings = new List<ControlMapping>()
                                {
                                    new ControlMapping()
                                    {
                                        ControlId = 5, LanguageId = 129, Status = true, Deleted = false, Text = "Công trình thực tế", 
                                        CreatedById = userId, CreatedDateTime = DateTime.Now,
                                        ModifiedById = userId, ModifiedDateTime = DateTime.Now
                                    },
                                    new ControlMapping()
                                    {
                                        ControlId = 5, LanguageId = 29, Status = true, Deleted = false, Text = "Constructions",
                                        CreatedById = userId, CreatedDateTime = DateTime.Now,
                                        ModifiedById = userId, ModifiedDateTime = DateTime.Now
                                    }
                                }
                            },
                            new Control()
                            {
                                GroupControlId = 1, Name = "Liên hệ", Url = "/lien-he", Parent = 0, Image = "", Description = "", ZOrder = 6, Status = true, Deleted = false,
                                MenuController = "Home", MenuAction = "Contact", MenuId = 0,
                                CreatedById = userId, CreatedDateTime = DateTime.Now,
                                ModifiedById = userId, ModifiedDateTime = DateTime.Now,
                                 ControlMappings = new List<ControlMapping>()
                                {
                                    new ControlMapping()
                                    {
                                        ControlId = 6, LanguageId = 129, Status = true, Deleted = false, Text = "Liên hệ", 
                                        CreatedById = userId, CreatedDateTime = DateTime.Now,
                                        ModifiedById = userId, ModifiedDateTime = DateTime.Now
                                    },
                                    new ControlMapping()
                                    {
                                        ControlId = 6, LanguageId = 29, Status = true, Deleted = false, Text = "Contact",
                                        CreatedById = userId, CreatedDateTime = DateTime.Now,
                                        ModifiedById = userId, ModifiedDateTime = DateTime.Now
                                    }
                                }
                            }
                        }
                    }
                };

                groupControlList.ForEach(m => context.GroupControls.AddOrUpdate(p => p.Id, m));

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

            #region News
            if (context.NewsCategories.Count() == 0)
            {
                var userId = UserManager.FindByName("Administrator").Id;
                var newsCategoryList = new List<NewsCategory>()
                {
                    //Main menu
                    new NewsCategory() { 
                        Name = "Xu Hướng", Parent = 0, Status = true, Deleted = false, ZOrder = 1, 
                        CreatedById = userId, CreatedDateTime = DateTime.Now,
                        ModifiedById = userId, ModifiedDateTime = DateTime.Now,
                        NewsCategoryMappings = new List<NewsCategoryMapping>()
                        {
                            new NewsCategoryMapping()
                            {
                                NewsCategoryId = 1, LanguageId = 129, Status = true, Deleted = false,
                                Intro = "Xu Hướng 1 - NewsCategory - Intro", Detail = "Xu Hướng 1 - NewsCategory - Detail", MetaData = "<meta name='author' content='thanhit.net'>",
                                Title = "Xu Hướng", Keyword = "Xu Hướng 1 - NewsCategory Keyword", Description = "Xu Hướng 1 - NewsCategory Description",
                                CreatedById = userId, CreatedDateTime = DateTime.Now,
                                ModifiedById = userId, ModifiedDateTime = DateTime.Now
                            },
                            new NewsCategoryMapping()
                            {
                                NewsCategoryId = 1, LanguageId = 29, Status = true, Deleted = false,
                                Intro = "Trend 1 - NewsCategory - Intro", Detail = "Trend 1 - NewsCategory - Detail", MetaData = "<meta name='author' content='thanhit.net'>",
                                Title = "Trend 1", Keyword = "Trend 1 - NewsCategory Keyword", Description = "Trend 1 - NewsCategory Description",
                                CreatedById = userId, CreatedDateTime = DateTime.Now,
                                ModifiedById = userId, ModifiedDateTime = DateTime.Now
                            }
                        },
                        Newses = new List<News>()
                        {
                            new News()
                            {
                                NewsCategoryId = 1, Name = "Xu Hướng 1", Status = true, Deleted = false, ListTagId = "",  ZOrder = 1,
                                CreatedById = userId, CreatedDateTime = DateTime.Now,
                                ModifiedById = userId, ModifiedDateTime = DateTime.Now,
                                NewsMappings = new List<NewsMapping>()
                                {
                                    new NewsMapping()
                                    {
                                        NewsId = 29, LanguageId = 129, Status = true, Deleted = false,
                                        Intro = "Xu Hướng 1 - News - Intro", Detail = "Xu Hướng 1 - News - Detail" + detail, MetaData = "<meta name='author' content='thanhit.net'>",
                                        Title = "Xu Hướng 1 - News ", Keyword = "Xu Hướng 1 - News Keyword", Description = "Xu Hướng 1 - News Description",
                                        ThumbUrl = "/Content/images/feat1.jpg",
                                        CreatedById = userId, CreatedDateTime = DateTime.Now,
                                        ModifiedById = userId, ModifiedDateTime = DateTime.Now
                                    },
                                    new NewsMapping()
                                    {
                                        NewsId = 29, LanguageId = 29, Status = true, Deleted = false,
                                        Intro = "Trend 1 - News - Intro", Detail = "Trend 1 - News - Detail" + detail, MetaData = "<meta name='author' content='thanhit.net'>",
                                        Title = "Trend 1 - News", Keyword = "Trend 1 - News Keyword", Description = "Trend 1 - News Description",
                                        ThumbUrl = "/Content/images/feat1.jpg",
                                        CreatedById = userId, CreatedDateTime = DateTime.Now,
                                        ModifiedById = userId, ModifiedDateTime = DateTime.Now
                                    }
                                }
                            },
                            new News()
                            {
                                NewsCategoryId = 1, Name = "Xu Hướng 2", Status = true, Deleted = false, ListTagId = "",   ZOrder = 2,
                                CreatedById = userId, CreatedDateTime = DateTime.Now,
                                ModifiedById = userId, ModifiedDateTime = DateTime.Now,
                                NewsMappings = new List<NewsMapping>()
                                {
                                    new NewsMapping()
                                    {
                                        NewsId = 30, LanguageId = 129, Status = true, Deleted = false,
                                        Intro = "Xu Hướng 2 - News - Intro", Detail = "Xu Hướng 2 - News - Detail" + detail, MetaData = "<meta name='author' content='thanhit.net'>",
                                        Title = "Xu Hướng 2 - News ", Keyword = "Xu Hướng 2 - News Keyword", Description = "Xu Hướng 2 - News Description",
                                        ThumbUrl = "/Content/images/feat2.jpg",
                                        CreatedById = userId, CreatedDateTime = DateTime.Now,
                                        ModifiedById = userId, ModifiedDateTime = DateTime.Now
                                    },
                                    new NewsMapping()
                                    {
                                        NewsId = 30, LanguageId = 29, Status = true, Deleted = false,
                                        Intro = "Trend 2 - News - Intro", Detail = "Trend 2 - News - Detail" + detail, MetaData = "<meta name='author' content='thanhit.net'>",
                                        Title = "Trend 2 - News", Keyword = "Trend 2 - News Keyword", Description = "Trend 2 - News Description",
                                        ThumbUrl = "/Content/images/feat2.jpg",
                                        CreatedById = userId, CreatedDateTime = DateTime.Now,
                                        ModifiedById = userId, ModifiedDateTime = DateTime.Now
                                    }
                                }
                            },
                            new News()
                            {
                                NewsCategoryId = 1, Name = "Xu hướng 3", Status = true, Deleted = false, ListTagId = "",  ZOrder = 3,
                                CreatedById = userId, CreatedDateTime = DateTime.Now,
                                ModifiedById = userId, ModifiedDateTime = DateTime.Now,
                                NewsMappings = new List<NewsMapping>()
                                {
                                    new NewsMapping()
                                    {
                                        NewsId = 31, LanguageId = 129, Status = true, Deleted = false,
                                        Intro = "Xu hướng 3 - News - Intro", Detail = "Xu hướng 3 - News - Detail" + detail, MetaData = "<meta name='author' content='thanhit.net'>",
                                        Title = "Xu hướng 3 - News ", Keyword = "Xu hướng 3 - News Keyword", Description = "Xu hướng 3 - News Description",
                                        ThumbUrl = "/Content/images/feat3.jpg",
                                        CreatedById = userId, CreatedDateTime = DateTime.Now,
                                        ModifiedById = userId, ModifiedDateTime = DateTime.Now
                                    },
                                    new NewsMapping()
                                    {
                                        NewsId = 31, LanguageId = 29, Status = true, Deleted = false,
                                        Intro = "Trend 3 - News - Intro", Detail = "Trend 3 - News - Detail" + detail, MetaData = "<meta name='author' content='thanhit.net'>",
                                        Title = "Trend 3 - News", Keyword = "Trend 3 - News Keyword", Description = "Trend 3 - News Description",
                                        ThumbUrl = "/Content/images/feat3.jpg",
                                        CreatedById = userId, CreatedDateTime = DateTime.Now,
                                        ModifiedById = userId, ModifiedDateTime = DateTime.Now
                                    }
                                }
                            },
                            new News()
                            {
                                NewsCategoryId = 1, Name = "Xu hướng 4", Status = true, Deleted = false, ListTagId = "", ZOrder = 4,  
                                CreatedById = userId, CreatedDateTime = DateTime.Now,
                                ModifiedById = userId, ModifiedDateTime = DateTime.Now,
                                NewsMappings = new List<NewsMapping>()
                                {
                                    new NewsMapping()
                                    {
                                        NewsId = 32, LanguageId = 129, Status = true, Deleted = false,
                                        Intro = "Xu hướng 4 - News - Intro", Detail = "Xu hướng 4 - News - Detail" + detail, MetaData = "<meta name='author' content='thanhit.net'>",
                                        Title = "Xu hướng 4 - News ", Keyword = "Xu hướng 4 - News Keyword", Description = "Xu hướng 4 - News Description",
                                        ThumbUrl = "/Content/images/feat4.jpg",
                                        CreatedById = userId, CreatedDateTime = DateTime.Now,
                                        ModifiedById = userId, ModifiedDateTime = DateTime.Now
                                    },
                                    new NewsMapping()
                                    {
                                        NewsId = 32, LanguageId = 29, Status = true, Deleted = false,
                                        Intro = "Trend 4 - News - Intro", Detail = "Trend 4 - News - Detail" + detail, MetaData = "<meta name='author' content='thanhit.net'>",
                                        Title = "Trend 4 - News", Keyword = "Trend 4 - News Keyword", Description = "Trend 4 - News Description",
                                        ThumbUrl = "/Content/images/feat4.jpg",
                                        CreatedById = userId, CreatedDateTime = DateTime.Now,
                                        ModifiedById = userId, ModifiedDateTime = DateTime.Now
                                    }
                                }
                            },
                            new News()
                            {
                                NewsCategoryId = 1, Name = "Xu hướng 5", Status = true, Deleted = false, ListTagId = "", ZOrder = 5,  
                                CreatedById = userId, CreatedDateTime = DateTime.Now,
                                ModifiedById = userId, ModifiedDateTime = DateTime.Now,
                                NewsMappings = new List<NewsMapping>()
                                {
                                    new NewsMapping()
                                    {
                                        NewsId = 33, LanguageId = 129, Status = true, Deleted = false,
                                        Intro = "Xu hướng 5 - News - Intro", Detail = "Xu hướng 5 - News - Detail" + detail, MetaData = "<meta name='author' content='thanhit.net'>",
                                        Title = "Xu hướng 5 - News ", Keyword = "Xu hướng 5 - News Keyword", Description = "Xu hướng 5 - News Description",
                                        ThumbUrl = "/Content/images/feat5.jpg",
                                        CreatedById = userId, CreatedDateTime = DateTime.Now,
                                        ModifiedById = userId, ModifiedDateTime = DateTime.Now
                                    },
                                    new NewsMapping()
                                    {
                                        NewsId = 33, LanguageId = 29, Status = true, Deleted = false,
                                        Intro = "Trend 5 - News - Intro", Detail = "Trend 5 - News - Detail" + detail, MetaData = "<meta name='author' content='thanhit.net'>",
                                        Title = "Trend 5 - News", Keyword = "Trend 5 - News Keyword", Description = "Trend 5 - News Description",
                                        ThumbUrl = "/Content/images/feat5.jpg",
                                        CreatedById = userId, CreatedDateTime = DateTime.Now,
                                        ModifiedById = userId, ModifiedDateTime = DateTime.Now
                                    }
                                }
                            },
                            new News()
                            {
                                NewsCategoryId = 1, Name = "Xu hướng 6", Status = true, Deleted = false, ListTagId = "", ZOrder = 6,  
                                CreatedById = userId, CreatedDateTime = DateTime.Now,
                                ModifiedById = userId, ModifiedDateTime = DateTime.Now,
                                NewsMappings = new List<NewsMapping>()
                                {
                                    new NewsMapping()
                                    {
                                        NewsId = 34, LanguageId = 129, Status = true, Deleted = false,
                                        Intro = "Xu hướng 6 - News - Intro", Detail = "Xu hướng 6 - News - Detail" + detail, MetaData = "<meta name='author' content='thanhit.net'>",
                                        Title = "Xu hướng 6 - News ", Keyword = "Xu hướng 6 - News Keyword", Description = "Xu hướng 6 - News Description",
                                        ThumbUrl = "/Content/images/feat6.jpg",
                                        CreatedById = userId, CreatedDateTime = DateTime.Now,
                                        ModifiedById = userId, ModifiedDateTime = DateTime.Now
                                    },
                                    new NewsMapping()
                                    {
                                        NewsId = 34, LanguageId = 29, Status = true, Deleted = false,
                                        Intro = "Trend 6 - News - Intro", Detail = "Trend 6 - News - Detail" + detail, MetaData = "<meta name='author' content='thanhit.net'>",
                                        Title = "Trend 6 - News", Keyword = "Trend 6 - News Keyword", Description = "Trend 6 - News Description",
                                        ThumbUrl = "/Content/images/feat6.jpg",
                                        CreatedById = userId, CreatedDateTime = DateTime.Now,
                                        ModifiedById = userId, ModifiedDateTime = DateTime.Now
                                    }
                                }
                            },
                            new News()
                            {
                                NewsCategoryId = 1, Name = "Xu hướng 7", Status = true, Deleted = false, ListTagId = "", ZOrder = 7,  
                                CreatedById = userId, CreatedDateTime = DateTime.Now,
                                ModifiedById = userId, ModifiedDateTime = DateTime.Now,
                                NewsMappings = new List<NewsMapping>()
                                {
                                    new NewsMapping()
                                    {
                                        NewsId = 35, LanguageId = 129, Status = true, Deleted = false,
                                        Intro = "Xu hướng 7 - News - Intro", Detail = "Xu hướng 7 - News - Detail" + detail, MetaData = "<meta name='author' content='thanhit.net'>",
                                        Title = "Xu hướng 7 - News ", Keyword = "Xu hướng 7 - News Keyword", Description = "Xu hướng 7 - News Description",
                                        ThumbUrl = "/Content/images/feat7.jpg",
                                        CreatedById = userId, CreatedDateTime = DateTime.Now,
                                        ModifiedById = userId, ModifiedDateTime = DateTime.Now
                                    },
                                    new NewsMapping()
                                    {
                                        NewsId = 35, LanguageId = 29, Status = true, Deleted = false,
                                        Intro = "Trend 7 - News - Intro", Detail = "Trend 7 - News - Detail" + detail, MetaData = "<meta name='author' content='thanhit.net'>",
                                        Title = "Trend 7 - News", Keyword = "Trend 7 - News Keyword", Description = "Trend 7 - News Description",
                                        ThumbUrl = "/Content/images/feat7.jpg",
                                        CreatedById = userId, CreatedDateTime = DateTime.Now,
                                        ModifiedById = userId, ModifiedDateTime = DateTime.Now
                                    }
                                }
                            }
                        }
                    },
                    new NewsCategory() { 
                        Name = "Khách Hàng", Parent = 0, Status = true, Deleted = false, ZOrder = 2, 
                        CreatedById = userId, CreatedDateTime = DateTime.Now,
                        ModifiedById = userId, ModifiedDateTime = DateTime.Now,
                        NewsCategoryMappings = new List<NewsCategoryMapping>()
                        {
                            new NewsCategoryMapping()
                            {
                                NewsCategoryId = 2, LanguageId = 129, Status = true, Deleted = false,
                                Intro = "Khách Hàng - NewsCategory - Intro", Detail = "Khách Hàng - NewsCategory - Detail", MetaData = "<meta name='author' content='thanhit.net'>",
                                Title = "Khách Hàng", Keyword = "Khách Hàng - NewsCategory Keyword", Description = "Khách Hàng - NewsCategory Description",
                                CreatedById = userId, CreatedDateTime = DateTime.Now,
                                ModifiedById = userId, ModifiedDateTime = DateTime.Now
                            },
                            new NewsCategoryMapping()
                            {
                                NewsCategoryId = 2, LanguageId = 29, Status = true, Deleted = false,
                                Intro = "Customers - NewsCategory - Intro", Detail = "Customers - NewsCategory - Detail", MetaData = "<meta name='author' content='thanhit.net'>",
                                Title = "Customers", Keyword = "Customers - NewsCategory Keyword", Description = "Customers - NewsCategory Description",
                                CreatedById = userId, CreatedDateTime = DateTime.Now,
                                ModifiedById = userId, ModifiedDateTime = DateTime.Now
                            }
                        },
                        Newses = new List<News>()
                        {
                            new News()
                            {
                                NewsCategoryId = 2, Name = "Khách Hàng 1", Status = true, Deleted = false, ListTagId = "", ZOrder = 1,  
                                CreatedById = userId, CreatedDateTime = DateTime.Now,
                                ModifiedById = userId, ModifiedDateTime = DateTime.Now,
                                NewsMappings = new List<NewsMapping>()
                                {
                                    new NewsMapping()
                                    {
                                        NewsId = 36, LanguageId = 129, Status = true, Deleted = false,
                                        Intro = "Khách Hàng 1 - News - Intro", Detail = "Khách Hàng 1 - News - Detail" + detail, MetaData = "<meta name='author' content='thanhit.net'>",
                                        Title = "Khách Hàng 1 - News ", Keyword = "Khách Hàng 1 - News Keyword", Description = "Khách Hàng 1 - News Description",
                                        ThumbUrl = "/Content/images/feat1.jpg",
                                        CreatedById = userId, CreatedDateTime = DateTime.Now,
                                        ModifiedById = userId, ModifiedDateTime = DateTime.Now
                                    },
                                    new NewsMapping()
                                    {
                                        NewsId = 36, LanguageId = 29, Status = true, Deleted = false,
                                        Intro = "Customers 1 - News - Intro", Detail = "Customers 1 - News - Detail" + detail, MetaData = "<meta name='author' content='thanhit.net'>",
                                        Title = "Customers 1 - News", Keyword = "Customers 1 - News Keyword", Description = "Customers 1 - News Description",
                                        ThumbUrl = "/Content/images/feat1.jpg",
                                        CreatedById = userId, CreatedDateTime = DateTime.Now,
                                        ModifiedById = userId, ModifiedDateTime = DateTime.Now
                                    }
                                }
                            },
                            new News()
                            {
                                NewsCategoryId = 2, Name = "Khách Hàng 2", Status = true, Deleted = false, ListTagId = "", ZOrder = 2,  
                                CreatedById = userId, CreatedDateTime = DateTime.Now,
                                ModifiedById = userId, ModifiedDateTime = DateTime.Now,
                                NewsMappings = new List<NewsMapping>()
                                {
                                    new NewsMapping()
                                    {
                                        NewsId = 37, LanguageId = 129, Status = true, Deleted = false,
                                        Intro = "Khách Hàng 2 - News - Intro", Detail = "Khách Hàng 2 - News - Detail" + detail, MetaData = "<meta name='author' content='thanhit.net'>",
                                        Title = "Khách Hàng 2 - News ", Keyword = "Khách Hàng 2 - News Keyword", Description = "Khách Hàng 2 - News Description",
                                        ThumbUrl = "/Content/images/feat2.jpg",
                                        CreatedById = userId, CreatedDateTime = DateTime.Now,
                                        ModifiedById = userId, ModifiedDateTime = DateTime.Now
                                    },
                                    new NewsMapping()
                                    {
                                        NewsId = 37, LanguageId = 29, Status = true, Deleted = false,
                                        Intro = "Customers 2 - News - Intro", Detail = "Customers 2 - News - Detail" + detail, MetaData = "<meta name='author' content='thanhit.net'>",
                                        Title = "Customers 2 - News", Keyword = "Customers 2 - News Keyword", Description = "Customers 2 - News Description",
                                        ThumbUrl = "/Content/images/feat2.jpg",
                                        CreatedById = userId, CreatedDateTime = DateTime.Now,
                                        ModifiedById = userId, ModifiedDateTime = DateTime.Now
                                    }
                                }
                            },
                            new News()
                            {
                                NewsCategoryId = 2, Name = "Khách Hàng 3", Status = true, Deleted = false, ListTagId = "", ZOrder = 3,  
                                CreatedById = userId, CreatedDateTime = DateTime.Now,
                                ModifiedById = userId, ModifiedDateTime = DateTime.Now,
                                NewsMappings = new List<NewsMapping>()
                                {
                                    new NewsMapping()
                                    {
                                        NewsId = 38, LanguageId = 129, Status = true, Deleted = false,
                                        Intro = "Khách Hàng 3 - News - Intro", Detail = "Khách Hàng 3 - News - Detail" + detail, MetaData = "<meta name='author' content='thanhit.net'>",
                                        Title = "Khách Hàng 3 - News ", Keyword = "Khách Hàng 3 - News Keyword", Description = "Khách Hàng 3 - News Description",
                                        ThumbUrl = "/Content/images/feat3.jpg",
                                        CreatedById = userId, CreatedDateTime = DateTime.Now,
                                        ModifiedById = userId, ModifiedDateTime = DateTime.Now
                                    },
                                    new NewsMapping()
                                    {
                                        NewsId = 38, LanguageId = 29, Status = true, Deleted = false,
                                        Intro = "Customers 3 - News - Intro", Detail = "Customers 3 - News - Detail" + detail, MetaData = "<meta name='author' content='thanhit.net'>",
                                        Title = "Customers 3 - News", Keyword = "Customers 3 - News Keyword", Description = "Customers 3 - News Description",
                                        ThumbUrl = "/Content/images/feat3.jpg",
                                        CreatedById = userId, CreatedDateTime = DateTime.Now,
                                        ModifiedById = userId, ModifiedDateTime = DateTime.Now
                                    }
                                }
                            },
                            new News()
                            {
                                NewsCategoryId = 2, Name = "Khách Hàng 4", Status = true, Deleted = false, ListTagId = "", ZOrder = 4, 
                                CreatedById = userId, CreatedDateTime = DateTime.Now,
                                ModifiedById = userId, ModifiedDateTime = DateTime.Now,
                                NewsMappings = new List<NewsMapping>()
                                {
                                    new NewsMapping()
                                    {
                                        NewsId = 39, LanguageId = 129, Status = true, Deleted = false,
                                        Intro = "Khách Hàng 4 - News - Intro", Detail = "Khách Hàng 4 - News - Detail" + detail, MetaData = "<meta name='author' content='thanhit.net'>",
                                        Title = "Khách Hàng 4 - News ", Keyword = "Khách Hàng 4 - News Keyword", Description = "Khách Hàng 4 - News Description",
                                        ThumbUrl = "/Content/images/feat4.jpg",
                                        CreatedById = userId, CreatedDateTime = DateTime.Now,
                                        ModifiedById = userId, ModifiedDateTime = DateTime.Now
                                    },
                                    new NewsMapping()
                                    {
                                        NewsId = 39, LanguageId = 29, Status = true, Deleted = false,
                                        Intro = "Customers 4 - News - Intro", Detail = "Customers 4 - News - Detail" + detail, MetaData = "<meta name='author' content='thanhit.net'>",
                                        Title = "Customers 4 - News", Keyword = "Customers 4 - News Keyword", Description = "Customers 4 - News Description",
                                        ThumbUrl = "/Content/images/feat4.jpg",
                                        CreatedById = userId, CreatedDateTime = DateTime.Now,
                                        ModifiedById = userId, ModifiedDateTime = DateTime.Now
                                    }
                                }
                            },
                            new News()
                            {
                                NewsCategoryId = 2, Name = "Khách Hàng 5", Status = true, Deleted = false, ListTagId = "", ZOrder = 5,  
                                CreatedById = userId, CreatedDateTime = DateTime.Now,
                                ModifiedById = userId, ModifiedDateTime = DateTime.Now,
                                NewsMappings = new List<NewsMapping>()
                                {
                                    new NewsMapping()
                                    {
                                        NewsId = 40, LanguageId = 129, Status = true, Deleted = false,
                                        Intro = "Khách Hàng 5 - News - Intro", Detail = "Khách Hàng 5 - News - Detail" + detail, MetaData = "<meta name='author' content='thanhit.net'>",
                                        Title = "Khách Hàng 5 - News ", Keyword = "Khách Hàng 5 - News Keyword", Description = "Khách Hàng 5 - News Description",
                                        ThumbUrl = "/Content/images/feat5.jpg",
                                        CreatedById = userId, CreatedDateTime = DateTime.Now,
                                        ModifiedById = userId, ModifiedDateTime = DateTime.Now
                                    },
                                    new NewsMapping()
                                    {
                                        NewsId = 40, LanguageId = 29, Status = true, Deleted = false,
                                        Intro = "Customers 5 - News - Intro", Detail = "Customers 5 - News - Detail" + detail, MetaData = "<meta name='author' content='thanhit.net'>",
                                        Title = "Customers 5 - News", Keyword = "Customers 5 - News Keyword", Description = "Customers 5 - News Description",
                                        ThumbUrl = "/Content/images/feat5.jpg",
                                        CreatedById = userId, CreatedDateTime = DateTime.Now,
                                        ModifiedById = userId, ModifiedDateTime = DateTime.Now
                                    }
                                }
                            },
                            new News()
                            {
                                NewsCategoryId = 2, Name = "Khách Hàng 6", Status = true, Deleted = false, ListTagId = "", ZOrder = 6,  
                                CreatedById = userId, CreatedDateTime = DateTime.Now,
                                ModifiedById = userId, ModifiedDateTime = DateTime.Now,
                                NewsMappings = new List<NewsMapping>()
                                {
                                    new NewsMapping()
                                    {
                                        NewsId = 41, LanguageId = 129, Status = true, Deleted = false,
                                        Intro = "Khách Hàng 6 - News - Intro", Detail = "Khách Hàng 6 - News - Detail" + detail, MetaData = "<meta name='author' content='thanhit.net'>",
                                        Title = "Khách Hàng 6 - News ", Keyword = "Khách Hàng 6 - News Keyword", Description = "Khách Hàng 6 - News Description",
                                        ThumbUrl = "/Content/images/feat6.jpg",
                                        CreatedById = userId, CreatedDateTime = DateTime.Now,
                                        ModifiedById = userId, ModifiedDateTime = DateTime.Now
                                    },
                                    new NewsMapping()
                                    {
                                        NewsId = 41, LanguageId = 29, Status = true, Deleted = false,
                                        Intro = "Customers 6 - News - Intro", Detail = "Customers 6 - News - Detail" + detail, MetaData = "<meta name='author' content='thanhit.net'>",
                                        Title = "Customers 6 - News", Keyword = "Customers 6 - News Keyword", Description = "Customers 6 - News Description",
                                        ThumbUrl = "/Content/images/feat6.jpg",
                                        CreatedById = userId, CreatedDateTime = DateTime.Now,
                                        ModifiedById = userId, ModifiedDateTime = DateTime.Now
                                    }
                                }
                            },
                            new News()
                            {
                                NewsCategoryId = 2, Name = "Khách Hàng 7", Status = true, Deleted = false, ListTagId = "", ZOrder = 7,  
                                CreatedById = userId, CreatedDateTime = DateTime.Now,
                                ModifiedById = userId, ModifiedDateTime = DateTime.Now,
                                NewsMappings = new List<NewsMapping>()
                                {
                                    new NewsMapping()
                                    {
                                        NewsId = 42, LanguageId = 129, Status = true, Deleted = false,
                                        Intro = "Khách Hàng 7 - News - Intro", Detail = "Khách Hàng 7 - News - Detail" + detail, MetaData = "<meta name='author' content='thanhit.net'>",
                                        Title = "Khách Hàng 7 - News ", Keyword = "Khách Hàng 7 - News Keyword", Description = "Khách Hàng 7 - News Description",
                                        ThumbUrl = "/Content/images/feat7.jpg",
                                        CreatedById = userId, CreatedDateTime = DateTime.Now,
                                        ModifiedById = userId, ModifiedDateTime = DateTime.Now
                                    },
                                    new NewsMapping()
                                    {
                                        NewsId = 42, LanguageId = 29, Status = true, Deleted = false,
                                        Intro = "Customers 7 - News - Intro", Detail = "Customers 7 - News - Detail" + detail, MetaData = "<meta name='author' content='thanhit.net'>",
                                        Title = "Customers 7 - News", Keyword = "Customers 7 - News Keyword", Description = "Customers 7 - News Description",
                                        ThumbUrl = "/Content/images/feat7.jpg",
                                        CreatedById = userId, CreatedDateTime = DateTime.Now,
                                        ModifiedById = userId, ModifiedDateTime = DateTime.Now
                                    }
                                }
                            }
                        }
                    }
                };

                newsCategoryList.ForEach(m => context.NewsCategories.AddOrUpdate(p => p.Id, m));

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

            #region CONFIG
            var contactForm = "<h3 class='head1'>Find Us</h3>";
            contactForm += "<p>You can always get 24/7 support for all <span class='col1'><a href='#' rel='nofollow'>premium website templates</a></span> from <br>TemplateMonster. Free themes go without it. </p>";
            contactForm += "<p><span class='col1'><a href='#' rel='nofollow'>TemplateTuning</a></span> will help you with any questions regarding customization of the chosen themes.</p>";
            contactForm += "<p>";
            contactForm += "The Company Name Inc. <br>";
            contactForm += "8901 Marmora Road, <br>";
            contactForm += "Glasgow, D04 89GR.";
            contactForm += "</p>";
            contactForm += "Freephone:+1 800 559 6580 <br>";
            contactForm += "Telephone:+1 800 603 6035<br>";
            contactForm += "FAX:+1 800 889 9898<br>";
            contactForm += "E-mail: <a href='#' class='col1'>mail@thanhit.net</a>";

            var slogan = "<h2>Welcome!</h2>";
            slogan += "Our company offers you the best design solutions to make your home interior unique and stylish";


            var footerContent = "Địa chỉ: 141 Gò Ô Môi, Phường Phú Thuận, Quận 7, TP.HCM";
            footerContent += "<br/>";
            footerContent += "Điện thoại: 08 3774 0218";
            footerContent += "<br/>";
            footerContent += "PenDesign &copy; 2015 | <a href='#'>Privacy Policy</a> <br> Website designed by <a href='http://www.thanhit.net/' rel='nofollow'>thanhit.net </a>";

            if (context.Configs.Count() == 0)
            {
                var config = new Config()
                {
                    CompanyName = "CÔNG TY TNHH PenDesign",
                    About = "CÔNG TY TNHH PenDesign",
                    Slogan = slogan,
                    Email = "mrthanh.testemail@gmail.com",
                    EmailPassword = "kt123456789",
                    EmailPort = 587,
                    EmailSignature = "<b>Mr.Thanh - Web Deverloper<br />Website: www.thanhit.net<br />Phone: 099.661.4884 - 0938.039.131</b>",
                    CCEmail = "pendesignteam@gmail.com",
                    //for SEO
                    GoogleAnalytics = "<script>var google = 'googleAnalytics';</script>",
                    FacebookRetager = "<script>facebook<script>",
                    GoogleWebMaster = "<meta name='google-site-verification' content='CqoI2eXvbapz6SW47AALzXpx4_ozI3mZb13QBP541d4' />",

                    Address = "141 Gò Ô Môi, Phường Phú Thuận, Quận 7, TP.HCM",
                    GoogleMap = "<iframe class='embed-responsive-item' src='https://www.google.com/maps/embed?pb=!1m18!1m12!1m3!1d3920.060876544128!2d106.7358477!3d10.729788200000005!2m3!1f0!2f0!3f0!3m2!1i1024!2i768!4f13.1!3m3!1m2!1s0x3175256f79e66e01%3A0xf3843eee25fb1859!2zMTQxIEfDsiDDlCBNw7RpLCBQaMO6IFRodeG6rW4sIFF14bqtbiA3LCBI4buTIENow60gTWluaCwgVmnhu4d0IE5hbQ!5e0!3m2!1svi!2s!4v1441451785499' frameborder='0' style='border:0' allowfullscreen></iframe>",
                    Phone = "0996614884",
                    LogoUrl = "/Content/images/logo.png",
                    ContactForm = contactForm,
                    FooterContent = footerContent,
                    Yahoo = "lonely_start10882000@yahoo.com",
                    Skype = "mrthanh.kientao",
                    FacebookSocial = "https://www.facebook.com/pages/Pendesign/1578482475737329?ref=hl",
                    GoogleSocial = "www.google.com",
                    TwitterSocial = "https://twitter.com/pendesignteam",
                    PicasaSocial = "",
                    Youtube = "www.youtube.com",
                    Instagram = ""
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
                        Name = "Banner", Url = "#", CssIcon =  "fa fa-folder-open", LabelCss =  "fa fa-angle-left", 
                        Type = "", Parent = 0, Order = 3, Available = true,
                        IsAdmin = true
                    },
                    new AdminMenu() { 
                        Name = "Nhóm/Thành viên", Url = "#", CssIcon =  "fa fa-user-md", LabelCss =  "fa fa-angle-left", 
                        Type = "", Parent = 0, Order = 4, Available = true,
                        IsAdmin = true
                    },
                    new AdminMenu() { 
                        Name = "Xu hướng", Url = "#", CssIcon =  "fa fa-rocket", LabelCss =  "fa fa-angle-left", 
                        Type = "", Parent = 0, Order = 5, Available = true,
                        IsAdmin = true
                    },
                    new AdminMenu() { 
                        Name = "Khách hàng", Url = "#", CssIcon =  "fa fa-rocket", LabelCss =  "fa fa-angle-left", 
                        Type = "", Parent = 0, Order = 6, Available = true,
                        IsAdmin = true
                    },
                    new AdminMenu() { 
                        Name = "Dự án", Url = "#", CssIcon =  "fa fa-file-text", LabelCss =  "fa fa-angle-left", 
                        Type = "", Parent = 0, Order = 7, Available = true,
                        IsAdmin = true
                    },
                    new AdminMenu() { 
                        Name = "Công trình thực tế", Url = "#", CssIcon =  "fa fa-glass", LabelCss =  "fa fa-angle-left", 
                        Type = "", Parent = 0, Order = 8, Available = true,
                        IsAdmin = true
                    },
                    new AdminMenu() { 
                        Name = "Video", Url = "#", CssIcon =  "fa fa-group", LabelCss =  "fa fa-angle-left", 
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
                    //SubMenu Banner
                    //--------------
                    new AdminMenu() { 
                        Name = "Thêm Banner", Url = "controlPanel.addBanner", CssIcon = "fa fa-users", LabelCss = "", 
                        Type = "", Parent = 1, Order = 1, Available = true,
                        IsAdmin = true
                    },
                    new AdminMenu() { 
                        Name = "Danh sách Banner", Url = "controlPanel.bannerList", CssIcon = "fa fa-user", LabelCss = "", 
                        Type = "", Parent = 1, Order = 2, Available = true,
                        IsAdmin = true
                    },
                    //--------------
                    //SubMenu Nhom/Thanh vien
                    //--------------
                    new AdminMenu() { 
                        Name = "Danh sách Thành viên", Url = "controlPanel.userList", CssIcon = "fa fa-user", LabelCss = "", 
                        Type = "", Parent = 2, Order = 2, Available = true,
                        IsAdmin = true
                    },
 
                    //--------------
                    //SubMenu Tin tuc
                    //--------------
                    new AdminMenu() { 
                        Name = "Thêm bài mới", Url = "controlPanel.addNews({newsCategoryId: 1})", CssIcon = "fa fa-plus-circle", LabelCss = "", 
                        Type = "", Parent = 3, Order = 1, Available = true,
                        IsAdmin = true
                    },
                    new AdminMenu() { 
                        Name = "Danh sách Bài viết", Url = "controlPanel.newsList({newsCategoryId: 1})", CssIcon = "fa fa-list-ol", LabelCss = "", 
                        Type = "", Parent = 3, Order = 2, Available = true,
                        IsAdmin = true
                    },

                    new AdminMenu() { 
                        Name = "Thêm bài mới", Url = "controlPanel.addNews({newsCategoryId: 2})", CssIcon = "fa fa-plus-circle", LabelCss = "", 
                        Type = "", Parent = 4, Order = 1, Available = true,
                        IsAdmin = true
                    },
                    new AdminMenu() { 
                        Name = "Danh sách Bài viết", Url = "controlPanel.newsList({newsCategoryId: 2})", CssIcon = "fa fa-list-ol", LabelCss = "", 
                        Type = "", Parent = 4, Order = 2, Available = true,
                        IsAdmin = true
                    },
                    //--------------
                    //SubMenu Du An
                    //--------------
                    new AdminMenu() { 
                        Name = "Thêm Dự án mới", Url = "controlPanel.addProject", CssIcon = "fa fa-plus-circle", LabelCss = "", 
                        Type = "", Parent = 5, Order = 1, Available = true,
                        IsAdmin = true
                    },
                    new AdminMenu() { 
                        Name = "Danh sách Dự án", Url = "controlPanel.projectList", CssIcon = "fa fa-list-ol", LabelCss = "", 
                        Type = "", Parent = 5, Order = 2, Available = true,
                        IsAdmin = true
                    },
                    //--------------
                    //SubMenu Cong trinh thuc te
                    //--------------
                    new AdminMenu() { 
                        Name = "Thêm Công trình thực tế mới", Url = "controlPanel.addConstruction", CssIcon = "fa fa-plus-circle", LabelCss = "", 
                        Type = "", Parent = 6, Order = 1, Available = true,
                        IsAdmin = true
                    },
                    new AdminMenu() { 
                        Name = "Danh sách Công trình thực tế", Url = "controlPanel.constructionList", CssIcon = "fa fa-list-ol", LabelCss = "", 
                        Type = "", Parent = 6, Order = 2, Available = true,
                        IsAdmin = true
                    },
                    //--------------
                    //SubMenu Video
                    //--------------
                    new AdminMenu() { 
                        Name = "Thêm video mới", Url = "controlPanel.addVideo", CssIcon = "fa fa-plus-circle", LabelCss = "", 
                        Type = "", Parent = 7, Order = 1, Available = true,
                        IsAdmin = true
                    },
                    new AdminMenu() { 
                        Name = "Danh sách Video", Url = "controlPanel.videoList", CssIcon = "fa fa-list-ol", LabelCss = "", 
                        Type = "", Parent = 7, Order = 2, Available = true,
                        IsAdmin = true
                    },
                    //--------------
                    //SubMenu Email
                    //--------------
                    //new AdminMenu() { 
                    //    Name = "Email đăng ký", Url = "controlPanel.email", CssIcon = "fa fa-list-ol", LabelCss = "", 
                    //    Type = "", Parent = 10, Order = 1, Available = true,
                    //    IsAdmin = true
                    //},
                    new AdminMenu() { 
                        Name = "Email liên hệ", Url = "controlPanel.contactEmail", CssIcon = "fa fa-list-ol", LabelCss = "", 
                        Type = "", Parent = 8, Order = 2, Available = true,
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

            #region OtherPageSEO
            if (context.OtherPageSeos.Count() == 0)
            {
                var otherPageSEOList = new List<OtherPageSEO>()
                {
                    new OtherPageSEO()
                    {
                        Page = "HomePage",
                        Keyword = "Homepage keyword",
                        Description = "Homepage Description",
                        MetaData = "<meta name='author' content='thanhit.net'>"
                    },
                    new OtherPageSEO()
                    {
                        Page = "About",
                        Keyword = "About keyword",
                        Description = "About Description",
                        MetaData = "<meta name='author' content='thanhit.net'>"
                    },
                    new OtherPageSEO()
                    {
                        Page = "Contact",
                        Keyword = "Contact keyword",
                        Description = "Contact Description",
                        MetaData = "<meta name='author' content='thanhit.net'>"
                    },
                    new OtherPageSEO()
                    {
                        Page = "Project",
                        Keyword = "Project keyword",
                        Description = "Project Description",
                        MetaData = "<meta name='author' content='thanhit.net'>"
                    },
                    new OtherPageSEO()
                    {
                        Page = "Construction",
                        Keyword = "Construction keyword",
                        Description = "Construction Description",
                        MetaData = "<meta name='author' content='thanhit.net'>"
                    },
                    new OtherPageSEO()
                    {
                        Page = "Video",
                        Keyword = "Video keyword",
                        Description = "Video Description",
                        MetaData = "<meta name='author' content='thanhit.net'>"
                    }
                };

                otherPageSEOList.ForEach(m => context.OtherPageSeos.AddOrUpdate(p => p.Id, m));

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