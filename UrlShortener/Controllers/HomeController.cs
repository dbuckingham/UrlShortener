using System;
using System.Web.Mvc;
using Raven.Client;
using UrlShortener.Business.UI.Builders;

namespace UrlShortener.Controllers
{
    public partial class HomeController : Controller
    {
        private readonly IDocumentStore _documentStore = null;

        public HomeController(IDocumentStore documentStore)
        {
            if (documentStore == null) throw new ArgumentNullException("documentStore");

            _documentStore = documentStore;
        }

        public virtual ActionResult Index()
        {
            var modelBuilder = new HomeIndexViewModelBuilder(_documentStore);
            var model = modelBuilder.Build();

            return View(model);
        }
    }
}