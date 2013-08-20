using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace UrlShortener.Business.UI.Models
{
    public class UrlDeleteViewModel
    {
        public string Id { get; set; }
        public string Key { get; set; }
        public Uri Url { get; set; }
    }
}
