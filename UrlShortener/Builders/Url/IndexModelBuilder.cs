using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Raven.Client;
using UrlShortener.Models;
using UrlShortener.Models.Url;

namespace UrlShortener.Builders.Url
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
            List<UrlModel> links = null;
            RavenQueryStatistics statistics = null;

            try
            {
                using (var session = _documentStore.OpenSession())
                {
                    var items = session.Query<UrlModel>()
                        .Statistics(out statistics)
                        //.Customize(x => x.WaitForNonStaleResults(TimeSpan.FromSeconds(5)))
                        .OrderBy(url => url.Key)
                        .ToList();

                    links = items;
                }
            }
            catch (Exception)
            {
            }

            var model = new IndexModel()
            {
                Urls = links ?? new List<UrlModel>(),
                Statistics = statistics
            };

            return model;
        }
    }
}