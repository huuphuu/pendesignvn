using System;
using System.Collections.Generic;

namespace PenDesign.Data.Interface
{
    public interface IDatabaseFactory: IDisposable
    {
        IDataContext Get();
    }
}
