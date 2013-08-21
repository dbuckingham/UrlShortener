using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UrlShortener.Business.Extensions
{
    public static class DateTimeOffsetExtensionMethods
    {
        public static string ToFriendlyString(this DateTimeOffset dt, string defaultValue = "N/A", string format = "G")
        {
            if (dt > DateTimeOffset.MinValue) return dt.ToString(format);

            return defaultValue;
        }
    }
}
