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
    using System.Data.Entity.Infrastructure;
    
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
        public DbSet<Entrada> Entradas { get; set; }
        public DbSet<CentroCosto> CentroCostos { get; set; }
        public DbSet<SalidaItem> SalidaItems { get; set; }
        public DbSet<Salida> Salidas { get; set; }
        public DbSet<InventarioFinal> InventarioFinals { get; set; }
        public DbSet<HistoricoProducto> HistoricoProductos { get; set; }
    }
}
