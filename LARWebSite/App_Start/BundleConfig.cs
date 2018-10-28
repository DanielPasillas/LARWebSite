using System.Web;
using System.Web.Optimization;

namespace LARWebSite
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Assets/js/jquery.min.js",
                        "~/Assets/js/jquery.shuffle.min.js",
                        "~/Assets/js/bootstrap.min.js",
                      "~/Assets/js/custom.js",
                      "~/Assets/js/jquery.easing.min.js",
                      "~/Assets/js/owl.carousel.min.js",
                      "~/Assets/js/preloader.js",
                      "~/Assets/js/megamenu.js",
                      "~/Assets/js/scrolling-nav.js"));

            //Bundle ProductDetail
            bundles.Add(new ScriptBundle("~/bundles/jsproductdetail").Include(
                        "~/Assets/js/red/product-detail/ProductDetail.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            //Bundle for Index Section.
            bundles.Add(new ScriptBundle("~/bundles/index").Include(
                       "~/Assets/js/flick-slider.js"));

            // Utilice la versión de desarrollo de Modernizr para desarrollar y obtener información. De este modo, estará
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            //bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        //"~/Scripts/modernizr-*"));

           

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Assets/css/bootstrap.min.css",
                      "~/Assets/css/colors.css",
                      "~/Assets/css/animate.css",
                      "~/Assets/css/owl.carousel.css",
                      "~/Assets/css/owl.theme.css",
                      "~/Assets/css/style.css",
                      "~/Assets/css/awesome.css",
                      "~/Assets/css/megamenu.css",
                      "~/Assets/css/linear-icons.css",
                      "~/Assets/css/site.css"));

            //Enable web optimizations. 
            BundleTable.EnableOptimizations = true;
        }
    }
}
