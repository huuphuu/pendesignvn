using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PenDesign.Core.Model
{
    public class ApplicationRole : IdentityRole
    {
        public ApplicationRole() : base() { }

        public ApplicationRole(string name, List<RolePermission> rolePermission) //List<RolePermission> rolePermission
            : base(name)
        {
            this.RolePermissions = rolePermission;
        }

        //public int RolePermissionId { get; set; }
        //[ForeignKey("RolePermissionId")]
        public string RolePermissionId { get; set; }
        public virtual List<RolePermission> RolePermissions { get; set; }
    }
}
