using System;
using PagedList;
using PagedList.Mvc;
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

        //Page size for pagination.
        private const int _pageSize = 9;

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
        public async Task<ActionResult> Category(int? page, int id, string category)
        {
            //Handling the 0 value in the page number.
            if (page < 1)
                throw new HttpException(404, "Page size is 0");

            var _category = await _dbContext.categories.FirstOrDefaultAsync(m => m.idCategory == id);

            //Check if the category is null.
            if (_category == null)
                throw new HttpException(404, "Category does not exist");

            string _expectedCategoryName = _category.categoryName.ToSeoUrl();

            string _currentCategoryName = (category ?? "").ToLower();

            if(_expectedCategoryName != _currentCategoryName)
            {
                //If the category name is not the same, we will redirect to the proper location by passing the name in a SEO format.
                return RedirectToActionPermanent("category", "search", new {page = 1, id = _category.idCategory, name = _expectedCategoryName });
            }

            //Get products by Category Id.
            var _products = await _dbContext.GetProductsByCategoryId(id);

            //Save category data.
            ViewBag.CategoryId = _category.idCategory;
            ViewBag.CategoryName = _category.categoryName;

            List<ItemProductModel> _viewModelProduct = new List<ItemProductModel>();

            foreach (var _product in _products)
            {
                _viewModelProduct.Add(new ItemProductModel(_product));
            }

            return View("Category", _viewModelProduct.ToList().ToPagedList(page ?? 1, _pageSize));
        }
        //----------------------------

        /*
         *  Search products by using the brand Id.
         */
        [ActionName("brand")]
        public async Task<ActionResult> BrandProductSearch(int? page, int id, string name)
        {

            //Handling the 0 value in the page number.
            if (page < 1)
                throw new HttpException(404, "Page size is 0");

            var _brand = await _dbContext.brands.FirstOrDefaultAsync(m => m.idBrand == id);

            if (_brand == null)
                throw new HttpException(404, "Brand Not found");

            string _expectedBrandName = _brand.Brand.ToSeoUrl();

            string actualBrandName = (name ?? "").ToLower();

            if(_expectedBrandName != actualBrandName)
            {
                //If the brand name is not the same, we will redirect to the proper location by passing the name in a SEO format.
                return RedirectToActionPermanent("brand", "search", new { page = 1, id = _brand.idBrand, name = _expectedBrandName });
            }

            //Otherwise, set the name of the brand in a ViewBag variable.
            ViewBag.BrandTitle = _brand.Brand;
            ViewBag.BrandId = _brand.idBrand;


            //Get the products by IdBrand.
            var productsByBrand =  _dbContext.ProductsByIdBrand(id);

            //View model for saving the products.
            List<ItemProductModel> _viewModelProducts = new List<ItemProductModel>();

            foreach(var _product in productsByBrand)
            {
                _viewModelProducts.Add(new ItemProductModel(_product));
            }

            return View("BrandProductSearch", _viewModelProducts.ToList().ToPagedList(page ?? 1, _pageSize));

        }
        //----------------------------
        
        /*
         * Method for showing the products by subcategory Id.
         */
        [ActionName("filtersubcategory")]
        public async Task<ActionResult> GetProductsBySubCategory(int? page, int id, string nameSubCategory)
        {
            //Handling the 0 value in the page number.
            if (page < 1)
                throw new HttpException(404, "Page size is 0");

            //--------------------------------------//
            // VALIDATE SEO SECTIO  FOR URL //.
            var _subCategory = await _dbContext.subcategories.FirstOrDefaultAsync(m => m.idSubCategory == id);

            if (_subCategory == null)
                throw new HttpException(404, "SubCategory not Found");

            string _expectedSubCategoryname = _subCategory.subCategoryName.ToLower();

            string actualCategoryName = (nameSubCategory ?? "").ToLower();

            if (_expectedSubCategoryname != actualCategoryName)
            {
                //If the category name is not the same, we will redirect to the proper location by passing the name in a SEO format.
                return RedirectToActionPermanent("filtersubcategory", "search", new { page = 1,  id = _subCategory.idSubCategory, nameSubCategory = _expectedSubCategoryname });
            }

            //Get the list of products by using the SubCategory Id as a parameter.
            var _productsByCategory = await _dbContext.GetProductsBySubCategoryId(id);
            
            //We throw an exception if the product was not found
            if (_productsByCategory == null)
                throw new HttpException(404, "Product not found");

            //Display names
            ViewBag.SubCategoryTitle = _subCategory.subCategoryName;
            ViewBag.IdSubCategory = _subCategory.idSubCategory;

            List<ItemProductModel> _viewModelProducts = new List<ItemProductModel>();

            foreach(var product in _productsByCategory)
            {
                _viewModelProducts.Add(new ItemProductModel(product));
            }
            //------------------------------------------

            return View("GetProductsBySubCategory", _viewModelProducts.ToList().ToPagedList(page ?? 1, _pageSize));
        }
        //----------------------------


        /*
         *   Search by Label
         */
        [ActionName("labels")]
        public async Task<ActionResult> BusquedaEtiqueta(int? page, int id, string name)
        {
            //Handling the 0 value in the page number.
            if (page < 1)
                throw new HttpException(404, "Page is less than 0");

            //Check for the label records
            var _labelRecords = await _dbContext.labels.FirstOrDefaultAsync(m => m.idLabel == id);

            //If the label records does not exist, we will return an http exception.
            if(_labelRecords == null)
                throw new HttpException(404, "No records found");

            string _expectedLabelName = _labelRecords.labelName.ToLower();

            string currectLabelName = (name ?? "").ToLower();

            if (_expectedLabelName != currectLabelName)
            {
                //If the label name is not the same, we will redirect to the proper location by passing the name in a SEO format.
                return RedirectToActionPermanent("labels", "search", new { page = 1, id = _labelRecords.idLabel, name = _expectedLabelName });
            }

            //-------------------------------------------------//
            //Search products by using the label id. It will make a search through the label and product table.
            var _productsLabel = await _dbContext.GetProductsByLabel(id);

            //And again, if there are not values found, we will throw an exception.
            if (_productsLabel == null)
                throw new HttpException(404, "No records found");

            //Initialize the View Model.
            List<ItemProductModel> _viewModelProducts = new List<ItemProductModel>();

            //-----------------------------------
            ViewBag.LabelId = id;
            ViewBag.TitleLabel = _labelRecords.labelName;

            foreach(var _product in _productsLabel)
            {
                _viewModelProducts.Add(new ItemProductModel(_product));
            }

            return View("BusquedaEtiqueta", _viewModelProducts.ToList().ToPagedList(page ?? 1, _pageSize));
        }   
        //----------------------------

    }
}