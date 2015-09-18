using System;


namespace PenDesign.Data.Interface
{
    public interface IDatabaseFactory: IDisposable
    {
        IDataContext Get();
    }
}
