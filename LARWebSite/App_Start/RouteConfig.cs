using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace LARWebSite
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            //Custom Route for list the products by Id Brand.
            routes.MapRoute(
                name: "ProductsbyBrand.",
                url: "{controller}/{action}/{id}/{brand}",
                defaults: new { controller = "Product", action = "Brand", id = UrlParameter.Optional}
            );

            //Custom Route for product detail
            routes.MapRoute(
                name: "ProductDetailPath",
                url: "{controller}/{action}/{id}/{code}/{name}",
                defaults: new { controller = "Product", action = "Detail", id = UrlParameter.Optional, code = UrlParameter.Optional, name = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Default",
                url: "",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
