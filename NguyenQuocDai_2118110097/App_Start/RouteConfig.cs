using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace NguyenQuocDai_2118110097
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                "Product Details",
                "san-pham/{metatitle}-{id}",
                new { controller = "Product", action = "Details", id = UrlParameter.Optional },
                new[] { "NguyenQuocDai_2118110097.Controllers" }
            );
            routes.MapRoute(
                "Default",
                "{controller}/{action}/{id}",
                new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                new[] { "NguyenQuocDai_2118110097.Controllers" }
            );
        }
    }
}
