using PenDesign.Core.Interface.Data;
using PenDesign.Core.Interface.Service.BasicServiceInterface;
using PenDesign.Core.Model;
namespace PenDesign.Service.Base
{
    public partial class ProjectImageService: BaseService<ProjectImage>, IProjectImageService
    {
        public ProjectImageService(IRepository<ProjectImage> repository, IUnitOfWork unitOfWork)
            :base(repository, unitOfWork)
        {
            
        } 
    }
}