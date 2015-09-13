using PenDesign.Core.Interface.Data;
using PenDesign.Core.Interface.Service.BasicServiceInterface;
using PenDesign.Core.Model;

namespace PenDesign.Service.Base
{
    public partial class NewsMappingService: BaseService<NewsMapping>, INewsMappingService
    {
        public NewsMappingService(IRepository<NewsMapping> repository, IUnitOfWork unitOfWork)
            :base(repository, unitOfWork)
        {
            
        } 
    }
}