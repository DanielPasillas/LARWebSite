﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class dbContextLAR : DbContext
    {
        public dbContextLAR()
            : base("name=dbContextLAR")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<brands> brands { get; set; }
        public virtual DbSet<categories> categories { get; set; }
        public virtual DbSet<images> images { get; set; }
        public virtual DbSet<labels> labels { get; set; }
        public virtual DbSet<product_label> product_label { get; set; }
        public virtual DbSet<products> products { get; set; }
        public virtual DbSet<sizes_product> sizes_product { get; set; }
        public virtual DbSet<slider> slider { get; set; }
        public virtual DbSet<subcategories> subcategories { get; set; }
        public virtual DbSet<types_categories> types_categories { get; set; }
        public virtual DbSet<master_menu_type> master_menu_type { get; set; }
    }
}