using PenDesign.Core.Model;
using PenDesign.Core.Models;
using PenDesign.Data.Interface;
using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity.EntityFramework;
using PenDesign.Data.MigrationConfiguration;
using PenDesign.Core.Model.BaseClass;

namespace PenDesign.Data
{
    public class PenDesignDbContext : IdentityDbContext<ApplicationUser>, IDataContext
    {
        public PenDesignDbContext()
            : base("name=DefaultConnection")
        {
            this.Configuration.LazyLoadingEnabled = true;
            this.Configuration.ProxyCreationEnabled = true;

            Database.SetInitializer(
                new MigrateDatabaseToLatestVersion<PenDesignDbContext, PenDesignMigrationConfiguration>());
        }

        public DbSet<AdminMenu> AdminMenus { get; set; }

        public DbSet<Banner> Banners { get; set; }
        public DbSet<BannerMapping> BannerMappings { get; set; }

        public DbSet<Contact> Contacts { get; set; }

        public DbSet<Control> Controls { get; set; }
        public DbSet<ControlMapping> ControlMappings { get; set; }
        public DbSet<GroupControl> GroupControls { get; set; }

        public DbSet<Language> Languages { get; set; }

        public DbSet<NewsCategory> NewsCategories { get; set; }
        public DbSet<NewsCategoryMapping> NewsCategoryMappings { get; set; }
        public DbSet<News> Newses { get; set; }
        public DbSet<NewsMapping> NewsMappings { get; set; }
        public DbSet<NewsDraft> NewsDrafts { get; set; }


        public DbSet<Project> Projects { get; set; }
        public DbSet<ProjectMapping> ProjectMappings { get; set; }
        public DbSet<ProjectImage> ProjectImages { get; set; }
        public DbSet<ProjectImageMapping> ProjectImageMappings { get; set; }


        public DbSet<UserInfo> UserInfos { get; set; }
        public DbSet<ApplicationRole> ApplicationRoles { get; set; }
        public DbSet<RolePermission> RolePermissions { get; set; }


        public DbSet<Config> Configs { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //modelBuilder.Entity<Project>()
            //            .HasKey(p => p.Id);

            //// Configure StudentId as FK for StudentAddress
            //modelBuilder.Entity<News>()
            //            .HasRequired(n => n.Project)
            //            .WithOptional(s => s.News); 
        }

        public IDbSet<T> DbSet<T>() where T : EditableEntity
        {
            return this.Set<T>();
        }

        public DbEntityEntry<T> EntryGet<T>(T entity) where T : EditableEntity
        {
            return this.Entry<T>(entity);
        }

        public virtual int Commit()
        {
            return this.SaveChanges();
        }
    }
}
