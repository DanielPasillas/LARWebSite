using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LARWebSite.Models
{
    public class Menu
    {
        //This value will save the id from the Master Type Category.
        public long IdMainType { get; set; }

        //String value for saving the name of the Master Type Category.
        public string MainType { get; set; }

        //Values for Types categories.
        public IEnumerable<MenuTypeCategories> _TypeCategorias { get; set; }
    }
    //-----------------------------

    public class MenuTypeCategories
    {
        public long IdTypeCategory { get; set; }

        public string TypeCategory { get; set; }

        public IEnumerable<MenuCategories> Categories { get; set; }
    }
    //-----------------------------

    public class MenuCategories
    {
        public long IdCategory { get; set; }

        public string Category { get; set; }

        public IEnumerable<SubCategories> SubCategories { get; set; }
    }
    //-----------------------------

    public class SubCategories
    {
        public long IdSubCategory { get; set; }

        public string SubCategory { get; set; }
    }
    //-----------------------------


}