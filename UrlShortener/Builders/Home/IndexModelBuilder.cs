using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using Raven.Client.Document;
using UrlShortener.Models;
using UrlShortener.Models.Home;

namespace UrlShortener.Builders.Home
{
    public class IndexModelBuilder
    {
        private DocumentStore _documentStore = null;

        public IndexModelBuilder(DocumentStore documentStore)
        {
            if (documentStore == null) throw new ArgumentNullException("documentStore");

            _documentStore = documentStore;
        }

        public IndexModel Build()
        {
            int count = 5;

            List<UrlModel> recent = null;
            List<UrlModel> popular = null;

            using (var session = _documentStore.OpenSession())
            {
                recent = session.Query<UrlModel>().OrderByDescending(url => url.Created).Take(count).ToList();
                popular = session.Query<UrlModel>().OrderByDescending(url => url.RequestCount).Take(count).ToList();
            }

            var model = new IndexModel()
            {
                RecentUrls = recent,
                PopularUrls = popular
            };

            return model;
        }
    }
}