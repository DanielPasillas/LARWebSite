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
        public static IEnumerable<products> ProductsByIdBrand(this dbContextLAR context, int idBrand, int totalRecords)
        {
            //Get the products filtered by the brand id.
           return context.products
                .Include("brands")
                .Include("categories")
                .Include("subcategories")
                .Include("images")
                .Include("sizes_product")
                .Include("product_label")
                .Where(m => m.idBrand == idBrand)
                .Take(totalRecords)
                .ToList();
        }
        //-------------------------------------------------------------------------

        public static IEnumerable<products> GetRelatedProducts(this dbContextLAR context, long idBrand, long idCategory, long idSubcategory)
        {

            string _sqlRelatedProducts = "SELECT idProduct, nameProduct, description, extendDescription, Image_link, idBrand, idCategory, idSubCategory, keyProduct, stock, discount, salePrice, wholesalePrice, limitWholeSalePrice, fecha_alta, status FROM products WHERE products.status = 1 AND (products.idBrand = " + idBrand + " OR products.idCategory = " + idCategory + " OR products.idSubCategory = " + idSubcategory + ") ORDER BY rand() LIMIT 4";

            return context.products.SqlQuery(_sqlRelatedProducts).ToList<products>();                
        }
        //-------------------------------------------------------------------------
    }
}