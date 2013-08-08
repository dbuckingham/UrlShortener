using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Raven.Client;
using Raven.Client.Document;
using UrlShortener.Builders.Home;
using UrlShortener.Models;
using UrlShortener.Models.Home;

namespace UrlShortener.Controllers
{
    public class HomeController : Controller
    {
        private readonly IDocumentStore _documentStore = null;

        public HomeController(IDocumentStore documentStore)
        {
            if (documentStore == null) throw new ArgumentNullException("documentStore");

            _documentStore = documentStore;
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