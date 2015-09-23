using PenDesign.Core.Interface.Data;
using PenDesign.Core.Interface.Service.BasicServiceInterface;
using PenDesign.Core.Model;

namespace PenDesign.Service.Base
{
    public partial class OtherPageSEOService : BaseService<OtherPageSEO>, IOtherPageSEOService
    {
        public OtherPageSEOService(IRepository<OtherPageSEO> repository, IUnitOfWork unitOfWork)
            : base(repository, unitOfWork)
        {

        }
    }
}
