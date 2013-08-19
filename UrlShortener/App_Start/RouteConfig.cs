using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Raven.Client.Linq;

namespace UrlShortener
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                "Go",
                "Go/{key}",
                MVC.Go.Index()
                );

            routes.MapRoute(
                "Default",
                "{controller}/{action}/{id}",
                MVC.Home.Index().AddRouteValue("id", "")
                );
        }
    }
}
