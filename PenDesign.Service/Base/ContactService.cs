using PenDesign.Core.Interface.Data;
using PenDesign.Core.Interface.Service.BasicServiceInterface;
using PenDesign.Core.Model;
namespace PenDesign.Service.Base
{
    public partial class ContactService: BaseService<Contact>, IContactService
    {
        public ContactService(IRepository<Contact> repository, IUnitOfWork unitOfWork)
            :base(repository, unitOfWork)
        {
            
        }
         
    }
}