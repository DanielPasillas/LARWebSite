using System;
using LARWebSite.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LARWebSite.ViewModels
{
    public class MenuViewModel
    {
        public IEnumerable<Menu> MasterMenu { get; set; }
    }
}