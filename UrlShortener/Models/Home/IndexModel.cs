using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UrlShortener.Models.Home
{
    public class IndexModel
    {
        public UrlModel NewUrl { get; set; }
        public IEnumerable<UrlModel> RecentUrls { get; set; }
        public IEnumerable<UrlModel> PopularUrls { get; set; }
    }
}