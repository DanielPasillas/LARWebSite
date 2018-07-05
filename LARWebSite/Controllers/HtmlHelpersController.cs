using System;
using System.Collections.Generic;
using System.Data.Entity;
using LARWebSite.Models;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace LARWebSite.Controllers
{
    public class HtmlHelpersController : Controller
    {

        private readonly dbContextLAR _dbContext; 

        public HtmlHelpersController()
        {
            _dbContext = new dbContextLAR();
        }
        //--------------------------------------------------------------------------------

        /*
         *  We are going to use this method to get the products filtered by category.
         *  Every request will have to get the category id and then it will response with HTML format.
         */
        [ActionName("galleryfilter")]
        [HttpPost]
        public async Task<ActionResult> GetHtmlResponseByCategoryId(int id/*Id category*/, string category)
        {
            var _listOfProducts = await _dbContext.products.Where(m => m.idCategory == id).ToListAsync();

            if (_listOfProducts == null)
                throw new HttpException(404, "No records found");

            List<ItemProductModel> _viewModelProducts = new List<ItemProductModel>();

            foreach(var _product in _listOfProducts)
            {
                _viewModelProducts.Add(new ItemProductModel(_product));
            }
            //-------------------------------------

            //Save the name of the category
            ViewBag.CategoryName = category;

            //Save the Category's id.
            ViewBag.CategoryId = id;

            return PartialView("GetHtmlResponseByCategoryId", _viewModelProducts);
        }
        //--------------------------------------------------------------------------------

    }
}