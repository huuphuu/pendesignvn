using PenDesign.Core.Interface.Data;
using PenDesign.Core.Interface.Service.BasicServiceInterface;
using PenDesign.Core.Model;
namespace PenDesign.Service.Base
{
    public partial class LanguageService : BaseService<Language>, ILanguageService
    {
        public LanguageService(IRepository<Language> repository, IUnitOfWork unitOfWork)
            :base(repository, unitOfWork)
        {
            
        }
         
    }
}