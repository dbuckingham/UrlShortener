using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Raven.Client;
using UrlShortener.Business.UI.Models;

namespace UrlShortener.Business.UI.Builders
{
    public class HomeIndexViewModelBuilder
    {
        private const int DefaultCount = 5;

        public int NewLinksCount { get; set; }
        public int PopularLinksCount { get; set; }

        private IDocumentStore _documentStore = null;

        public HomeIndexViewModelBuilder(IDocumentStore documentStore)
        {
            if (documentStore == null) throw new ArgumentNullException("documentStore");

            _documentStore = documentStore;

            NewLinksCount = DefaultCount;
            PopularLinksCount = DefaultCount;
        }

        public HomeIndexViewModel Build()
        {
            var model = new HomeIndexViewModel();

            try
            {
                using (var session = _documentStore.OpenSession())
                {
                    var latestLinks = 
                        session.Query<ShortLink>()
                            .OrderByDescending(link => link.Created)
                            .Take(NewLinksCount)
                            .ToList();

                    var popularLinks =
                        session.Query<ShortLink>()
                            .OrderByDescending(link => link.RequestCount)
                            .Take(PopularLinksCount)
                            .ToList();

                    model.NewLinks = Mapper.Map<IEnumerable<ShortLink>, IEnumerable<NewShortLink>>(latestLinks);
                    model.PopularLinks = Mapper.Map<IEnumerable<ShortLink>, IEnumerable<PopularShortLink>>(popularLinks);
                }
            }
            catch (Exception)
            {
            }

            return model;
        }
    }
}
