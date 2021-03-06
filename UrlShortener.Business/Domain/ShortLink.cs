﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UrlShortener.Business.Domain
{
    public class ShortLink
    {
        public string Id { get; set; }
        
        public string Key { get; set; }
        public Uri Url { get; set; }

        public long RequestCount { get; private set; }

        public DateTimeOffset Created { get; set; }
        public DateTimeOffset Updated { get; set; }
        public DateTimeOffset LastRequest { get; private set; }

        public void Request()
        {
            RequestCount++;
            LastRequest = DateTimeOffset.UtcNow;
        }
    }
}
