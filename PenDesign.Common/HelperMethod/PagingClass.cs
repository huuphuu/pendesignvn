using PenDesign.Common.Utils;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using System.Web.Routing;

namespace PenDesign.Common.HelperMethod
{
    public static class PagingClass
    {
        /// <summary>
        /// Tạo ra mã Html cho bộ phân trang với thông tin cấu hình cho
        /// bởi info
        /// </summary>
        public static MvcHtmlString Paging(this HtmlHelper html, PagingOptions option)
        {
            InitPagingOptions(html, option);
            StringBuilder builder = new StringBuilder();
            if (option.CreateWrapDiv)
            {
                builder.Append("<div ")
                    .Append(html.HtmlAttributes(option.DivHtmlAttributes))
                    .Append(">");
            }
            if (option.AllowGoToPage)
            {
                UrlHelper urlHelper = new UrlHelper(html.ViewContext.RequestContext);
                builder.Append("<form method='get' action='")
                    .Append(urlHelper.Action((string)null, (string)null))
                    .Append("'>");
            }
            CreatePageLink(html, option, builder);

            if (option.AllowGoToPage)
            {
                builder.Append("<input type='text' name='")
                    .Append(option.PageSegmentName)
                    .Append("' value='")
                    .Append(option.CurrentPage)
                    .Append("' ")
                    .Append(html.HtmlAttributes(option.TextBoxHtmlAttributes))
                    .Append(">");
                builder.Append("<input type='submit' value='")
                    .Append(option.GoButtonTitle)
                    .Append("' ")
                    .Append(html.HtmlAttributes(option.GoButtonHtmlAttributes))
                    .Append(">");

                KeepQueryStringAlive(builder, html, option);
                builder.Append("<form>");
            }


            if (option.CreateWrapDiv)
            {
                builder.Append("</div>");
            }
            return new MvcHtmlString(builder.ToString());
        }

        private static void KeepQueryStringAlive(StringBuilder builder,
            HtmlHelper html, PagingOptions option)
        {
            var queryString = html.ViewContext.HttpContext.Request.QueryString;
            foreach (string key in queryString.AllKeys)
            {
                if (!string.Equals(key, option.PageSegmentName, StringComparison.CurrentCultureIgnoreCase))
                    builder.Append(html.Hidden(key, queryString[key], new { id = (string)null }));
            }
        }

        /// <summary>
        /// thực hiện 1 số xử lý ban đầu để chỉnh lại những thông tin trong
        /// PagingOptions cho đúng, ví dụ như tính lại TotalPage, CurentPage
        /// , hay là đọc CurentPage từ QueryString
        /// </summary>
        /// <param name="option"></param>
        private static void InitPagingOptions(HtmlHelper html, PagingOptions option)
        {
            if (option.IsNotSetCurrentPage())
            {
                var request = html.ViewContext.HttpContext.Request;
                string str = request.QueryString[option.PageSegmentName];
                option.CurrentPage = 1;
                int currentPage;
                if (int.TryParse(str, out currentPage))
                    option.CurrentPage = currentPage;
            }
            option.RecaculatePagingInfo();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="html"></param>
        /// <param name="option"></param>
        /// <param name="builder"></param>
        private static void CreatePageLink(HtmlHelper html,
            PagingOptions option, StringBuilder builder)
        {
            var routeDict = GetRouteValueDictionary(html);
            routeDict.Remove(option.PageSegmentName);

            IDictionary<string, object> pageLinkHtmlAttributes =
                AnonymousTypeTools.GetDictionary(option.PageLinkHtmlAttributes);
            IDictionary<string, object> currentPageLinkHtmlAttributes =
                (option.CurrentPageLinkHtmlAttributes == null ? pageLinkHtmlAttributes :
                AnonymousTypeTools.GetDictionary(option.CurrentPageLinkHtmlAttributes));
            IDictionary<string, object> firstLastLinkHtmlAttributes =
                (option.FirstLastLinkHtmlAttributes == null ? pageLinkHtmlAttributes :
                AnonymousTypeTools.GetDictionary(option.FirstLastLinkHtmlAttributes));

            if (option.IncludeQueryStringForPage1)
                routeDict[option.PageSegmentName] = 1;

            builder.Append(html.RouteLink(option.FirstButtonTitle,
                routeDict, firstLastLinkHtmlAttributes));

            int count = option.MaxPageDisplay / 2;
            int prePage = Math.Max(option.CurrentPage - count, 1);
            count = option.MaxPageDisplay - 1 - option.CurrentPage + prePage;
            int lastPage = Math.Min(option.CurrentPage + count, option.TotalPage);
            count = count - lastPage + option.CurrentPage;
            prePage = Math.Max(prePage - count, 1);

            IDictionary<string, object> htmlDict;
            for (int page = prePage; page <= lastPage; page++)
            {
                if (page == option.CurrentPage)
                    htmlDict = currentPageLinkHtmlAttributes;
                else htmlDict = pageLinkHtmlAttributes;
                if (page > 1)
                    routeDict[option.PageSegmentName] = page;
                builder.Append(html.RouteLink(page.ToString(),
                    routeDict, htmlDict));
            }

            if (option.TotalPage > 1)
                routeDict[option.PageSegmentName] = option.TotalPage;
            builder.Append(html.RouteLink(option.LastButtonTitle,
                routeDict, firstLastLinkHtmlAttributes));
        }

        private static RouteValueDictionary GetRouteValueDictionary(HtmlHelper html)
        {
            var routeData = html.ViewContext.RouteData;
            var routeDict = new RouteValueDictionary(
                (IDictionary<string, object>)routeData.Values);

            var queryString = html.ViewContext.HttpContext.Request.QueryString;
            foreach (string key in queryString.AllKeys)
                if (!routeDict.ContainsKey(key))
                    routeDict.Add(key, queryString[key]);

            return routeDict;
        }
    }
}
