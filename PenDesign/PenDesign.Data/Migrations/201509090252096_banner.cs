namespace PenDesign.Data.MigrationConfiguration
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class banner : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AdminMenus",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Url = c.String(),
                        CssIcon = c.String(),
                        LabelCss = c.String(),
                        Type = c.String(),
                        Parent = c.Int(nullable: false),
                        Order = c.Int(nullable: false),
                        Available = c.Boolean(nullable: false),
                        IsAdmin = c.Boolean(nullable: false),
                        Status = c.Int(nullable: false),
                        CreatedById = c.String(),
                        CreatedDateTime = c.DateTime(),
                        ModifiedById = c.String(),
                        ModifiedDateTime = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                        RolePermissionId = c.String(),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.RolePermissions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Available = c.Boolean(nullable: false),
                        ApplicationRoleId = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.BannerMappings",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        BannerId = c.Int(nullable: false),
                        LanguageId = c.Int(nullable: false),
                        Name = c.String(),
                        Description = c.String(),
                        Status = c.Int(nullable: false),
                        CreatedById = c.String(),
                        CreatedDateTime = c.DateTime(),
                        ModifiedById = c.String(),
                        ModifiedDateTime = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Banners", t => t.BannerId, cascadeDelete: true)
                .Index(t => t.BannerId);
            
            CreateTable(
                "dbo.Banners",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Type = c.Int(nullable: false),
                        Position = c.Int(nullable: false),
                        MediaType = c.Int(nullable: false),
                        MediaUrl = c.String(),
                        MediaThumbUrl = c.String(),
                        ZOrder = c.Int(nullable: false),
                        Status = c.Int(nullable: false),
                        CreatedById = c.String(),
                        CreatedDateTime = c.DateTime(),
                        ModifiedById = c.String(),
                        ModifiedDateTime = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Configs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CompanyName = c.String(),
                        Slogan = c.String(),
                        About = c.String(),
                        Email = c.String(),
                        EmailPassword = c.String(),
                        EmailPort = c.Int(nullable: false),
                        EmailSignature = c.String(),
                        GoogleAnalytics = c.String(),
                        Address = c.String(),
                        GoogleMap = c.String(),
                        GoogleWebMaster = c.String(),
                        FacebookRetager = c.String(),
                        Phone = c.String(),
                        LogoUrl = c.String(),
                        BannerLogo = c.String(),
                        ContactForm = c.String(),
                        FooterContent = c.String(),
                        Yahoo = c.String(),
                        Skype = c.String(),
                        FacebookSocial = c.String(),
                        GoogleSocial = c.String(),
                        TwitterSocial = c.String(),
                        PicasaSocial = c.String(),
                        Youtube = c.String(),
                        Instagram = c.String(),
                        Status = c.Int(nullable: false),
                        CreatedById = c.String(),
                        CreatedDateTime = c.DateTime(),
                        ModifiedById = c.String(),
                        ModifiedDateTime = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Contacts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Title = c.String(),
                        Email = c.String(),
                        Description = c.String(),
                        Status = c.Int(nullable: false),
                        CreatedById = c.String(),
                        CreatedDateTime = c.DateTime(),
                        ModifiedById = c.String(),
                        ModifiedDateTime = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ControlMappings",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        LanguageId = c.Int(nullable: false),
                        Text = c.String(),
                        Status = c.Int(nullable: false),
                        CreatedById = c.String(),
                        CreatedDateTime = c.DateTime(),
                        ModifiedById = c.String(),
                        ModifiedDateTime = c.DateTime(),
                        Control_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Controls", t => t.Control_Id)
                .Index(t => t.Control_Id);
            
            CreateTable(
                "dbo.Controls",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        GroupControlId = c.Int(nullable: false),
                        Name = c.String(),
                        Image = c.String(),
                        Description = c.String(),
                        ZOrder = c.Int(nullable: false),
                        Status = c.Int(nullable: false),
                        CreatedById = c.String(),
                        CreatedDateTime = c.DateTime(),
                        ModifiedById = c.String(),
                        ModifiedDateTime = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.GroupControls", t => t.GroupControlId, cascadeDelete: true)
                .Index(t => t.GroupControlId);
            
            CreateTable(
                "dbo.GroupControls",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Code = c.String(),
                        Name = c.String(),
                        Type = c.String(),
                        Parent = c.Int(nullable: false),
                        ZOrder = c.Int(nullable: false),
                        Status = c.Int(nullable: false),
                        CreatedById = c.String(),
                        CreatedDateTime = c.DateTime(),
                        ModifiedById = c.String(),
                        ModifiedDateTime = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Languages",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        LanguageCode = c.String(),
                        Image = c.String(),
                        Text = c.String(),
                        NaturalText = c.String(),
                        Status = c.Boolean(nullable: false),
                        ZOrder = c.Int(nullable: false),
                        CreatedById = c.String(),
                        CreatedDateTime = c.DateTime(),
                        ModifiedById = c.String(),
                        ModifiedDateTime = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.NewsDrafts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        NewsId = c.Int(),
                        CourseId = c.Int(),
                        GalleryId = c.Int(),
                        Name = c.String(),
                        Keyword = c.String(),
                        Description = c.String(),
                        Order = c.Int(nullable: false),
                        PreviewImage = c.String(),
                        Url = c.String(),
                        HomeBannerImageUrl = c.String(),
                        BannerImageIntro = c.String(),
                        SubBannerImageUrl = c.String(),
                        ContentIntro = c.String(),
                        ContentLeft = c.String(),
                        ContentRight = c.String(),
                        CreateTime = c.DateTime(nullable: false),
                        DraftType = c.String(),
                        EditDeleteBy = c.String(),
                        AdminChecked = c.Boolean(nullable: false),
                        Status = c.Int(nullable: false),
                        CreatedById = c.String(),
                        CreatedDateTime = c.DateTime(),
                        ModifiedById = c.String(),
                        ModifiedDateTime = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.News",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        Name = c.String(),
                        CategoryId = c.Int(nullable: false),
                        ListTagId = c.String(),
                        Status = c.Int(nullable: false),
                        CreatedById = c.String(),
                        CreatedDateTime = c.DateTime(),
                        ModifiedById = c.String(),
                        ModifiedDateTime = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Projects", t => t.Id)
                .Index(t => t.Id);
            
            CreateTable(
                "dbo.NewsMappings",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        NewsId = c.Int(nullable: false),
                        LanguageId = c.Int(nullable: false),
                        Title = c.String(),
                        Intro = c.String(),
                        Detail = c.String(),
                        MetaData = c.String(),
                        Keyword = c.String(),
                        Description = c.String(),
                        Status = c.Int(nullable: false),
                        CreatedById = c.String(),
                        CreatedDateTime = c.DateTime(),
                        ModifiedById = c.String(),
                        ModifiedDateTime = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.News", t => t.NewsId, cascadeDelete: true)
                .Index(t => t.NewsId);
            
            CreateTable(
                "dbo.Projects",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Type = c.Int(nullable: false),
                        ZOrder = c.Int(nullable: false),
                        Status = c.Int(nullable: false),
                        CreatedById = c.String(),
                        CreatedDateTime = c.DateTime(),
                        ModifiedById = c.String(),
                        ModifiedDateTime = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ProjectImages",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ProjectId = c.Int(nullable: false),
                        Thumbnail = c.String(),
                        ResourceUrl = c.String(),
                        Type = c.Int(nullable: false),
                        ZOrder = c.Int(nullable: false),
                        Status = c.Int(nullable: false),
                        CreatedById = c.String(),
                        CreatedDateTime = c.DateTime(),
                        ModifiedById = c.String(),
                        ModifiedDateTime = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Projects", t => t.ProjectId, cascadeDelete: true)
                .Index(t => t.ProjectId);
            
            CreateTable(
                "dbo.ProjectImageMappings",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ProjectImageId = c.Int(nullable: false),
                        LanguageId = c.Int(nullable: false),
                        Name = c.String(),
                        Description = c.String(),
                        Status = c.Int(nullable: false),
                        CreatedById = c.String(),
                        CreatedDateTime = c.DateTime(),
                        ModifiedById = c.String(),
                        ModifiedDateTime = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ProjectImages", t => t.ProjectImageId, cascadeDelete: true)
                .Index(t => t.ProjectImageId);
            
            CreateTable(
                "dbo.ProjectMappings",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ProjectId = c.Int(nullable: false),
                        LanguageId = c.Int(nullable: false),
                        Name = c.String(),
                        Description = c.String(),
                        Status = c.Int(nullable: false),
                        CreatedById = c.String(),
                        CreatedDateTime = c.DateTime(),
                        ModifiedById = c.String(),
                        ModifiedDateTime = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Projects", t => t.ProjectId, cascadeDelete: true)
                .Index(t => t.ProjectId);
            
            CreateTable(
                "dbo.UserInfoes",
                c => new
                    {
                        UserInfoId = c.Int(nullable: false, identity: true),
                        Email = c.String(),
                        Avatar = c.String(),
                        Address = c.String(),
                        Skype = c.String(),
                        Yahoo = c.String(),
                        Facebook = c.String(),
                        Available = c.Boolean(nullable: false),
                        AspNetUser_Id = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.UserInfoId)
                .ForeignKey("dbo.AspNetUsers", t => t.AspNetUser_Id)
                .Index(t => t.AspNetUser_Id);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.RolePermissionApplicationRoles",
                c => new
                    {
                        RolePermission_Id = c.Int(nullable: false),
                        ApplicationRole_Id = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.RolePermission_Id, t.ApplicationRole_Id })
                .ForeignKey("dbo.RolePermissions", t => t.RolePermission_Id, cascadeDelete: true)
                .ForeignKey("dbo.AspNetRoles", t => t.ApplicationRole_Id, cascadeDelete: true)
                .Index(t => t.RolePermission_Id)
                .Index(t => t.ApplicationRole_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserInfoes", "AspNetUser_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.News", "Id", "dbo.Projects");
            DropForeignKey("dbo.ProjectMappings", "ProjectId", "dbo.Projects");
            DropForeignKey("dbo.ProjectImageMappings", "ProjectImageId", "dbo.ProjectImages");
            DropForeignKey("dbo.ProjectImages", "ProjectId", "dbo.Projects");
            DropForeignKey("dbo.NewsMappings", "NewsId", "dbo.News");
            DropForeignKey("dbo.Controls", "GroupControlId", "dbo.GroupControls");
            DropForeignKey("dbo.ControlMappings", "Control_Id", "dbo.Controls");
            DropForeignKey("dbo.BannerMappings", "BannerId", "dbo.Banners");
            DropForeignKey("dbo.RolePermissionApplicationRoles", "ApplicationRole_Id", "dbo.AspNetRoles");
            DropForeignKey("dbo.RolePermissionApplicationRoles", "RolePermission_Id", "dbo.RolePermissions");
            DropIndex("dbo.RolePermissionApplicationRoles", new[] { "ApplicationRole_Id" });
            DropIndex("dbo.RolePermissionApplicationRoles", new[] { "RolePermission_Id" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.UserInfoes", new[] { "AspNetUser_Id" });
            DropIndex("dbo.ProjectMappings", new[] { "ProjectId" });
            DropIndex("dbo.ProjectImageMappings", new[] { "ProjectImageId" });
            DropIndex("dbo.ProjectImages", new[] { "ProjectId" });
            DropIndex("dbo.NewsMappings", new[] { "NewsId" });
            DropIndex("dbo.News", new[] { "Id" });
            DropIndex("dbo.Controls", new[] { "GroupControlId" });
            DropIndex("dbo.ControlMappings", new[] { "Control_Id" });
            DropIndex("dbo.BannerMappings", new[] { "BannerId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropTable("dbo.RolePermissionApplicationRoles");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.UserInfoes");
            DropTable("dbo.ProjectMappings");
            DropTable("dbo.ProjectImageMappings");
            DropTable("dbo.ProjectImages");
            DropTable("dbo.Projects");
            DropTable("dbo.NewsMappings");
            DropTable("dbo.News");
            DropTable("dbo.NewsDrafts");
            DropTable("dbo.Languages");
            DropTable("dbo.GroupControls");
            DropTable("dbo.Controls");
            DropTable("dbo.ControlMappings");
            DropTable("dbo.Contacts");
            DropTable("dbo.Configs");
            DropTable("dbo.Banners");
            DropTable("dbo.BannerMappings");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.RolePermissions");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.AdminMenus");
        }
    }
}
