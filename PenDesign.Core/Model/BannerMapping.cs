using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using PenDesign.Core.Model.BaseClass;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace PenDesign.Core.Model
{
    public partial class BannerMapping: EditableEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int BannerId { get; set; }
        public int LanguageId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string MediaUrl { get; set; }
        public string MediaThumbUrl { get; set; }
        public string LinkUrl { get; set; }

        [IgnoreDataMember]
        [JsonIgnore]
        public virtual Banner Banner { get; set; }
    }
}
