using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace PenDesign.Common.Utils
{
    public static class Cookies
    {
        /// <summary>
        /// Đọc và gán giá trị mặc định của cookie
        /// </summary>
        /// <param name="key"> Key của cookie</param>
        /// <param name="defaultValue"> Gán giá trị mặc định cho Cookie</param>
        /// <returns></returns>
        public static string ReadCookie(string key, string defaultValue)
        {
            var LanguageId = defaultValue;
            var cookies = HttpContext.Current.Request.Cookies[key].Value;
            if (cookies != null)
            {
                LanguageId = cookies;
            }
            return LanguageId;
        }
    }
}
