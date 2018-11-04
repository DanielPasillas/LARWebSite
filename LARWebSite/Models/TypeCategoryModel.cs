using System;
using LARWebSite.Models.MenuClasses;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LARWebSite.Models
{
    public class TypeCategoryModel
    {
        //Id Type Category
        public long idTypeCategory { get; set; }

        //name Type Category
        public string typeCategoryName { get; set; }

        //Category List
        public IEnumerable<CategoriasModel> CategoryList { get; set; }
    }
}