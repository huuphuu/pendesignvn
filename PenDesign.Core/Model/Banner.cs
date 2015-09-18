using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using PenDesign.Core.Model.BaseClass;

namespace PenDesign.Core.Model
{
    public partial class Banner: EditableEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string Name { get; set; }
        public int Type { get; set; }
        public int Position { get; set; }
        public int MediaType { get; set; }
        public string MediaUrl { get; set; }
        public string MediaThumbUrl { get; set; }
        public string LinkUrl { get; set; }
        public int ZOrder { get; set; }

        public virtual ICollection<BannerMapping> BannerMappings { get; set; }
    }
}
