using System;
using System.Data.Entity;
using LARWebSite.Models;
using LARWebSite.ViewModels;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Threading.Tasks;

namespace LARWebSite.Controllers
{
    public class HomeController : Controller
    {

        private readonly dbContextLAR _dbContext;

        public HomeController()
        {
            _dbContext = new dbContextLAR();
        }
        //---------------------

        public async Task<ActionResult> Index()
        {
            var _slides = await _dbContext.slider.ToListAsync();

            List<SliderModel> _carousel = new List<SliderModel>();

            foreach(var carousel in _slides)
            {
                _carousel.Add(new SliderModel(carousel));
            }

            IndexViewModel _viewModel = new IndexViewModel()
            {
                Carousel = _carousel
            };

            return View(_viewModel);
        }
        //---------------------------

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