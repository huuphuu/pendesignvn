using PenDesign.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PenDesign.WebUI.Areas.Admin.Models
{
    public class AdminNewsVM
    {
        public News Newses { get; set; }
        public string EditedUser { get; set; }
    }
}