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

        public static async Task<IEnumerable<products>> GetProductsBySubCategoryId(this dbContextLAR context, int idSubCategory)
        {
            return await context.products
                        .Include("brands")
                        .Include("categories")
                        .Include("subcategories")
                        .Include("images")
                        .Include("sizes_product")
                        .Include("product_label")
                        .Where(m => m.idSubCategory == idSubCategory).ToListAsync();
        }
        //-------------------------------------------------------------------------

        public static IEnumerable<products> GetRelatedProducts(this dbContextLAR context, long idBrand, long idCategory, long idSubcategory)
        {

            string _sqlRelatedProducts = "SELECT idProduct, nameProduct, description, extendDescription, Image_link, idBrand, idCategory, idSubCategory, keyProduct, stock, discount, salePrice, wholesalePrice, limitWholeSalePrice, fecha_alta, status FROM products WHERE products.status = 1 AND (products.idBrand = " + idBrand + " OR products.idCategory = " + idCategory + " OR products.idSubCategory = " + idSubcategory + ") ORDER BY rand() LIMIT 4";

            return context.products.SqlQuery(_sqlRelatedProducts).ToList<products>();                
        }
        //-------------------------------------------------------------------------

        
        /*
         *  WE USE THIS METHOD FOR SEARCHING PRODUCTS
         */
        public static IEnumerable<products> ProductSearch(this dbContextLAR context, string query)
        {
            //search order
            //1. Look for the related tables.
            //2. Brands. 
            //3. Categories => Señuelos, Cañas, Carretes, etc.
            //4. Sub Categories => Cucharillas, Jigs, Plásticos, etc...
            //5. Labels => plasticos, curricanes, agua dulce, etc...
            //6. At the end

            string _searchQuery = "(SELECT products.idProduct, products.nameProduct, products.description, products.extendDescription, products.Image_link, products.idBrand, products.idCategory, products.idSubCategory, products.keyProduct, products.stock, products.discount, products.salePrice, products.wholesalePrice, products.limitWholeSalePrice, products.fecha_alta, products.status FROM products" +
                " INNER JOIN(SELECT brands.idBrand FROM brands WHERE MATCH(brands.Brand) AGAINST('"+ query + "*' IN BOOLEAN MODE)) AS b ON b.idBrand = products.idBrand LIMIT 9 )" +
                " UNION" +
                " (SELECT products.idProduct, products.nameProduct, products.description, products.extendDescription, products.Image_link, products.idBrand, products.idCategory, products.idSubCategory, products.keyProduct, products.stock, products.discount, products.salePrice, products.wholesalePrice, products.limitWholeSalePrice, products.fecha_alta, products.status FROM products" +
                " INNER JOIN(SELECT categories.idCategory FROM categories WHERE MATCH(categories.categoryName) AGAINST('"+ query + "*' IN BOOLEAN MODE)) AS c ON c.idCategory = products.idCategory LIMIT 9 )" +
                " UNION" +
                " (SELECT products.idProduct, products.nameProduct, products.description, products.extendDescription, products.Image_link, products.idBrand, products.idCategory, products.idSubCategory, products.keyProduct, products.stock, products.discount, products.salePrice, products.wholesalePrice, products.limitWholeSalePrice, products.fecha_alta, products.status FROM products" +
                " INNER JOIN(SELECT subcategories.idSubCategory FROM subcategories WHERE MATCH(subcategories.subCategoryName) AGAINST('"+ query + "*' IN BOOLEAN MODE)) AS s ON s.idSubCategory = products.idSubCategory LIMIT 9)" +
                " UNION" +
                " (SELECT products.idProduct, products.nameProduct, products.description, products.extendDescription, products.Image_link, products.idBrand, products.idCategory, products.idSubCategory, products.keyProduct, products.stock, products.discount, products.salePrice, products.wholesalePrice, products.limitWholeSalePrice, products.fecha_alta, products.status FROM products" +
                " WHERE MATCH(products.nameProduct, products.description, products.extendDescription) AGAINST('"+ query + "*' IN BOOLEAN MODE) LIMIT 9 )";

            return context.products.SqlQuery(_searchQuery.Trim()).ToList<products>();
        }
        //-------------------------------------------------------------------------
    }
}