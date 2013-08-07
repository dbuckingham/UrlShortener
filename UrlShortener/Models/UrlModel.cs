using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UrlShortener.Models
{
    public class UrlModel
    {
        public string Id { get; set; }
        public string Key { get; set; }
        public Uri Url { get; set; }
        public long VisitCount { get; set; }
    }
}