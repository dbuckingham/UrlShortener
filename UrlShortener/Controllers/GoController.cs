using System;
using System.Linq;
using System.Web.Mvc;
using AutoMapper;
using Raven.Client;
using UrlShortener.Business;
using UrlShortener.Business.Domain;
using UrlShortener.Business.UI.Models;

namespace UrlShortener.Controllers
{
    public partial class GoController : Controller
    {
        private readonly IDocumentStore _documentStore = null;

        public GoController(IDocumentStore documentStore)
        {
            if (documentStore == null) throw new ArgumentNullException("documentStore");

            _documentStore = documentStore;
        }

        //
        // GET: /Go/key
        public virtual ActionResult Index(string key)
        {
            try
            {
                ShortLink shortLink = null;

                using (var session = _documentStore.OpenSession())
                {
                    var results = from urls in session.Query<ShortLink>()
                                  where urls.Key == key
                                  select urls;

                    shortLink = results.FirstOrDefault();

                    if (shortLink != null)
                    {
                        shortLink.Request();

                        session.Store(shortLink);
                        session.SaveChanges();
                    }
                }

                if (shortLink == null) return View();

                return Redirect(shortLink.Url.ToString());
            }
            catch (Exception)
            {
                return View();
            }
        }
    }
}