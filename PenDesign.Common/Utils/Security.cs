using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace PenDesign.Common.Utils
{
    public static class Security
    {
        public static String ToBase64(this String s)
        {
            var data = Encoding.UTF8.GetBytes(s);
            return Convert.ToBase64String(data);
        }

        public static String FromBase64(this String s)
        {
            var data = Convert.FromBase64String(s);
            return Encoding.UTF8.GetString(data);
        }

        public static String ToMD5(this String s)
        {
            var data = Encoding.UTF8.GetBytes(s);
            var md5 = MD5.Create().ComputeHash(data);
            return Convert.ToBase64String(md5);
        }
    }
}