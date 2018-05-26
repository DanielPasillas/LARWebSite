using System;
using LARWebSite.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LARWebSite.Models
{
    public class SliderModel
    {
        public int itHasLink { get; set; }
        public string Descripcion { get; set; }
        public string Imagen { get; set; }
        public string Title { get; set; }
        public string UrlResource { get; set; }

        public SliderModel(slider _slider)
        {
            this.itHasLink = _slider.hasLink;
            this.Descripcion = _slider.description;
            this.Title = _slider.title;
            this.Imagen = _slider.Image;
            this.UrlResource = _slider.url_target;
        }
    }
}