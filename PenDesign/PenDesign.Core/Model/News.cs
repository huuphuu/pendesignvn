using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using PenDesign.Core.Model.BaseClass;

namespace PenDesign.Core.Model
{
    public partial class News: EditableEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public int ZOrder { get; set; }
        public int CategoryId { get; set; }
        public string ListTagId { get; set; }

        [ForeignKey("CategoryId")]
        public virtual Project Project { get; set; }
        public virtual ICollection<NewsMapping> NewsMappings { get; set; }
    }
}