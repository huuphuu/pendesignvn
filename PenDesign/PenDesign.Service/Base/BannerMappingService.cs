using PenDesign.Core.Interface.Data;
using PenDesign.Core.Interface.Service.BasicServiceInterface;
using PenDesign.Core.Model;
namespace PenDesign.Service.Base
{
    public partial class BannerMappingService: BaseService<BannerMapping>, IBannerMappingService
    {
        public BannerMappingService(IRepository<BannerMapping> repository, IUnitOfWork unitOfWork)
            :base(repository, unitOfWork)
        {
            
        }
    }
}