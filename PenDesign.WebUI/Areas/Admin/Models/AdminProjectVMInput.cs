using PenDesign.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PenDesign.WebUI.Areas.Admin.Models
{
    public class AdminProjectVMInput: Project
    {
        public string Title { get; set; }
        public string Intro { get; set; }
        public string Detail { get; set; }
        public string Keyword { get; set; }
        public string Description { get; set; }
        public string MetaData { get; set; }
        public string ResourceUrl { get; set; }
        public string Thumbnail { get; set; }
    }
}