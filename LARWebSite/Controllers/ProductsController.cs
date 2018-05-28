using System;
using LARWebSite.Utilerias;
using System.Data.Entity;
using LARWebSite.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace LARWebSite.Controllers
{
    public class ProductsController : Controller
    {
        private readonly dbContextLAR _dbContext;

        public ProductsController()
        {
            _dbContext = new dbContextLAR();
        }
        //----------------------------------------------

        // GET: Products
        public ActionResult Index()
        {
            return View();
        }

        [ActionName("detail")]
        public async Task<ActionResult> ProductDetail(int id, string code, string name)
        {
            //Get product information.
            var _product = await _dbContext.ProductByCodeAndId(id, code);

            if (_product == null)
                throw new HttpException(404, "Error 404, product not found.");

            ProductModel _viewModelProduct = new ProductModel(_product);

            return View("ProductDetail", _viewModelProduct);
        }
        //----------------------------------------------
    }
}