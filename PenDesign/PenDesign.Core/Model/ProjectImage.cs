using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using PenDesign.Core.Model.BaseClass;

namespace PenDesign.Core.Model
{
    public partial class ProjectImage: EditableEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public int ProjectId { get; set; }
        public string Name { get; set; }
        public string Thumbnail { get; set; }
        public string ResourceUrl { get; set; }
        public string Description { get; set; }
        public int Type { get; set; }
        public int Status { get; set; }
        public int ZOrder { get; set; }

        public virtual Project Project { get; set; }
        public virtual ICollection<ProjectImageMapping> ProjectImageMappings { get; set; }
    }
}
