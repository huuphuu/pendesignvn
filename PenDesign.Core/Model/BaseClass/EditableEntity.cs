using System;


namespace PenDesign.Core.Model.BaseClass
{
    public abstract class EditableEntity
    {
        private  DateTime? _DateTimeNow = DateTime.Now;
        public bool Status { get; set; }
        public bool Deleted { get; set; }
        public string CreatedById { get; set; }

        public Nullable<DateTime> CreatedDateTime
        {
            get { return _DateTimeNow; } 
            set { _DateTimeNow = value; }
        }

        public string  ModifiedById { get; set; }

        public Nullable<DateTime> ModifiedDateTime
        {
            get { return _DateTimeNow; }
            set { _DateTimeNow = value; }
        }
    }
}
