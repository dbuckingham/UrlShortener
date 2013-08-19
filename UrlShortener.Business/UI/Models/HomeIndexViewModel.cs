using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UrlShortener.Business.UI.Models
{
    public class HomeIndexViewModel
    {
        public IEnumerable<NewShortLink> NewLinks { get; set; }
        public IEnumerable<PopularShortLink> PopularLinks { get; set; }
    }
}
