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
    public partial class Banner: EditableEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [DataMember]
        public string Name { get; set; }
        //[DataMember]
        //public int Type { get; set; }
        [DataMember]
        public int Position { get; set; }
        [DataMember]
        public int MediaType { get; set; }
        //[DataMember]
        //public string MediaUrl { get; set; }
        //[DataMember]
        //public string MediaThumbUrl { get; set; }
        //[DataMember]
        //public string LinkUrl { get; set; }
        [DataMember]
        public int ZOrder { get; set; }

        public virtual ICollection<BannerMapping> BannerMappings { get; set; }
    }
}
