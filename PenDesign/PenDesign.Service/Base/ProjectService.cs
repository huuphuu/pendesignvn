using PenDesign.Core.Interface.Data;
using PenDesign.Core.Interface.Service.BasicServiceInterface;
using PenDesign.Core.Model;
namespace PenDesign.Service.Base
{
    public partial class ProjectService: BaseService<Project>, IProjectService
    {
        public ProjectService(IRepository<Project> repository, IUnitOfWork unitOfWork)
            :base(repository, unitOfWork)
        {
            
        } 
    }
}