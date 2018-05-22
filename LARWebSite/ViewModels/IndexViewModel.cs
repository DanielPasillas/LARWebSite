using System;
using LARWebSite.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LARWebSite.ViewModels
{

    /*
     *  In this view model we will save the set of models that will let index module to work..
     *  i.e. The Slider Model, the category model, the brands model and so on.
     *  Everything will be fill up automatically.
     */
    public class IndexViewModel
    {
        public IEnumerable<SliderModel> Carousel { get; set; }
    }
}