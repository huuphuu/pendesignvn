using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PenDesign.Core.Model;

namespace PenDesign.WebUI.Areas.Admin.Models
{
    public class AdminBannerVM
    {
        public Banner Banners { get; set; }
        public string EditedUser  { get; set; }
    }
}