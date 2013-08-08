using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace UrlShortener.Models
{
    public class UrlModel
    {
        public string Id { get; set; }
        public string Key { get; set; }
        public Uri Url { get; set; }

        [Display(Name="Request Count")]
        public long RequestCount { get; set; }

        public DateTimeOffset Created { get; set; }
        public DateTimeOffset Updated { get; set; }

        [Display(Name="Last Request")]
        public DateTimeOffset LastRequest { get; set; }
    }
}