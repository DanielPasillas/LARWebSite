using System;
using LARWebSite.Utilerias;
using System.Data.Entity;
using LARWebSite.Models;
using LARWebSite.ViewModels;
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
        //----------------------------------------------

        [ActionName("detail")]
        public async Task<ActionResult> Detail(int id, string code, string name)
        {
            //Get product information.
            var _product = await _dbContext.ProductByCodeAndId(id, code);

            if (_product == null)
                throw new HttpException(404, "Error 404, product not found.");

            ProductModel _viewModelProduct = new ProductModel(_product);

            //Get the related products.
            List<ItemProductModel> _viewModelProductList = new List<ItemProductModel>();

            //Get the list of products.
            var _productList = await _dbContext.GetRelatedProducts(_viewModelProduct.IdMarca, _viewModelProduct.IdCategoria, _viewModelProduct.IdSubCategoria);

            foreach (var _listProducts in _productList)
            {
                _viewModelProductList.Add(new ItemProductModel(_listProducts));
            }

            //View model for the Product Detail Module.
            ProductDetailViewModel _viewModelDetail = new ProductDetailViewModel()
            {
                Producto = _viewModelProduct,
                SliderProducts = _viewModelProductList
            };

            return View("Detail", _viewModelDetail);
        }
        //----------------------------------------------

        [ActionName("brand")]
        public async Task<ActionResult> Brand(int id, string brand)
        {
            //Get the products filtered by the brand id.
            var _products = await _dbContext.ProductsByIdBrand(id);

            ViewBag.TitleBrand = brand;

            return View("Brand", _products);
        }
        //----------------------------------------------

        /*
         * We will retrieve the list if products by using the 
         * Category Id as parameter.
         */
        [ActionName("category")]
        public async Task<ActionResult> GetProductsByCategory(int id, string category)
        {
            var _products = await _dbContext.products.Where(m => m.idCategory == id).ToListAsync();

            List<ItemProductModel> _viewModelProduct = new List<ItemProductModel>();

            foreach(var _product in _products)
            {
                _viewModelProduct.Add(new ItemProductModel(_product));
            }

            ViewBag.TitleCategory = category;

            return View("GetProductsByCategory", _viewModelProduct);
        }
        //----------------------------------------------

    }
}