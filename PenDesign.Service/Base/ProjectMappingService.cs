using PenDesign.Core.Interface.Data;
using PenDesign.Core.Interface.Service.BasicServiceInterface;
using PenDesign.Core.Model;
namespace PenDesign.Service.Base
{
    public class ProjectMappingService: BaseService<ProjectMapping>, IProjectMappingService
    {
        public ProjectMappingService(IRepository<ProjectMapping> repository, IUnitOfWork unitOfWork)
            :base(repository, unitOfWork)
        {
            
        } 
    }
}