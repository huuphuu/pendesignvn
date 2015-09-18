using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using PenDesign.Core.Model.BaseClass;


namespace PenDesign.Core.Model
{
    public partial class ProjectImageMapping: EditableEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public int ProjectImageId { get; set; }
        public int LanguageId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public virtual ProjectImage ProjectImage { get; set; }
    }
}
