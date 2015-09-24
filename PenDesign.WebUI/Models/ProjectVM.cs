using PenDesign.Common.Paging;
using PenDesign.Core.Model;
using System.Collections.Generic;

namespace PenDesign.WebUI.Models
{
    public class ProjectVM
    {
        public IEnumerable<Project> Project  { get; set; }
        public IPage<Project> PagingItems { get; set; } 
    }
}