using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PenDesign.Core.Interface.Data
{
    public interface IUnitOfWork
    {
        int Commit();
    }
}
