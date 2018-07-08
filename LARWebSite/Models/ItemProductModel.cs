using System;
using LARWebSite.Utilerias;
using LARWebSite.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LARWebSite.Models
{
    //This model includes only a couple of properties for the product information.
    public class ItemProductModel
    {
        public long IdProducto { get; set; }

        public string NombreProducto { get; set; }

        public decimal Precio { get; set; }

        public decimal Descuento { get; set; }

        public decimal PrecioDescuento { get; set; }

        public string ImagenDetalle { get; set; }

        public string Codigo { get; set; }

        public ItemProductModel()
        {
            //Empty constructor
        }

        public ItemProductModel(products _product)
        {
            this.IdProducto =  _product.idProduct;
            this.NombreProducto = _product.nameProduct;
            this.Precio = _product.salePrice;
            this.Descuento = _product.discount;
            this.PrecioDescuento = _product.discount != 0 ? (_product.salePrice - _product.discount) : 0;
            this.ImagenDetalle = _product.Image_link.ToString();
            this.Codigo = _product.keyProduct;
        }
    }
}