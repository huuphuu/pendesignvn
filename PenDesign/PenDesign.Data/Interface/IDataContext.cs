using PenDesign.Core.Model.BaseClass;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;

namespace PenDesign.Data.Interface
{
    public interface IDataContext: IDisposable
    {
        IDbSet<T> DbSet<T>() where T : EditableEntity;

        DbEntityEntry<T> EntryGet<T>(T entity) where T : EditableEntity;

        int Commit();
    }
}
