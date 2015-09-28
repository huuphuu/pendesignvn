using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Http;
using PenDesign.WebUI.Authencation;
using FooService;
using System.Web.Http.WebHost;
using System.Web.SessionState;
using System.IO;

namespace PenDesign.WebUI
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            GlobalConfiguration.Configuration.Filters.Add(new CustomAuthorize());

            GlobalConfiguration.Configuration.Formatters.JsonFormatter.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
            GlobalConfiguration.Configuration.Formatters.Remove(GlobalConfiguration.Configuration.Formatters.XmlFormatter);
            GlobalConfiguration.Configuration.Formatters.Insert(0, new RootFormatter());


            var path = Server.MapPath("~/Resources/Visitors.txt");
            Application["Visitors"] = int.Parse(File.ReadAllText(path));
        }

        protected void Session_Start()
        {
            Application.Lock();
            Application["Visitors"] = (int)Application["Visitors"] + 1;

            var path = Server.MapPath("~/Resources/Visitors.txt");
            File.WriteAllText(path, Application["Visitors"].ToString());
            Application.UnLock();
        }

        protected void Application_BeginRequest()
        {
            string LanguageId = "129";

            if (Request.Cookies["PenDesign:Language"] == null)
            {
                var ckiLanguage = new HttpCookie("PenDesign:Language", LanguageId);
                ckiLanguage.Value = LanguageId;
                ckiLanguage.Expires = DateTime.Now.AddMonths(1);
                Response.Cookies.Add(ckiLanguage);
            }
            else if (Request.Cookies["PenDesign:Language"] != null)
            {
                LanguageId = Request.Cookies["PenDesign:Language"].Value;
            }
        }

        protected void Application_PostAuthorizeRequest()
        {
            System.Web.HttpContext.Current.SetSessionStateBehavior(System.Web.SessionState.SessionStateBehavior.Required);
        }

        public class SessionableControllerHandler : HttpControllerHandler, IRequiresSessionState
        {
            public SessionableControllerHandler(RouteData routeData)
                : base(routeData)
            { }
        }

        public class SessionStateRouteHandler : IRouteHandler
        {
            IHttpHandler IRouteHandler.GetHttpHandler(RequestContext requestContext)
            {
                return new SessionableControllerHandler(requestContext.RouteData);
            }
        }  
    }
}
