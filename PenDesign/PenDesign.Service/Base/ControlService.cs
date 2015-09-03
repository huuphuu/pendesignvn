using PenDesign.Core.Interface.Data;
using PenDesign.Core.Interface.Service.BasicServiceInterface;
using PenDesign.Core.Model;
namespace PenDesign.Service.Base
{
    public partial class ControlService: BaseService<Control>, IControlService
    {
        public ControlService(IRepository<Control> repository, IUnitOfWork unitOfWork)
            :base(repository, unitOfWork)
        {
            
        }
    }
}