using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using PenDesign.Core.Model.BaseClass;

namespace PenDesign.Core.Model
{
    public partial class GroupControl: EditableEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string Code { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public int Parent { get; set; }
        public int ZOrder { get; set; }
        public int Status { get; set; }

        public virtual ICollection<Control> Controls { get; set; }
    }
}
