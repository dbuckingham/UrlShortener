using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UrlShortener.Business.Extensions;

namespace UrlShortener.Business.UI.Models
{
    public class UrlDetailsViewModel
    {
        public string Id { get; set; }
        public string Key { get; set; }
        public Uri Url { get; set; }

        [DisplayName("Request Count")]
        public long RequestCount { get; set; }

        public DateTimeOffset Created { get; set; }
        public DateTimeOffset Updated { get; set; }

        [DisplayName("Last Request")]
        public DateTimeOffset LastRequest { get; set; }

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
    }
}
