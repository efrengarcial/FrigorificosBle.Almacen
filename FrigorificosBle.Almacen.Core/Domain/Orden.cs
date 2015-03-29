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
    
    public partial class Orden
    {
        public Orden()
        {
            this.OrdenItems = new HashSet<OrdenItem>();
        }
    
        public long Id { get; set; }
        public long Numero { get; set; }
        public string Tipo { get; set; }
        public int IdProveedor { get; set; }
        public Nullable<int> IdOperario { get; set; }
        public Nullable<int> IdUsuario { get; set; }
        public string CentroCostos { get; set; }
        public System.DateTime FechaCreacion { get; set; }
        public double Descuento { get; set; }
        public bool Anulada { get; set; }
        public bool Cancelada { get; set; }
        public Nullable<int> IdCaja { get; set; }
        public string Notas { get; set; }
        public string Estado { get; set; }
    
        public virtual Proveedor Proveedor { get; set; }
        public virtual ICollection<OrdenItem> OrdenItems { get; set; }
    }
}
