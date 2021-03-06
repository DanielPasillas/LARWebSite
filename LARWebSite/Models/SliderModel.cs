﻿using System;
using LARWebSite.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LARWebSite.Models
{
    public class SliderModel
    {
        public string Imagen { get; set; }
        public string UrlResource { get; set; }
        public string ImgSmall { get; set; }

        public SliderModel(slider _slider)
        {
            this.Imagen = _slider.Image;
            this.UrlResource = _slider.url_target;
            this.ImgSmall = _slider.ImgSmall;
        }
    }
}