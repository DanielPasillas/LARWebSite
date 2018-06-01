using System;
using System.Data.Entity;
using LARWebSite.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading.Tasks;

namespace LARWebSite.Utilerias
{
    public static class Extensions
    {
       
        public static async Task<products> ProductByCodeAndId(this dbContextLAR context, int idProduct, string code)
        {
            //We will search the product by using the code field.
            return await context.products
                .Include("brands")
                .Include("categories")
                .Include("subcategories")
                .Include("images")
                .Include("sizes_product")
                .Include("product_label")
                .FirstOrDefaultAsync(m => m.idProduct == idProduct && m.keyProduct == code);
        }
        //-------------------------------------------------------------------------

        
        //Get the products by looking for the Id brand.
        public static async Task<IEnumerable<ProductModel>> ProductsByIdBrand(this dbContextLAR context, int idBrand)
        {
            //Get the products filtered by the brand id.
            var _products = await context.products
                .Include("brands")
                .Include("categories")
                .Include("subcategories")
                .Include("images")
                .Include("sizes_product")
                .Include("product_label")
                .Where(m => m.idBrand == idBrand).ToListAsync();


            List<ProductModel> _viewModelProduct = new List<ProductModel>();

            foreach(var _product in _products)
            {
                _viewModelProduct.Add(new ProductModel(_product));
            }

            //We will search the product by using the code field.
            return _viewModelProduct;
        }
        //-------------------------------------------------------------------------

        public static async Task<IEnumerable<products>> GetRelatedProducts(this dbContextLAR context, long idBrand, long idCategory, long idSubcategory)
        {
            return await context.products
                .Include("images")
                .Where(m => m.idBrand == idBrand || m.idCategory == idCategory || m.idSubCategory == idSubcategory)
                .Take(4).ToListAsync();
        }
        //-------------------------------------------------------------------------
    }
}