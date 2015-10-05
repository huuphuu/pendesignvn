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
    public partial class News: EditableEntity
    {
        //[Key, ForeignKey("Project")] // 1-1 relation Ship Config
        //[DatabaseGenerated(DatabaseGeneratedOption.None)]
        //public int Id { get; set; }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [DataMember]
        public int? NewsCategoryId { get; set; }
        [DataMember]
        public int? ProjectId { get; set; }
        [DataMember]
        public string Name { get; set; }
        //[DataMember]
        //public string Thumbnail { get; set; }
        [DataMember]
        public int? ZOrder { get; set; }
        [DataMember]
        public string ListTagId { get; set; }

        [IgnoreDataMember]
        [JsonIgnore]
        public virtual NewsCategory NewsCategory { get; set; }
        
        [IgnoreDataMember]
        [JsonIgnore]
        public virtual Project Project { get; set; }
        
        [DataMember]
        public virtual ICollection<NewsMapping> NewsMappings { get; set; }
    }
}