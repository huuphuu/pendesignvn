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
        public string HomePageKeyword { get; set; }
        public string HomePageDescription { get; set; }
        public string HomePageMetaData { get; set; }


        public string AboutKeyword { get; set; }
        public string AboutDescription { get; set; }
        public string AboutMetaData { get; set; }

        public string ContactKeyword { get; set; }
        public string ContactDescription { get; set; }
        public string ContactMetaData { get; set; }

        public string ProjectKeyword { get; set; }
        public string ProjectDescription { get; set; }
        public string ProjectMetaData { get; set; }

        public string ConstructionKeyword { get; set; }
        public string ConstructionDescription { get; set; }
        public string ConstructionMetaData { get; set; }

        public string VideoKeyword { get; set; }
        public string VideoDescription { get; set; }
        public string VideoMetaData { get; set; }
    }
}
