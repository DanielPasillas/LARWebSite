using System;
using System.Data.Entity;
using System.Collections.Generic;
using LARWebSite.Models;
using LARWebSite.Utilerias;
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

        /*
         * We will retrieve the list of products by using the 
         * Category Id as parameter.
         */
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

        /*
         *  Search products by using the brand Id.
         */
        [ActionName("brand")]
        public async Task<ActionResult> BrandProductSearch(int id, string name)
        {
            var _brand = await _dbContext.brands.FirstOrDefaultAsync(m => m.idBrand == id);

            if (_brand == null)
                throw new HttpException(404, "Brand Not found");

            string _expectedBrandName = _brand.Brand.ToSeoUrl();

            string actualBrandName = (name ?? "").ToLower();

            if(_expectedBrandName != actualBrandName)
            {
                //If the brand name is not the same, we will redirect to the proper location by passing the name in a SEO format.
                return RedirectToActionPermanent("brand", "search", new { id = _brand.idBrand, name = _expectedBrandName });
            }

            //Otherwise, set the name of the brand in a ViewBag variable.
            ViewBag.BrandTitle = _brand.Brand;
            ViewBag.BrandTitleAjax = _expectedBrandName;
            ViewBag.idCode = _brand.idBrand;
            ViewBag.recordsTake = 9;

            //Get the products by IdBrand.
            var productsByBrand =  _dbContext.ProductsByIdBrand(id, 9);

            //View model for saving the products.
            List<ItemProductModel> _viewModelProducts = new List<ItemProductModel>();

            foreach(var _product in productsByBrand)
            {
                _viewModelProducts.Add(new ItemProductModel(_product));
            }

            return View("BrandProductSearch", _viewModelProducts);

        }
        //----------------------------

        [ActionName("brandrecords")]
        [HttpPost]
        public ActionResult BrandProductSearch(int id, string name, int records)
        {
            if(Request.IsAjaxRequest())
            {
                int _take = records + 9;

                //Get the products by IdBrand.
                var productsByBrand = _dbContext.ProductsByIdBrand(id, _take);

                //Otherwise, set the name of the brand in a ViewBag variable
                ViewBag.BrandTitleAjax = name;
                ViewBag.idCode = id;
                ViewBag.recordsTake = _take;

                //View model for saving the products.
                List<ItemProductModel> _viewModelProducts = new List<ItemProductModel>();

                foreach (var _product in productsByBrand)
                {
                    _viewModelProducts.Add(new ItemProductModel(_product));
                }


                return PartialView("_brandProductsSearch/_serverRenderBrandsView", _viewModelProducts);
            }
            else
            {
                throw new HttpException(404, "Not Ajax Request");
            }
        }
        //----------------------------

        
        /*
         * Method for showing the products by subcategory Id.
         */
        [ActionName("filtersubcategory")]
        public async Task<ActionResult> GetProductsBySubCategory(int id, string nameSubCategory)
        {

            //--------------------------------------//
            // VALIDATE SEO SECTIO  FOR URL //.

            var _subCategory = await _dbContext.subcategories.FirstOrDefaultAsync(m => m.idSubCategory == id);

            if (_subCategory == null)
                throw new HttpException(404, "SubCategory not Found");

            string _expectedSubCategoryname = _subCategory.subCategoryName.ToLower();

            string actualCategoryName = (nameSubCategory ?? "").ToLower();

            if (_expectedSubCategoryname != actualCategoryName)
            {
                //If the brand name is not the same, we will redirect to the proper location by passing the name in a SEO format.
                return RedirectToActionPermanent("filtersubcategory", "search", new { id = _subCategory.idSubCategory, nameSubCategory = _expectedSubCategoryname });
            }

            //Get the list of products by using the SubCategory Id as a parameter.
            var _productsByCategory = await _dbContext.GetProductsBySubCategoryId(id);
            
            //We throw an exception if the product was not found
            if (_productsByCategory == null)
                throw new HttpException(404, "Product not found");

            //Display names
            ViewBag.SubCategoryTitle = _subCategory.subCategoryName;

            List<ItemProductModel> _viewModelProducts = new List<ItemProductModel>();

            foreach(var product in _productsByCategory)
            {
                _viewModelProducts.Add(new ItemProductModel(product));
            }
            //------------------------------------------


            return View("GetProductsBySubCategory", _viewModelProducts);
        }
        //----------------------------

        /*
         * Typehaead functionality 
         */
        [HttpGet]
        [ActionName("autocomplete")]
        public async Task<JsonResult> AutocompleteSearchFilter(string query)
        {
            
            var result = await (from e in _dbContext.products
                          where e.nameProduct.StartsWith(query)
                          select e.nameProduct).ToListAsync();//_list.Where(m => m.NombreProducto.Contains(query)).ToList();

            

            return Json(result, JsonRequestBehavior.AllowGet);
        }
        //----------------------------

    }
}