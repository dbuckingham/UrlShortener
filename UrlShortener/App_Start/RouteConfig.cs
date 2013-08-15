using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace UrlShortener
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            //routes.MapRoute(
            //    name: "Go",
            //    url: "Go/{key}",
            //    defaults: new { controller = "Go", action = "Index" }
            //);

            routes.MapRoute(
                "Go",
                "Go/{key}",
                MVC.Go.Index()
                );

            //routes.MapRoute(
            //    name: "Default",
            //    url: "{controller}/{action}/{id}",
            //    defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            //);

            routes.MapRoute(
                "Default",
                "{controller}/{action}/{id}",
                MVC.Home.Index().AddRouteValue("id", "anything")
                );
        }
    }
}
