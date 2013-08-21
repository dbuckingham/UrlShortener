using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using UrlShortener.Business.Extensions;
using UrlShortener.Business.Helpers;

namespace UrlShortener.Business.UI.Models
{
    public class NewShortLink
    {
        public string Key { get; set; }
        public Uri Url { get; set; }
        public DateTimeOffset Created { get; set; }

        public bool IsNew()
        {
            TimeSpan age = DateTimeOffset.UtcNow - Created;

            if (age <= ConfigurationHelper.NewLinkMaximumAge) return true;

            return false;
        }

        public string GetCreatedTimestamp()
        {
            return Created.ToFriendlyString();
        }
    }
}
