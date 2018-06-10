using System;
using LARWebSite.Models.MenuClasses;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LARWebSite.Models
{
    public class MarcasModel
    {
        public long IdMarca {get; set; }
        public string NombreMarca { get; set; }

        public MarcasModel()
        {
            //Empty constructor
        }

        public MarcasModel(brands _brand)
        {
            this.IdMarca = _brand.idBrand;
            this.NombreMarca = _brand.Brand;
        }
    }
}