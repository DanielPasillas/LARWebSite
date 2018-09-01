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
       
        /*
         *   Get products by Code and Id.
         */
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
                .FirstOrDefaultAsync(m => m.idProduct == idProduct && m.keyProduct == code && m.status == 1);
        }
        //-------------------------------------------------------------------------

        
        /*
         *   Get Products by Brand Id
         */
        public static IEnumerable<products> ProductsByIdBrand(this dbContextLAR context, int idBrand)
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
                .ToList();
        }
        //-------------------------------------------------------------------------

        /*
         *    Get products by SubCategory Id
         */
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

        /*
         *  Get products by category Id
         */
        public static async Task<IEnumerable<products>> GetProductsByCategoryId(this dbContextLAR context, int idCategory)
        {
            return await context.products
                        .Include("brands")
                        .Include("categories")
                        .Include("subcategories")
                        .Include("images")
                        .Include("sizes_product")
                        .Include("product_label")
                        .Where(m => m.idCategory == idCategory).ToListAsync();
        }
        //-------------------------------------------------------------------------

        /*
         *  Get products by label
         */
        public static async Task<IEnumerable<products>> GetProductsByLabel(this dbContextLAR context, int idLabel)
        {

            return await (from e in context.product_label
                          join p in context.products on e.idProduct equals p.idProduct
                          join lbl in context.labels on e.idLabel equals lbl.idLabel
                          where e.idLabel == idLabel
                          select p).ToListAsync();
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

            string _cleanQuery = query.Replace("\"", "_").Replace("'", "_").Replace("''","_");

            string _searchQuery = "(SELECT products.idProduct, products.nameProduct, products.description, products.extendDescription, products.Image_link, products.idBrand, products.idCategory, products.idSubCategory, products.keyProduct, products.stock, products.discount, products.salePrice, products.wholesalePrice, products.limitWholeSalePrice, products.fecha_alta, products.status FROM products" +
                " INNER JOIN(SELECT brands.idBrand FROM brands WHERE MATCH(brands.Brand) AGAINST('"+ _cleanQuery + "*' IN BOOLEAN MODE)) AS b ON b.idBrand = products.idBrand)" +
                " UNION" +
                " (SELECT products.idProduct, products.nameProduct, products.description, products.extendDescription, products.Image_link, products.idBrand, products.idCategory, products.idSubCategory, products.keyProduct, products.stock, products.discount, products.salePrice, products.wholesalePrice, products.limitWholeSalePrice, products.fecha_alta, products.status FROM products" +
                " INNER JOIN(SELECT categories.idCategory FROM categories WHERE MATCH(categories.categoryName) AGAINST('"+ _cleanQuery + "*' IN BOOLEAN MODE)) AS c ON c.idCategory = products.idCategory)" +
                " UNION" +
                " (SELECT products.idProduct, products.nameProduct, products.description, products.extendDescription, products.Image_link, products.idBrand, products.idCategory, products.idSubCategory, products.keyProduct, products.stock, products.discount, products.salePrice, products.wholesalePrice, products.limitWholeSalePrice, products.fecha_alta, products.status FROM products" +
                " INNER JOIN(SELECT subcategories.idSubCategory FROM subcategories WHERE MATCH(subcategories.subCategoryName) AGAINST('"+ _cleanQuery + "*' IN BOOLEAN MODE)) AS s ON s.idSubCategory = products.idSubCategory)" +
                " UNION" +
                " (SELECT products.idProduct, products.nameProduct, products.description, products.extendDescription, products.Image_link, products.idBrand, products.idCategory, products.idSubCategory, products.keyProduct, products.stock, products.discount, products.salePrice, products.wholesalePrice, products.limitWholeSalePrice, products.fecha_alta, products.status FROM products" +
                " WHERE MATCH(products.nameProduct, products.description, products.extendDescription) AGAINST('" + _cleanQuery + "*' IN BOOLEAN MODE))" +
                " UNION "+
                " (SELECT products.idProduct, products.nameProduct, products.description, products.extendDescription, products.Image_link, products.idBrand, products.idCategory, products.idSubCategory, products.keyProduct, products.stock, products.discount, products.salePrice, products.wholesalePrice, products.limitWholeSalePrice, products.fecha_alta, products.status FROM products" +
                " WHERE MATCH(products.keyProduct) AGAINST('" + query + "' IN BOOLEAN MODE)) " +
                " UNION" +
                " (SELECT products.idProduct, products.nameProduct, products.description, products.extendDescription, products.Image_link, products.idBrand, products.idCategory, products.idSubCategory, products.keyProduct, products.stock, products.discount, products.salePrice, products.wholesalePrice, products.limitWholeSalePrice, products.fecha_alta, products.status FROM products, product_label, labels " +
                " WHERE products.idProduct = product_label.idProduct AND labels.idLabel = product_label.idLabel AND MATCH(labels.labelName) AGAINST ('" + _cleanQuery + "*' IN BOOLEAN MODE) ) ";

            return context.products.SqlQuery(_searchQuery.Trim()).ToList<products>();
        }
        //-------------------------------------------------------------------------
    }
}