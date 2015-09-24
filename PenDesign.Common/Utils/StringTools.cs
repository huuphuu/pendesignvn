using System;
using System.Collections.Generic;
using System.Text;

namespace PenDesign.Common.Utils
{
    public class StringTools
    {
        public static string PriceString(decimal? price)
        {
            string result = price == null ? "Chưa có giá" : string.Format("{0:0,0}đ", price);
            if (result.Length > 2 && result[0] == '0')
                result = result.Substring(1);
            return result;
        }

        public static string DateTimeString(DateTime? date, string nullMessage = "")
        {
            if (date == null) return nullMessage;

            DateTime d = (DateTime)date;
            string dayOfWeek;
            switch (d.DayOfWeek)
            {
                case DayOfWeek.Monday:
                    dayOfWeek = "Thứ 2"; break;
                case DayOfWeek.Tuesday:
                    dayOfWeek = "Thứ 3"; break;
                case DayOfWeek.Wednesday:
                    dayOfWeek = "Thứ 4"; break;
                case DayOfWeek.Thursday:
                    dayOfWeek = "Thứ 5"; break;
                case DayOfWeek.Friday:
                    dayOfWeek = "Thứ 6"; break;
                case DayOfWeek.Saturday:
                    dayOfWeek = "Thứ 7"; break;
                default:
                    dayOfWeek = "Chủ nhật"; break;
            }
            return string.Format(d.ToString("HH:mm:ss, {0} dd/MM/yyyy"), dayOfWeek + ", ngày");
        }

        public static string DateTimeFormat(DateTime? date, string formatString = "dd/MM/yyyy")
        {
            return date == null ? "" : ((DateTime)date).ToString(formatString);
        }

        public static string TrimString(string str, int maxLength, string endString = " ...")
        {
            if (str == null) return "";
            if (str.Length > maxLength)
                str = str.Substring(0, maxLength) + endString;
            return str;
        }
    }
}
