using System;
using LARWebSite.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LARWebSite.Models
{
    public class ProductModel
    {
        public long IdProducto { get; set; }
        public string NombreProducto { get; set; }
        public string Descripcion { get; set; }
        public string ExtDescripcion { get; set; }
        public long IdMarca { get; set; }
        public string Marca { get; set; }
        public long IdCategoria { get; set; }
        public string Categoria { get; set; }
        public long IdSubCategoria { get; set; }
        public string SubCategoria { get; set; }
        public string CodigoProducto { get; set; }
        public long Existencias { get; set; }
        public decimal Descuento { get; set; }
        public decimal PrecioVenta { get; set; }
        public decimal PrecioMayoreo { get; set; }
        public System.DateTime FechaAlta { get; set; }
        public int Estatus { get; set; }

        public ProductModel()
        {
                //Empty constructor.
        }

        public ProductModel(products _products)
        {
            this.IdProducto = _products.idProduct;
            this.NombreProducto = _products.nameProduct;
            this.Descripcion = _products.description;
            this.ExtDescripcion = _products.extendDescription;
            this.IdMarca = _products.idBrand;
            this.Marca = _products.brands.Brand;
            this.IdCategoria = _products.idCategory;
            this.Categoria = _products.categories.categoryName;
            this.IdSubCategoria = _products.idSubCategory;
            this.SubCategoria = _products.subcategories.subCategoryName;
            this.CodigoProducto = _products.keyProduct;
            this.Existencias = _products.stock;
            this.Descuento = _products.discount;
            this.PrecioVenta = _products.salePrice;
            this.PrecioMayoreo = _products.wholesalePrice;
            this.FechaAlta = _products.fecha_alta;
            this.Estatus = _products.status;
        }
    }
}