using System;


namespace PenDesign.Core.Model.BaseClass
{
    public class PersistentEntity
    {
        public bool Deleted { get; set; }
        public Nullable<System.DateTime> DeletedDate { get; set; }
    }
}
