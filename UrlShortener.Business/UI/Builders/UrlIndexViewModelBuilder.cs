using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
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
            IEnumerable<ShortLink> shortLinks = null;
            RavenQueryStatistics statistics = null;
            UrlIndexViewModel model = null;

            try
            {
                using (var session = _documentStore.OpenSession())
                {
                    shortLinks = session.Query<ShortLink>()
                        .Statistics(out statistics)
                        //.Customize(x => x.WaitForNonStaleResults(TimeSpan.FromSeconds(5)))
                        .OrderBy(url => url.Key)
                        .ToList();
                }

                var urls = Mapper.Map<IEnumerable<ShortLinkDetails>>(shortLinks);

                model = new UrlIndexViewModel()
                {
                    Urls = urls ?? new List<ShortLinkDetails>(),
                    Statistics = statistics
                };
            }
            catch (Exception)
            {
            }

            return model;
        }
    }
}
