using System;
using System.Text;
using System.Web.Mvc;

namespace PenDesign.Common.HelperMethod
{
    public static class HtmlAttributesClass
    {
        public static MvcHtmlString HtmlAttributes(this HtmlHelper helper,
            object htmlAttributes, bool replaceUnderscoreWithMinusSign = false)
        {
            if (htmlAttributes == null) return new MvcHtmlString("");

            StringBuilder builder = new StringBuilder();
            foreach (var property in htmlAttributes.GetType().GetProperties())
                if (property.CanRead)
                {
                    var value = property.GetValue(htmlAttributes);

                    if (value != null)
                    {
                        builder.Append(builder.Length == 0 ? "" : " ")
                            .Append(replaceUnderscoreWithMinusSign ? property.Name.Replace('_', '-') : property.Name)
                            .Append("=\"")
                            .Append(value)
                            .Append("\"");
                    }
                }
            return new MvcHtmlString(builder.ToString());
        }
    }
}
