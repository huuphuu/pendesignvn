using PenDesign.Core.Model.BaseClass;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PenDesign.Core.Model
{
    public partial class AdminMenu : EditableEntity
    {
        private bool _Available = true;

        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
        public string CssIcon { get; set; }
        public string LabelCss { get; set; }
        public string Type { get; set; }
        public int Parent { get; set; }
        public int Order { get; set; }

        public bool Available
        {
            get { return _Available; }
            set { _Available = value; }
        }
        public bool IsAdmin { get; set; }
    }
}
