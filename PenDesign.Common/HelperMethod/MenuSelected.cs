using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace PenDesign.Common.HelperMethod
{
    public static class MenuSelected
    {
        public static string IsSelected(this HtmlHelper html, string controllers = "", string actions = "", int newsCategoryId = 0, string cssClass = "current")
        {
            ViewContext viewContext = html.ViewContext;
            var request = HttpContext.Current.Request.RequestContext;
            //bool isChildAction = viewContext.Controller.ControllerContext.IsChildAction;
            var routeData = HttpContext.Current.Request.RequestContext.RouteData.Values;
            string currentAction = routeData["action"].ToString();
            string currentController = routeData["controller"].ToString();


            int currentNewsCategoryId = 0;
            var tempNewsCategoryId = routeData["id"];
            if (tempNewsCategoryId != null)
                currentNewsCategoryId = Int32.Parse(routeData["id"].ToString());
            else
                currentNewsCategoryId = -1;



            //if (isChildAction)
            //    viewContext = html.ViewContext.ParentActionViewContext;
            //RouteValueDictionary routeValues = viewContext.RouteData.Values;
            //string currentAction = routeValues["action"].ToString();
            //string currentController = routeValues["controller"].ToString();

            if (String.IsNullOrEmpty(actions))
                actions = currentAction;

            if (String.IsNullOrEmpty(controllers))
                controllers = currentController;

            string[] acceptedActions = actions.Trim().Split(',').Distinct().ToArray();
            for (var i = 0; i < acceptedActions.Length; i++)
            {
                acceptedActions[i] = acceptedActions[i].Trim();
            }
            string[] acceptedControllers = controllers.Trim().Split(',').Distinct().ToArray();
            for (var i = 0; i < acceptedControllers.Length; i++)
            {
                acceptedControllers[i] = acceptedControllers[i].Trim();
            }

            int acceptedNewsCategoryId = newsCategoryId;

            return acceptedActions.Contains(currentAction) && acceptedControllers.Contains(currentController) ? cssClass : String.Empty;


            //if (currentNewsCategoryId == -1)
            //    return acceptedActions.Contains(currentAction) && acceptedControllers.Contains(currentController) ? cssClass : String.Empty;
            //else
            //    return acceptedActions.Contains(currentAction) && acceptedControllers.Contains(currentController) && acceptedNewsCategoryId == currentNewsCategoryId ?
            //        cssClass : String.Empty;
        }
    }
}
