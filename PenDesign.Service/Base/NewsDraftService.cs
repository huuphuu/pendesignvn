using PenDesign.Core.Interface.Data;
using PenDesign.Core.Interface.Service.BasicServiceInterface;
using PenDesign.Core.Model;
using System;

namespace PenDesign.Service.Base
{
    public partial class NewsDraftService : BaseService<NewsDraft>, INewsDraftService
    {
        public NewsDraftService(IRepository<NewsDraft> repository, IUnitOfWork unitOfWork)
            : base(repository, unitOfWork)
        {

        }
    }
}
