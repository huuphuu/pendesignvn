using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace PenDesign.WebUI
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");


            routes.MapRoute(
                name: "Xu huong",
                url: "xu-huong",
                defaults: new { controller = "News", action = "ListByTrends", newsCategoryId = 1 },
                namespaces: new[] { "PenDesign.WebUI.Controllers" }
            );
            routes.MapRoute(
                name: "Xu huong - news",
                url: "xu-huong/{NewsName}-{id}",
                defaults: new { controller = "News", action = "Detail", id = UrlParameter.Optional },
                namespaces: new[] { "PenDesign.WebUI.Controllers" }
            );

            routes.MapRoute(
                name: "Khach hang",
                url: "khach-hang",
                defaults: new { controller = "News", action = "ListByCustomers", newsCategoryId = 2 },
                namespaces: new[] { "PenDesign.WebUI.Controllers" }
            );
            routes.MapRoute(
                name: "Khach hang - news",
                url: "khach-hang/{NewsName}-{id}",
                defaults: new { controller = "News", action = "Detail", id = UrlParameter.Optional },
                namespaces: new[] { "PenDesign.WebUI.Controllers" }
            );




            routes.MapRoute(
                name: "Construction",
                url: "cong-trinh-thuc-te/",
                defaults: new { controller = "Construction", action = "List" },
                namespaces: new[] { "PenDesign.WebUI.Controllers" }
            );
            routes.MapRoute(
                name: "Construction - news",
                url: "cong-trinh-thuc-te/{NewsName}-{id}",
                defaults: new { controller = "Construction", action = "Detail", id = UrlParameter.Optional },
                namespaces: new[] { "PenDesign.WebUI.Controllers" }
            );


            routes.MapRoute(
                name: "Video",
                url: "video-clip/",
                defaults: new { controller = "Video", action = "List" },
                namespaces: new[] { "PenDesign.WebUI.Controllers" }
            );


            routes.MapRoute(
                name: "Project",
                url: "du-an/",
                defaults: new { controller = "Project", action = "List" },
                namespaces: new[] { "PenDesign.WebUI.Controllers" }
            );
            routes.MapRoute(
                name: "Project - news",
                url: "du-an/{NewsName}-{id}",
                defaults: new { controller = "Project", action = "Detail", id = UrlParameter.Optional },
                namespaces: new[] { "PenDesign.WebUI.Controllers" }
            );


            routes.MapRoute(
                name: "Contact",
                url: "lien-he/",
                defaults: new { controller = "Home", action = "Contact" },
                namespaces: new[] { "PenDesign.WebUI.Controllers" }
            );


            routes.MapRoute(
                name: "About",
                url: "gioi-thieu/",
                defaults: new { controller = "Home", action = "About" },
                namespaces: new[] { "PenDesign.WebUI.Controllers" }
            );


            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                namespaces: new[] { "PenDesign.WebUI.Controllers" }
            );
        }
    }
}
