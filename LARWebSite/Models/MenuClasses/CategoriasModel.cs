using System;
using LARWebSite.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LARWebSite.Models.MenuClasses
{
    public class CategoriasModel
    {
        public long IdCategoria { get; set; }
        public string NombreCategoria { get; set; }
        public string ImagenCategoria { get; set; }

        public CategoriasModel()
        {
            //Empty constructor.
        }
        //----------------------

        public CategoriasModel(categories _category)
        {
            this.IdCategoria = _category.idCategory;
            this.NombreCategoria = _category.categoryName;
            this.ImagenCategoria = _category.Image;
        }
        //----------------------

    }
}