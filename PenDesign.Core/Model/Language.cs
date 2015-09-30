using PenDesign.Core.Model.BaseClass;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PenDesign.Core.Model
{
    public partial class Language : EditableEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string LanguageCode { get; set; }
        public string Image { get; set; }
        public string Text { get; set; }
        public string NaturalText { get; set; }
        public int ZOrder { get; set; }

    }
}
