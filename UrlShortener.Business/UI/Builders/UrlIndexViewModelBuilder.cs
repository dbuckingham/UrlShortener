using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Raven.Client;
using UrlShortener.Business.Domain;
using UrlShortener.Business.UI.Models;

namespace UrlShortener.Business.UI.Builders
{
    public class UrlIndexViewModelBuilder
    {
        private IDocumentStore _documentStore = null;

        public UrlIndexViewModelBuilder(IDocumentStore documentStore)
        {
            if (documentStore == null) throw new ArgumentNullException("documentStore");

            _documentStore = documentStore;
        }

        public UrlIndexViewModel Build()
        {
            List<ShortLink> links = null;
            RavenQueryStatistics statistics = null;

            try
            {
                using (var session = _documentStore.OpenSession())
                {
                    var items = session.Query<ShortLink>()
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

            var model = new UrlIndexViewModel()
            {
                Urls = links ?? new List<ShortLink>(),
                Statistics = statistics
            };

            return model;
        }
    }
}
