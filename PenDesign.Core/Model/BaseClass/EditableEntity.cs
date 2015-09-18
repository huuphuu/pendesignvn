using System;


namespace PenDesign.Core.Model.BaseClass
{
    public abstract class EditableEntity
    {
        public int Status { get; set; }
        public string CreatedById { get; set; }
        public Nullable<DateTime> CreatedDateTime { get; set; }

        public string  ModifiedById { get; set; }
        public Nullable<DateTime> ModifiedDateTime { get; set; }
    }
}
