using PenDesign.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PenDesign.WebUI.Areas.Admin.Models
{
    public class AdminProjectVMInput: Project
    {
        public AdminProjectNewsVMInput News;
    }

    public class AdminProjectNewsVMInput : News
    {
        public NewsMapping currentNewsMappings;
    }


}