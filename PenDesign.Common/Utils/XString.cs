using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

public static class XString
{
    public static String ToAscii(this String s)
    {
        string[] values = {"[áàảãạăắằẳẵặâấầẩẫậ]",
                   "đ",
                   "[éèẻẽẹêếềểễệ]",
                   "[íìỉĩị]",
                   "[óòỏõọôốồổỗộơớờởỡợ]",
                   "[úùủũụưứừửữự]",
                   "[ýỳỷỹỵ]",
                   "[\\s]+",
                   "[\"\'\\;\\.]"
              };
        string[] key = { "a", "d", "e", "i", "o", "u", "y", "-", "" };

        for (int i = 0; i < values.Length; i++)
        {
            s = Regex.Replace(s.ToLower(), values[i], key[i]);
        }
        return s;
    }
}