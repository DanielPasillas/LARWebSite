using System;
using LARWebSite.Utilerias;
using System.Data.Entity;
using PagedList;
using PagedList.Mvc;
using LARWebSite.Models;
using LARWebSite.ViewModels;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Net;

namespace LARWebSite.Controllers
{
    public class ProductsController : Controller
    {
        private readonly dbContextLAR _dbContext;

        private const int _pageSize = 9;

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

            //We convert the product name to SEO URL, after that, we will make a validation.
            string _expectedName = _product.nameProduct.ToSeoUrl();

            //Validate if the expected name is actually the same that the given by the url.
            string actualName = (name ?? "").ToLower();
            
            if( _expectedName != actualName )
            {
                //If the name is not the same, we will redirect to the right location by passing the name in a SEO format.
                return RedirectToActionPermanent("detail", "products", new { id = _product.idProduct, code = _product.keyProduct, name = _expectedName });
            }

            ProductModel _viewModelProduct = new ProductModel(_product);

            //Get the related products.
            List<ItemProductModel> _viewModelProductList = new List<ItemProductModel>();

            //Get the list of products.
            var _productList =  _dbContext.GetRelatedProducts(_viewModelProduct.IdMarca, _viewModelProduct.IdCategoria, _viewModelProduct.IdSubCategoria);

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


        [ActionName("search")]
        [HttpGet]
        public ActionResult SearchFilterProducts(int? page, string keywords)
        {
            //Handling the 0 value in the page number.
            if (page < 1)
                throw new HttpException(404, "Page size is 0");

            //Check is the query is empty
            if (String.IsNullOrWhiteSpace(keywords) || keywords == "'")
            {
                var _viewModelProduct = new List<ItemProductModel>();

                ViewBag.TitleKeyWords = keywords;
                return View("SearchFilterProducts", _viewModelProduct.ToList().ToPagedList(page ?? 1, _pageSize));

            }
            //--------------------------------------------------------

            string keywordEscape = keywords.Replace("'", "\"");

            var _productSearch = _dbContext.ProductSearch(keywordEscape);

            List<ItemProductModel> _viewModelProducts = new List<ItemProductModel>();

            foreach (var _product in _productSearch)
            {
                _viewModelProducts.Add(new ItemProductModel(_product));
            }

            ViewBag.TitleKeyWords = keywords;
            ViewBag.keywords = keywords;

            return View("SearchFilterProducts", _viewModelProducts.ToList().ToPagedList(page ?? 1, _pageSize));
        }
        //----------------------------



    }
}