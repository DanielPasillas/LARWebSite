﻿using System;
using LARWebSite.Utilerias;
using System.Data.Entity;
using LARWebSite.Models;
using LARWebSite.Models.MenuClasses;
using LARWebSite.ViewModels;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Threading.Tasks;
using PagedList;

namespace LARWebSite.Controllers
{
    public class HomeController : Controller
    {
        //Db Context for connection.
        private readonly dbContextLAR _dbContext;

        //Page size for pagination.
        private const int _pageSize = 9;

        //Obj for Utilerias.
        private Utilerias.Utilerias _utilerias;

        public HomeController()
        {
            _dbContext = new dbContextLAR();

            _utilerias = new Utilerias.Utilerias();
        }
        //----------------------------------------------

        /* 
         *   Home Page.
         */
        public async Task<ActionResult> Index()
        {
            //Get Slider List.
            var _slides = await _dbContext.slider.ToListAsync();

            List<SliderModel> _carousel = new List<SliderModel>();

            foreach(var carousel in _slides)
            {
                _carousel.Add(new SliderModel(carousel));
            }
            //------------------//

            //Get Categories Collage.
            string _queryRandomCategories = "SELECT idCategory, parentCategory, categoryName, Image FROM categories LIMIT 3";
            var _categories = _dbContext.categories.SqlQuery(_queryRandomCategories).ToList<categories>();

            List<CategoriasModel> _viewModelCategories = new List<CategoriasModel>();

            foreach(var category in _categories)
            {
                _viewModelCategories.Add(new CategoriasModel(category));
            }
            //------------------//

            //Get The new Products
            //We will take the newer products. Only 8 records.
            string _queryNewProducts = "SELECT idProduct, nameProduct, description, extendDescription, Image_link, idBrand, idCategory, idSubCategory, keyProduct," +
                                           " stock, discount, salePrice, wholesalePrice, limitWholeSalePrice, fecha_alta, status FROM products WHERE status = 1 ORDER BY fecha_alta DESC LIMIT 8";
            var _newProducts =  _dbContext.products.SqlQuery(_queryNewProducts).ToList<products>();

            List<ItemProductModel> _viewModelProduct = new List<ItemProductModel>();
            
            foreach(var _itemProduct in _newProducts)
            {
                _viewModelProduct.Add(new ItemProductModel(_itemProduct));
            }

            //Get 9 random products.
            string _collageProductString = "SELECT idProduct, nameProduct, description, extendDescription, Image_link, idBrand, idCategory, idSubCategory, keyProduct," +
                                           " stock, discount, salePrice, wholesalePrice, limitWholeSalePrice, fecha_alta, status FROM products WHERE status = 1 LIMIT 9";
            var _collageProductsGallery = _dbContext.products.SqlQuery(_collageProductString).ToList<products>();

            List<ItemProductModel> _collageViewModel = new List<ItemProductModel>();

            foreach(var _productCollage in _collageProductsGallery)
            {
                _collageViewModel.Add(new ItemProductModel(_productCollage));
            }

            IndexViewModel _viewModel = new IndexViewModel()
            {
                Carousel = _carousel,
                Categorias = _viewModelCategories,
                NuevosProductos = _viewModelProduct,
                ProductosGallery = _collageViewModel
            };


            return View(_viewModel);
        }
        //----------------------------------------------

        /* 
         *   Contact Page.
         */
        [ActionName("contacto")]
        public ActionResult Contact()
        {
            return View("Contacto");
        }
        //----------------------------------------------

        /* 
         *   We use this method to send the contact information
         */
        [ActionName("contact")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Contact(ContactModel _viewModelContact)
        {
            //If model is invalid
            if (!ModelState.IsValid)
                return View("contact", _viewModelContact);

            //Otherwise
            try
            {
                bool isSent =  _utilerias.SendEmailAsync(_viewModelContact.EmailContacto, _viewModelContact.NombreContacto, _viewModelContact.ComentarioContacto);

                if (!isSent)
                    return Content("An error occurred while sending the Email");

                //Otherwise
                return View("_SentContactSuccessful");
            }
            catch (Exception e)
            {

                throw e;
            }
        }
        //----------------------------------------------

        /* 
         *   About Page.
         */
        [ActionName("about")]
        public ActionResult About()
        {
            return View();
        }
        //----------------------------------------------

        /* 
         *   Offers Page.
         */
        [ActionName("gallery")]
        public async Task<ActionResult> Galeria(int? page, int id, string category)
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

            if (_expectedCategoryName != _currentCategoryName)
            {
                //If the category name is not the same, we will redirect to the proper location by passing the name in a SEO format.
                return RedirectToActionPermanent("gallery", "home", new { page = 1, id = _category.idCategory, category = _expectedCategoryName.ToSeoUrl() });
            }

            //Get products by Category Id.
            var _products = await _dbContext.GetProductsByCategoryId(id);

            List<ItemProductModel> _viewModelProduct = new List<ItemProductModel>();

            foreach (var _product in _products)
            {
                _viewModelProduct.Add(new ItemProductModel(_product));
            }

            //Get the list of categories and Master Categories.
            //Everytime users click on a category record, we will retrieve the products related with that category.
            var _masterTypeCategories = await _dbContext.types_categories.ToArrayAsync();

            //Initialize the category view model.
            List<TypeCategoryModel> _viewModelTypeCategory = new List<TypeCategoryModel>();

            foreach (var itemTypeCategory in _masterTypeCategories)
            {
                List<CategoriasModel> _categoryViewModel = new List<CategoriasModel>();
                //Fetch the list of categories based on the masterTypeCategory record.
                var _categoriesByMasterType = await _dbContext.categories.Where(m => m.parentCategory == itemTypeCategory.idMasterCategory).ToArrayAsync();

                foreach (var itemCategory in _categoriesByMasterType)
                {
                    _categoryViewModel.Add(new CategoriasModel(itemCategory));
                }

                _viewModelTypeCategory.Add(new TypeCategoryModel(){
                    idTypeCategory = itemTypeCategory.idMasterCategory,
                    typeCategoryName = itemTypeCategory.parentCategory,
                    CategoryList = _categoryViewModel
                });
            }

            GalleryViewModel _viewModel = new GalleryViewModel()
            {
                TypeCategory = _viewModelTypeCategory,
                ArticleList = _viewModelProduct.ToList().ToPagedList(page ?? 1, _pageSize)
            };

            ViewBag.CategoryId = _category.idCategory;
            ViewBag.CategoryName = _category.categoryName;
            ViewBag.IsActive = id; //Save the category Id.

            return View("Galeria", _viewModel);
        }
        //----------------------------------------------

        /* 
         *   By using this resource we load the mega menu for categories and subcategories.
         */
        [ActionName("menu")]
        [OutputCache(Duration = 50, VaryByParam = "none")]
        public async Task<ActionResult> LoadMenu()
        {
            //Get the master categories and types.
            var _MasterMenuItems = await _dbContext.master_menu_type.Where(m => m.showRecord == 1).ToListAsync();

            //Create viewModel for MasterCategories.
            List<Menu> _mainMenu = new List<Menu>();

            foreach(var _masterType in _MasterMenuItems)
            {
                //Fetch the type categories for the products.
                var _typeCategory = await _dbContext.types_categories.Where(m => m.idMenuMaster == _masterType.idMenuMaster).ToListAsync();

                //Fill up the category viewModel.
                //-----------------------------
                List<MenuTypeCategories> _listTypeCategories = new List<MenuTypeCategories>();
                foreach (var type in _typeCategory)
                {

                    //Select all the categories that includes the type_categiry Id.
                    var _categories = await _dbContext.categories.Where(m => m.parentCategory == type.idMasterCategory).ToListAsync();

                    List<MenuCategories> _viewModelCategory = new List<MenuCategories>();
                    foreach(var _category in _categories)
                    {

                        var _subCategories = await _dbContext.subcategories.Where(m => m.idCategory == _category.idCategory).ToListAsync();

                        List<SubCategories> _viewModelSubCategories = new List<SubCategories>();
                        foreach(var _subcategory in _subCategories)
                        {
                            _viewModelSubCategories.Add(new SubCategories()
                            {
                                IdSubCategory = _subcategory.idSubCategory,
                                SubCategory = _subcategory.subCategoryName
                            });
                        }

                        _viewModelCategory.Add(new MenuCategories()
                        {
                            IdCategory = _category.idCategory,
                            Category = _category.categoryName.ToString(),
                            SubCategories = _viewModelSubCategories
                        });
                    }


                    _listTypeCategories.Add(new MenuTypeCategories()
                    {
                        IdTypeCategory = type.idMasterCategory,
                        TypeCategory = type.parentCategory.ToString(),
                        Categories = _viewModelCategory
                    });
                }

                _mainMenu.Add(new Menu()
                {
                    IdMainType = _masterType.idMenuMaster,
                    MainType = _masterType.nameMasterMenu.ToString(),
                    _TypeCategorias = _listTypeCategories
                });
            }

            MenuViewModel _viewModel = new MenuViewModel()
            {
                MasterMenu = _mainMenu
            };

            return PartialView("LoadMenu", _viewModel);
        }
        //----------------------------------------------

        /* 
         *    Brands List Section - Master _Layout.
         *    This dynamic menu is shown in the main menu.
         */
        [ActionName("brands")]
        [OutputCache(Duration = 50, VaryByParam = "none")]
        public ActionResult Brands()
        {
            //Get the brands list.
            //We will use a direct MySQL query for getting the random values.
            string _queryBrand = "SELECT idBrand, Brand, Image_b, Image_s FROM brands ORDER BY rand() LIMIT 5";
            var brands = _dbContext.brands.SqlQuery(_queryBrand).ToList<brands>();

            List<MarcasModel> _vieModelMarcas = new List<MarcasModel>();

            foreach(var _brand in brands)
            {
                _vieModelMarcas.Add(new MarcasModel(_brand));
            }

            return PartialView("Brands", _vieModelMarcas);
        }
        //----------------------------------------------

        /* 
         *    Categories List Section - Master _Layout.
         *    This menu is shonw it the main footer.
         */
        [ActionName("categories")]
        [OutputCache(Duration = 50, VaryByParam = "none")]
        public ActionResult GetCategories()
        {   
            //Get the random values from database.
            string _queryCategories = "SELECT idCategory, parentCategory, categoryName, Image FROM categories ORDER BY rand() LIMIT 5  ";

            var _categories = _dbContext.categories.SqlQuery(_queryCategories).ToList<categories>();

            //List categories for the Main footer.
            List<CategoriasModel> _viewListCategories = new List<CategoriasModel>();

            foreach(var _category in _categories)
            {
                _viewListCategories.Add(new CategoriasModel(_category));
            }

            return PartialView("GetCategories", _viewListCategories);
        }
        //----------------------------------------------


    }
}