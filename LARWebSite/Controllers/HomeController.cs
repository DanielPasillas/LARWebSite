using System;
using System.Data.Entity;
using LARWebSite.Models;
using LARWebSite.Models.MenuClasses;
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
            //Get Slider List.
            var _slides = await _dbContext.slider.ToListAsync();

            List<SliderModel> _carousel = new List<SliderModel>();

            foreach(var carousel in _slides)
            {
                _carousel.Add(new SliderModel(carousel));
            }
            //------------------//

            //Get Categories Collage.
            //At this point we only will take only 3 random records.
            var _categories = await _dbContext.categories.Take(3).ToListAsync();

            List<CategoriasModel> _viewModelCategories = new List<CategoriasModel>();

            foreach(var category in _categories)
            {
                _viewModelCategories.Add(new CategoriasModel(category));
            }
            //------------------//

            //Get The new Products
            //We will take the newer products. Only 8 records.
            var _newProducts = await _dbContext.products.OrderByDescending(m => m.fecha_alta).Take(17).ToListAsync();

            List<ItemProductModel> _viewModelProduct = new List<ItemProductModel>();
            
            foreach(var _itemProduct in _newProducts)
            {
                _viewModelProduct.Add(new ItemProductModel(_itemProduct));
            }

            IndexViewModel _viewModel = new IndexViewModel()
            {
                Carousel = _carousel,
                Categorias = _viewModelCategories,
                NuevosProductos = _viewModelProduct.Take(8).ToList(),
                ProductosGallery = _viewModelProduct.Skip(8).Take(9).ToList()
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
        //-------------------------

        [ActionName("brands")]
        [OutputCache(Duration = 50, VaryByParam = "none")]
        public async Task<ActionResult> Brands()
        {
            //Get the brands list.
            var brands = await _dbContext.brands.Take(5).ToListAsync();

            List<MarcasModel> _vieModelMarcas = new List<MarcasModel>();

            foreach(var _brand in brands)
            {
                _vieModelMarcas.Add(new MarcasModel(_brand));
            }

            return PartialView("Brands", _vieModelMarcas);
        }
        //----------------------------------------------

        [ActionName("categories")]
        public async Task<ActionResult> GetCategories()
        {
            var _categories = await _dbContext.categories.Take(5).ToListAsync();

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