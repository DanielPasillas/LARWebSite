using System;
using System.Web.Http;
using System.Web.Routing;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Threading;
using System.Net;
using System.Web.Caching;

namespace LARWebSite
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            _SetupRefreshJob();
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);   
        }
        //----------------------------------------------

        /*
        *  Method to keep alive the website.
        *  SRC1: https://stackoverflow.com/questions/30679792/application-keep-alive-in-shared-hosting-with-no-access-to-iis-manager
        *  SRC2: https://www.codeproject.com/Articles/39118/Keep-Your-Website-Alive-Don-t-Let-IIS-Recycle-Your
        */
        private static void _SetupRefreshJob()
        {
            System.Diagnostics.Debugger.Break();
            //remove a previous job
            Action remove = HttpContext.Current.Cache["Refresh"] as Action;
            if (remove is Action)
            {
                HttpContext.Current.Cache.Remove("Refresh");
                remove.EndInvoke(null);
            }
            //get the worker
            Action work = () =>
            {
                while (true)
                {
                    Thread.Sleep(30000);
                    WebClient refresh = new WebClient();
                    try
                    {
                       refresh.UploadString("http://www.laredcazaypesca.com.mx", string.Empty);
                    }
                    catch (Exception ex)
                    {
                        //snip...
                    }
                    finally
                    {
                        refresh.Dispose();
                    }
                }
            };
            work.BeginInvoke(null, null);

            //add this job to the cache
            HttpContext.Current.Cache.Add(
            "Refresh",
            work,
            null,
            Cache.NoAbsoluteExpiration,
            Cache.NoSlidingExpiration,
            CacheItemPriority.Normal,
            (s, o, r) => { _SetupRefreshJob(); }
            );
        }
        //----------------------------------------------


    }
}
