using System.Web.Mvc;
using PenDesign.WebUI.Extensions;
using Newtonsoft.Json.Serialization;
using System.Web.Http;
using System.Web.Routing;

namespace PenDesign.WebUI.Areas.Admin
{
    public class AdminAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Admin";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapHttpRoute(
                "DefaultApiWithId",
                "Admin/api/{controller}/{id}",
                new { id = RouteParameter.Optional }, new { id = @"\d+" });

            context.MapHttpRoute(
                "DefaultApiWithAction",
                "Admin/api/{controller}/{action}"
                );
            context.MapHttpRoute(
                "DefaultApiWithActionAndId",
                "Admin/api/{controller}/{action}/{id}"
                );
            context.MapHttpRoute(
                "DefaultApiWithController",
                "Admin/api/{controller}"
                );

            /////////////////////////
            //METHOD
            ////////////////////////
            context.MapRoute(
                "Admin_controlPanel",
                "Admin-area/",
                new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                namespaces: new[] { "PenDesign.WebUI.Areas.Admin.Controllers" }
            );

            context.MapRoute(
                "Admin_default",
                "Admin/",
                new { controller = "Home", action = "Login", id = UrlParameter.Optional },
                namespaces: new[] { "PenDesign.WebUI.Areas.Admin.Controllers" }
            );


            RouteTable.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{action}/{id}",
                defaults: new { id = RouteParameter.Optional }
            ).RouteHandler = new PenDesign.WebUI.MvcApplication.SessionStateRouteHandler();

            var jsonFormatter = GlobalConfiguration.Configuration.Formatters.JsonFormatter;
            jsonFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
        }
    }
}