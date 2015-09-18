using PenDesign.Core.Interface.Data;
using PenDesign.Core.Interface.Service.BasicServiceInterface;
using PenDesign.Core.Model;
namespace PenDesign.Service.Base
{
    public class GroupControlService : BaseService<GroupControl>, IGroupControlService
    {
        public GroupControlService(IRepository<GroupControl> repository, IUnitOfWork unitOfWork)
            :base(repository, unitOfWork)
        {
            
        }
    }
}