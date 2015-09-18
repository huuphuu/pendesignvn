using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using PenDesign.Core.Model.BaseClass;
using Newtonsoft.Json;
using System.Runtime.Serialization;

namespace PenDesign.Core.Model
{
    [JsonObject(IsReference = true)] 
    public partial class Project: EditableEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public int Type { get; set; }
        [DataMember]
        public int ZOrder { get; set; }

        public virtual ICollection<News> News { get; set; }
        public virtual ICollection<ProjectMapping> ProjectMappings { get; set; }
        public virtual ICollection<ProjectImage> ProjectImages { get; set; }
    }
}
