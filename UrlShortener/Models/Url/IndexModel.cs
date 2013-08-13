using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Raven.Client;

namespace UrlShortener.Models.Url
{
    public class IndexModel
    {
        public IEnumerable<UrlModel> Urls { get; set; }
        public RavenQueryStatistics Statistics { get; set; }
    }
}