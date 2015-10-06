using PenDesign.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PenDesign.WebUI.Areas.Admin.Models
{
    public class AdminProjectVM
    {
        public Project Projects;
        public string EditedUser { get; set; }
    }
}