using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using PenDesign.Core.Model.BaseClass;

namespace PenDesign.Core.Model
{
    public partial class NewsMapping: EditableEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public int NewsId { get; set; }
        public int LanguageId { get; set; }
        public string Title { get; set; }
        public string Intro { get; set; }
        public string Detail { get; set; }
        public string MetaData { get; set; }
        public string Keyword { get; set; }
        public string Description { get; set; }

        public virtual News News { get; set; }
    }
}
