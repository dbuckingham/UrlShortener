using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Raven.Client;
using UrlShortener.Business.Domain;
using UrlShortener.Business.Extensions;
using UrlShortener.Business.Helpers;

namespace UrlShortener.Business.UI.Models
{
    public class UrlIndexViewModel
    {
        public IEnumerable<ShortLinkDetails> Urls { get; set; }
        public RavenQueryStatistics Statistics { get; set; }
    }

    public class ShortLinkDetails
    {
        public string Id { get; set; }

        public string Key { get; set; }
        public Uri Url { get; set; }

        [DisplayName("Request Count")]
        public long RequestCount { get; private set; }

        public DateTimeOffset Created { get; set; }
        public DateTimeOffset Updated { get; set; }

        [DisplayName("Last Request")]
        public DateTimeOffset LastRequest { get; private set; }

        public string GetCreatedTimestamp()
        {
            return Created.ToFriendlyString();
        }

        public string GetUpdatedTimestamp()
        {
            return Updated.ToFriendlyString();
        }

        public string GetLastRequestTimestamp()
        {
            return LastRequest.ToFriendlyString();
        }

        public bool IsNew()
        {
            TimeSpan age = DateTimeOffset.UtcNow - Created;

            if (age <= ConfigurationHelper.NewLinkMaximumAge) return true;

            return false;
        }
    }
}
