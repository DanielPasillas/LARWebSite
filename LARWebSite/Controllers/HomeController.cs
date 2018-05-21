using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LARWebSite.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        [ActionName("contacto")]
        public ActionResult Contact()
        {
            return View("Contacto");
        }
        //-------------------------

        [ActionName("about")]
        public ActionResult About()
        {
            return View();
        }
        //-------------------------

        [ActionName("offers")]
        public ActionResult Offers()
        {
            return View();
        }
        //-------------------------
    }
}