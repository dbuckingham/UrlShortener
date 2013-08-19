using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UrlShortener.Business.UI.Models
{
    public class PopularShortLink
    {
        public string Key { get; set; }
        public Uri Url { get; set; }
        public long RequestCount { get; set; }
    }
}
