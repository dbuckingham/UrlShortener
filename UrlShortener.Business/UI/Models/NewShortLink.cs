using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using UrlShortener.Business.Extensions;

namespace UrlShortener.Business.UI.Models
{
    public class NewShortLink
    {
        public string Key { get; set; }
        public Uri Url { get; set; }
        public DateTimeOffset Created { get; set; }

        private readonly TimeSpan _maximumAge = TimeSpan.FromDays(1);

        public bool IsNew()
        {
            TimeSpan age = DateTimeOffset.UtcNow - Created;

            if (age <= _maximumAge) return true;

            return false;
        }

        public string GetCreatedTimestamp()
        {
            return Created.ToFriendlyString();
        }
    }
}
