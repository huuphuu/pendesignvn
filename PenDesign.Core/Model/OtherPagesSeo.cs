using PenDesign.Core.Model.BaseClass;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PenDesign.Core.Model
{ 
    public class OtherPageSEO: EditableEntity
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string Page { get; set; }
        public string Keyword { get; set; }
        public string Description { get; set; }
        public string MetaData { get; set; }
    }
}
