using System.Web.Mvc;
using System.Web.Routing;

namespace PokerBot
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{action}/{id}",
                defaults: new { controller = "Poker", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
