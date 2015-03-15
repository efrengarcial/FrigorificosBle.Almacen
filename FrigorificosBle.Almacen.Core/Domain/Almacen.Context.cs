﻿//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace FrigorificosBle.Almacen.Core.Domain
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Core.Objects;
    using System.Data.Entity.Infrastructure;
    using System.Linq;
    
    public partial class AlmacenContext : DbContext
    {
        public AlmacenContext()
            : base("name=AlmacenContext")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public DbSet<Producto> Productos { get; set; }
        public DbSet<Linea> Lineas { get; set; }
        public DbSet<Medida> Medidas { get; set; }
        public DbSet<SubLinea> SubLineas { get; set; }
        public DbSet<Proveedor> Proveedors { get; set; }
        public DbSet<Orden> Ordens { get; set; }
        public DbSet<OrdenItem> OrdenItems { get; set; }
    
        public virtual ObjectResult<Nullable<long>> CrearNumeroOrden(string tipoOrden)
        {
            var tipoOrdenParameter = tipoOrden != null ?
                new ObjectParameter("tipoOrden", tipoOrden) :
                new ObjectParameter("tipoOrden", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Nullable<long>>("CrearNumeroOrden", tipoOrdenParameter);
        }
    }
}
