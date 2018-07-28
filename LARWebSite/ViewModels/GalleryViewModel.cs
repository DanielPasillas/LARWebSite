using System;
using PagedList;
using PagedList.Mvc;
using LARWebSite.Models;
using LARWebSite.Models.MenuClasses;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LARWebSite.ViewModels
{
    public class GalleryViewModel
    {
        public IEnumerable<CategoriasModel> CategoryList { get; set; }

        public IPagedList<ItemProductModel> ArticleList { get; set; }
    }
}