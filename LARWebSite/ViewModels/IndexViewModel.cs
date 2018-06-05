using System;
using LARWebSite.Models;
using LARWebSite.Models.MenuClasses;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LARWebSite.ViewModels
{

    /*
     *  In this view model we will save the set of models that will let index module to work..
     *  i.e. The Slider Model, the category model, the brands model and so on.
     *  Everything will be fill up automatically.
     */
    public class IndexViewModel
    {
        //Property for Slider/Carousel.
        public IEnumerable<SliderModel> Carousel { get; set; }

        //Enumerable property to list the categories.
        public IEnumerable<CategoriasModel> Categorias { get; set; }

        //Property to list the newProducts.
        public IEnumerable<ItemProductModel> NuevosProductos { get; set; }

        public IEnumerable<ItemProductModel> ProductosGallery { get; set; }



    }
}