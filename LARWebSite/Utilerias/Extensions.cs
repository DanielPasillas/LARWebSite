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
                .Include("products.brands")
                .Include("products.categories")
                .Include("products.subcategories")
                .Include("products.images")
                .Include("products.sizes_product")
                .Include("products.product_label")
                .FirstOrDefaultAsync(m => m.idProduct == idProduct && m.keyProduct == code);
        }
        //-------------------------------------------------------------------------
    }
}