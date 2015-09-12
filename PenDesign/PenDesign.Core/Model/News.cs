using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using PenDesign.Core.Model.BaseClass;

namespace PenDesign.Core.Model
{
    public partial class News: EditableEntity
    {
        //[Key, ForeignKey("Project")] // 1-1 relation Ship Config
        //[DatabaseGenerated(DatabaseGeneratedOption.None)]
        //public int Id { get; set; }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public int? NewsCategoryId { get; set; }
        public int? ProjectId { get; set; }

        public string Name { get; set; }
        public string Thumbnail { get; set; }
        public int? ZOrder { get; set; }
        public string ListTagId { get; set; }

        public virtual NewsCategory NewsCategory { get; set; }
        public virtual Project Project { get; set; }
        public virtual ICollection<NewsMapping> NewsMappings { get; set; }
    }
}