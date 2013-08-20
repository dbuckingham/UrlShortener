using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UrlShortener.Business.UI.Models
{
    public class UrlEditViewModel
    {
        public string Id { get; set; }
        public string Key { get; set; }
        public Uri Url { get; set; }
    }
}
