using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UrlShortener.Business.Helpers
{
    public class ConfigurationHelper : ConfigurationHelperBase
    {
        private const string NewLinkMaximumAgeAppSetting = "newLink.maximumAgeInDays";
        private const int NewLinkMaximumAgeDefault = 1;

        public static TimeSpan NewLinkMaximumAge
        {
            get
            {
                int maximumAgeInDays = GetValue<int>(NewLinkMaximumAgeAppSetting, NewLinkMaximumAgeDefault);

                return TimeSpan.FromDays(maximumAgeInDays);
            }
        }
    }
}
