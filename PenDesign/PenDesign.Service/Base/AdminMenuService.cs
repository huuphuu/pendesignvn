using PenDesign.Core.Interface.Data;
using PenDesign.Core.Interface.Service.BasicServiceInterface;
using PenDesign.Core.Model;
namespace PenDesign.Service.Base
{
    public class AdminMenuService: BaseService<AdminMenu>, IAdminMenuService
    {
        public AdminMenuService(IRepository<AdminMenu> repository, IUnitOfWork unitOfWork)
            :base(repository, unitOfWork)
        {
            
        }
    }
}