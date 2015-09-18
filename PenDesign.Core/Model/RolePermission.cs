using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PenDesign.Core.Model
{
    public class RolePermission
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; }
        public bool Available { get; set; }
        public string ApplicationRoleId { get; set; }
        [ForeignKey("ApplicationRoleId")]
        public virtual List<ApplicationRole> ApplicationRoles { get; set; }

    }
}
