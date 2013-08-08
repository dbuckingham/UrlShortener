using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Raven.Client.Document;
using UrlShortener.Models;

namespace UrlShortener.Controllers
{
    public class GoController : Controller
    {
        private DocumentStore _documentStore = null;

        public GoController()
        {
            // TODO - Inject DocumentStore

            _documentStore = new DocumentStore() { ConnectionStringName = "localRavenDB" };
            _documentStore.Initialize();
            _documentStore.Conventions.IdentityPartsSeparator = "-";
        }

        //
        // GET: /Go/key
        public ActionResult Index(string key)
        {
            try
            {
                UrlModel urlModel = null;

                using (var session = _documentStore.OpenSession())
                {
                    var results = from urls in session.Query<UrlModel>()
                                  where urls.Key == key
                                  select urls;

                    urlModel = results.FirstOrDefault();

                    if (urlModel != null)
                    {
                        urlModel.RequestCount++;
                        urlModel.LastRequest = DateTimeOffset.UtcNow;
                        session.Store(urlModel);
                        session.SaveChanges();
                    }
                }

                if (urlModel == null) return View();

                return Redirect(urlModel.Url.ToString());
            }
            catch (Exception)
            {
                return View();
            }
        }
    }
}