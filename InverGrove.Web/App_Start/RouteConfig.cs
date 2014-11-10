using System.Web.Mvc;
using System.Web.Routing;

namespace InverGrove.Web
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("Content/{*pathInfo}"); // prevents validation routines from returning false from here and confuse the router
            routes.IgnoreRoute("bundles/{*pathInfo}");
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            var route = routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }, namespaces: new[] { "InverGrove.Web.Controllers" }
            );

            route.DataTokens["UseNamespaceFallback"] = false;
        }
    }
}
