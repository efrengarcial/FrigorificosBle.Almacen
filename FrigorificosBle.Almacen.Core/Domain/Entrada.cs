//------------------------------------------------------------------------------
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
    using System.Collections.Generic;
    
    public partial class Entrada
    {
        public int Id { get; set; }
        public long IdOrdenItems { get; set; }
        public int IdProducto { get; set; }
        public System.DateTime Fecha { get; set; }
        public int Cantidad { get; set; }
        public Nullable<decimal> Costo { get; set; }
    
        public virtual OrdenItem OrdenItem { get; set; }
    }
}