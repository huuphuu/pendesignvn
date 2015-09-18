using System.Data;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Newtonsoft.Json;
using System.Runtime.Serialization;
using PenDesign.Core.Model;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration;

namespace PenDesign.WebUI.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    [JsonObject(IsReference = true)]
    public class ApplicationUser : IdentityUser
    {
        [JsonIgnore]
        [IgnoreDataMember]
        public UserInfo UserInfo { get; set; }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false) 
        {
        }

        public DbSet<ApplicationRole> ApplicationRoles { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<IdentityRole>().ToTable("AspNetRoles");

            // Change these from IdentityRole to ApplicationRole:
            EntityTypeConfiguration<ApplicationRole> entityTypeConfiguration1 =
                modelBuilder.Entity<ApplicationRole>().ToTable("AspNetRoles");

            entityTypeConfiguration1.Property((ApplicationRole r) => r.Name).IsRequired();

            modelBuilder.Entity<ApplicationRole>()
             .HasMany(p => p.RolePermissions)
             .WithMany(d => d.ApplicationRoles)
             .Map(m =>
             {
                 m.MapLeftKey("RoleId");
                 m.MapRightKey("RolePermissionId");
                 m.ToTable("RoleRolePermission");
             });
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }
}