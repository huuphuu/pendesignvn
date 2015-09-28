using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace PenDesign.WebUI.Infrastructure
{
    public class AppSettings
    {
        public static int ItemsPerPage
        {
            get
            {
                return int.Parse(ConfigurationManager.AppSettings["ItemsPerPage"]);
            }
        }
    }
}