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
    public partial class ProjectImage: EditableEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [DataMember]
        public int ProjectId { get; set; }
        [DataMember]
        public string Thumbnail { get; set; }
        [DataMember]
        public string ResourceUrl { get; set; }
        [DataMember]
        public int Type { get; set; }
        [DataMember]
        public int ZOrder { get; set; }

        [IgnoreDataMember]
        [JsonIgnore]
        public virtual Project Project { get; set; }
        [DataMember]
        public virtual ICollection<ProjectImageMapping> ProjectImageMappings { get; set; }
    }
}
