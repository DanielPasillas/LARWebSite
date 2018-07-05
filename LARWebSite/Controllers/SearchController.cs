using System;
using System.Data.Entity;
using System.Collections.Generic;
using LARWebSite.Models;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Threading.Tasks;

namespace LARWebSite.Controllers
{
    public class SearchController : Controller
    {
        private readonly dbContextLAR _dbContext;

        public SearchController()
        {
            _dbContext = new dbContextLAR();
        }
        //-----------------------

        [ActionName("category")]
        public async Task<ActionResult> Category(int id, string category)
        {
            ViewBag.Title = category;

            var _products = await _dbContext.products.Where(m => m.idCategory == id).Take(9).ToListAsync();

            List<ItemProductModel> _viewModelProduct = new List<ItemProductModel>();

            foreach (var _product in _products)
            {
                _viewModelProduct.Add(new ItemProductModel(_product));
            }

            return View("Category", _viewModelProduct);
        }
        //----------------------------
    }
}