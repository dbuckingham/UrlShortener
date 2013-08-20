using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Raven.Client;
using UrlShortener.Business.Domain;

namespace UrlShortener.Business.UI.Models
{
    public class UrlIndexViewModel
    {
        public IEnumerable<ShortLink> Urls { get; set; }
        public RavenQueryStatistics Statistics { get; set; }
    }
}
