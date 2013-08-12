using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Raven.Abstractions.Data;
using Raven.Client;
using Raven.Client.Document;
using Raven.Client.Extensions;
using UrlShortener.Controllers;

namespace UrlShortener.App_Start
{
    public class UrlShortenerControllerFactory : DefaultControllerFactory
    {
        private static IDocumentStore _documentStore = null;

        public UrlShortenerControllerFactory()
        {
            if (_documentStore == null)
            {
                _documentStore = new DocumentStore() { ConnectionStringName = "localRavenDB" };
                _documentStore.Conventions.IdentityPartsSeparator = "-";
                _documentStore.Initialize();

                Glimpse.RavenDb.Profiler.AttachTo(_documentStore as DocumentStore);
            }
        }

        protected override IController GetControllerInstance(RequestContext requestContext, Type controllerType)
        {
            if (controllerType == typeof(HomeController)) return new HomeController(_documentStore);
            if (controllerType == typeof(UrlController)) return new UrlController(_documentStore);
            if (controllerType == typeof(GoController)) return new GoController(_documentStore);

            return base.GetControllerInstance(requestContext, controllerType);
        }
    }
}