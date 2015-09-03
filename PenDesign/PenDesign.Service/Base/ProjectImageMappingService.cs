using PenDesign.Core.Interface.Data;
using PenDesign.Core.Interface.Service.BasicServiceInterface;
using PenDesign.Core.Model;
namespace PenDesign.Service.Base
{
    public class ProjectImageMappingService: BaseService<ProjectImageMapping>, IProjectImageMappingService
    {
        public ProjectImageMappingService(IRepository<ProjectImageMapping> repository, IUnitOfWork unitOfWork)
            :base(repository, unitOfWork)
        {
            
        } 
    }
}