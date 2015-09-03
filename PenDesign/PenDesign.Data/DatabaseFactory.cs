using PenDesign.Common.Disposable;
using PenDesign.Data.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PenDesign.Data
{
    public class DatabaseFactory: Disposable, IDatabaseFactory
    {
        private IDataContext dataContext;
        public IDataContext Get()
        {
            return dataContext ?? (dataContext = new PenDesignDbContext());
        }

        protected override void DisposeCore()
        {
            if (dataContext != null) dataContext.Dispose();
        }
    }
}
