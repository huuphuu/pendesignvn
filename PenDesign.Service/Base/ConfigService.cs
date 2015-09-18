using PenDesign.Core.Interface.Data;
using PenDesign.Core.Interface.Service.BasicServiceInterface;
using PenDesign.Core.Model;
namespace PenDesign.Service.Base
{
    public class ConfigService: BaseService<Config>, IConfigService
    {
        public ConfigService(IRepository<Config> repository, IUnitOfWork unitOfWork)
            :base(repository, unitOfWork)
        {
            
        }
    }
}