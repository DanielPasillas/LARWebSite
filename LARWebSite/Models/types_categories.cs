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
    
    public partial class types_categories
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public types_categories()
        {
            this.categories = new HashSet<categories>();
        }
    
        public long idMasterCategory { get; set; }
        public string parentCategory { get; set; }
        public long idMenuMaster { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<categories> categories { get; set; }
        public virtual master_menu_type master_menu_type { get; set; }
    }
}
