using PenDesign.Core.Interface.Data;
using PenDesign.Core.Interface.Service.BasicServiceInterface;
using PenDesign.Core.Model;
using System;

namespace PenDesign.Service.Base
{
    public partial class NewsCategoryService : BaseService<NewsCategory>, INewsCategoryService
    {
        public NewsCategoryService(IRepository<NewsCategory> repository, IUnitOfWork unitOfWork)
            : base(repository, unitOfWork)
        {

        }
    }
}
