using PenDesign.Core.Interface.Data;
using PenDesign.Core.Interface.Service.BasicServiceInterface;
using PenDesign.Core.Model;
using System;

namespace PenDesign.Service.Base
{
    public partial class NewsCategoryMappingService : BaseService<NewsCategoryMapping>, INewsCategoryMappingService
    {
        public NewsCategoryMappingService(IRepository<NewsCategoryMapping> repository, IUnitOfWork unitOfWork)
            : base(repository, unitOfWork)
        {

        }
    }
}
