using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace UrlShortener.Business.UI.Models
{
    public class NewShortLink
    {
        public string Key { get; set; }
        public Uri Url { get; set; }
        public DateTimeOffset Created { get; set; }
    }
}
