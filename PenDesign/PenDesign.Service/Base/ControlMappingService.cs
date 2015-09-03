using PenDesign.Core.Interface.Data;
using PenDesign.Core.Interface.Service.BasicServiceInterface;
using PenDesign.Core.Model;
namespace PenDesign.Service.Base
{
    public partial class ControlMappingService: BaseService<ControlMapping>, IControlMappingService
    {
        public ControlMappingService(IRepository<ControlMapping> repository, IUnitOfWork unitOfWork)
            :base(repository, unitOfWork)
        {

        }
    }
}