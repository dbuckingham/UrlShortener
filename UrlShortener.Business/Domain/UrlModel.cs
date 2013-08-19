using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UrlShortener.Business.Domain
{
    public class UrlModel
    {
        public string Id { get; set; }
        public string Key { get; set; }
        public Uri Url { get; set; }

        public long RequestCount { get; set; }

        public DateTimeOffset Created { get; set; }
        public DateTimeOffset Updated { get; set; }

        public DateTimeOffset LastRequest { get; set; }
    }
}
