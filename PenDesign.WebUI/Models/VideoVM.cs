using PenDesign.Common.Paging;
using PenDesign.Core.Model;
using System.Collections.Generic;

namespace PenDesign.WebUI.Models
{
    public class VideoVM
    {
        public IEnumerable<ProjectImage> ProjectImages { get; set; }
        public IPage<ProjectImage> PagingItems { get; set; } 
    }
}