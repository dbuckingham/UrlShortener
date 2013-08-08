using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Raven.Client.Document;
using UrlShortener.Builders.Home;
using UrlShortener.Models;
using UrlShortener.Models.Home;

namespace UrlShortener.Controllers
{
    public class HomeController : Controller
    {
        private DocumentStore _documentStore = null;

        public HomeController()
        {
            // TODO - Inject DocumentStore

            _documentStore = new DocumentStore { ConnectionStringName = "localRavenDB" };
            _documentStore.Initialize();
            _documentStore.Conventions.IdentityPartsSeparator = "-";
        }

        public ActionResult Index()
        {
            var modelBuilder = new IndexModelBuilder(_documentStore);
            var model = modelBuilder.Build();

            return View(model);
        }

        //public ActionResult About()
        //{
        //    ViewBag.Message = "Your application description page.";

        //    return View();
        //}

        //public ActionResult Contact()
        //{
        //    ViewBag.Message = "Your contact page.";

        //    return View();
        //}
    }
}