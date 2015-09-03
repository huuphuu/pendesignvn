using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using PenDesign.Core.Model.BaseClass;

namespace PenDesign.Core.Model
{
    public partial class Project: EditableEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string Name { get; set; }
        public int Type { get; set; }
        public int ZOrder { get; set; }

        public virtual ICollection<News> Newses { get; set; }
        public virtual ICollection<ProjectMapping> ProjectMappings { get; set; }
        public virtual ICollection<ProjectImage> ProjectImages { get; set; }
    }
}
