using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using Raven.Client;
using Raven.Client.Document;
using UrlShortener.Models;
using UrlShortener.Models.Home;

namespace UrlShortener.Builders.Home
{
    public class IndexModelBuilder
    {
        private IDocumentStore _documentStore = null;

        public IndexModelBuilder(IDocumentStore documentStore)
        {
            if (documentStore == null) throw new ArgumentNullException("documentStore");

            _documentStore = documentStore;
        }

        public IndexModel Build()
        {
            int count = 5;

            List<UrlModel> recent = null;
            List<UrlModel> popular = null;

            try
            {
                using (var session = _documentStore.OpenSession())
                {
                    recent = session.Query<UrlModel>().OrderByDescending(url => url.Created).Take(count).ToList();
                    popular = session.Query<UrlModel>().OrderByDescending(url => url.RequestCount).Take(count).ToList();
                }
            }
            catch (Exception e)
            {
            }

            var model = new IndexModel()
            {
                RecentUrls = recent ?? new List<UrlModel>() ,
                PopularUrls = popular ?? new List<UrlModel>()
            };

            return model;
        }
    }
}