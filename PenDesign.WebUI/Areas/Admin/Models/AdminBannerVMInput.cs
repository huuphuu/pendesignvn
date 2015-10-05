using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PenDesign.Core.Model;

namespace PenDesign.WebUI.Models
{
    public class AdminBannerVMInput: Banner
    {
        public string MediaUrl { get; set; }
        public string LinkUrl { get; set; }
        public string Description { get; set; }
    }
}