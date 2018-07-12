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

            //Custom Route for list products by SubCategory.
            routes.MapRoute(
                name: "SubCategorySearchFilter",
                url: "search/{action}/{id}/{nameSubCategory}",
                defaults: new { controller = "Search", action = "filtersubcategory", id = UrlParameter.Optional, nameSubCategory = UrlParameter.Optional }
            );

            //Custom Route for list accessing to the categories path.
            routes.MapRoute(
                name: "ProductsByCategories",
                url: "search/{action}/{id}/{category}",
                defaults: new { controller = "Search", action = "Category", id = UrlParameter.Optional, category = UrlParameter.Optional }
            );

            //Custom Route for list the products by Id Brand.
            routes.MapRoute(
                name: "ProductsbyBrand.",
                url: "products/{action}/{id}/{brand}",
                defaults: new { controller = "Products", action = "Brand", id = UrlParameter.Optional}
            );

            //Custom Route for product detail
            routes.MapRoute(
                name: "ProductDetailPath",
                url: "products/{action}/{id}/{code}/{name}",
                defaults: new { controller = "Products", action = "Detail", id = UrlParameter.Optional, code = UrlParameter.Optional, name = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
