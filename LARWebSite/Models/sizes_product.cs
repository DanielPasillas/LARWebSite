//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace LARWebSite.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class sizes_product
    {
        public long idSize { get; set; }
        public long idProduct { get; set; }
        public string sizeValue { get; set; }
    
        public virtual products products { get; set; }
    }
}