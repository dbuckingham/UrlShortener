using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Raven.Client;
using Raven.Client.Document;
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
                _documentStore.Initialize();
                _documentStore.Conventions.IdentityPartsSeparator = "-";
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