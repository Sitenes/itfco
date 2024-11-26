using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Toplearn.Core.Convertors
{
    public static class FixText
    {
        public static string FixEmail(string txt)
        {
            return txt.Trim().ToLower();
        }
        public static string ToPrice(this string value)
        {
            return long.Parse(value).ToString("#,0");
        }

        public static string ToPrice(this long value)
        {
            return value.ToString("#,0");
        }

    }
}
