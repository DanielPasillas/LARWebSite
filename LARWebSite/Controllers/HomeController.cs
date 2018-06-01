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

        [ActionName("menu")]
        public async Task<ActionResult> LoadMenu()
        {
            //Get the master categories and types.
            var _MasterMenuItems = await _dbContext.master_menu_type.ToListAsync();

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
    }
}