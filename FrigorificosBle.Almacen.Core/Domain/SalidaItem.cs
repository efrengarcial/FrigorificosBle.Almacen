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
    
    public partial class SalidaItem
    {
        public long Id { get; set; }
        public int IdProducto { get; set; }
        public int IdCentroCostos { get; set; }
        public int Cantidad { get; set; }
        public string EquipoObra { get; set; }
        public Nullable<long> IdOrden { get; set; }
        public long IdSalida { get; set; }
        public decimal Precio { get; set; }
    
        public virtual CentroCosto CentroCosto { get; set; }
        public virtual Orden Orden { get; set; }
        public virtual Producto Producto { get; set; }
        public virtual Salida Salida { get; set; }
    }
}
