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

            //Custom Route for searching products. For pagination implmementation.
            /*routes.MapRoute(
                name: "ProductSearchPagination",
                url: "products/{action}/{page}/{keywords}",
                defaults: new { controller = "Products", action = "SearchFilterProducts", page = UrlParameter.Optional, keywords = UrlParameter.Optional }
            );
            */

            //Custom Route for list accessing to the categories path.
            routes.MapRoute(
                name: "ProductsByCategories",
                url: "search/{action}/{page}/{id}/{category}",
                defaults: new { controller = "Search", action = "Category", page = UrlParameter.Optional ,id = UrlParameter.Optional, category = UrlParameter.Optional }
            );

            //Custom Route to show the products by brand id.
            routes.MapRoute(
                name: "ProductsBySubCategory",
                url: "search/{action}/{page}/{id}/{nameSubCategory}",
                defaults: new { controller = "Search", action = "GetProductsBySubCategory", page = UrlParameter.Optional, id = UrlParameter.Optional, nameSubCategory = UrlParameter.Optional }
            );

            //Custom Route to show the products by brand id.
            routes.MapRoute(
                name: "ProductsByBrandId",
                url: "search/{action}/{page}/{id}/{name}",
                defaults: new { controller = "Search", action = "BrandProductSearch", page = UrlParameter.Optional, id = UrlParameter.Optional, name = UrlParameter.Optional }
            );

            //Custom Route for product detail
            routes.MapRoute(
                name: "ProductDetailPath",
                url: "products/{action}/{id}/{code}/{name}",
                defaults: new { controller = "Products", action = "Detail", id = UrlParameter.Optional, code = UrlParameter.Optional, name = UrlParameter.Optional }
            );

            // ** Default Path ** //
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
