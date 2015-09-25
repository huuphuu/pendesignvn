using PenDesign.Common.Paging;
using PenDesign.Core.Model;
using System.Collections.Generic;

namespace PenDesign.WebUI.Models
{
    public class ConstructionVM
    {
        public IEnumerable<News> News { get; set; }
        public IPage<News> PagingItems { get; set; } 
    }
}