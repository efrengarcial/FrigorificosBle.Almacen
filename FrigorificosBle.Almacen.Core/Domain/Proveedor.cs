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
    
    public partial class Proveedor
    {
        public int Id { get; set; }
        public long Nit { get; set; }
        public string Nombre { get; set; }
        public string Contacto { get; set; }
        public string Telefono { get; set; }
        public string Fax { get; set; }
        public string EMail { get; set; }
        public string Direccion { get; set; }
        public string Ciudad { get; set; }
        public decimal CupoCredito { get; set; }
        public string Regimen { get; set; }
        public string FormaPago { get; set; }
        public int Plazo { get; set; }
        public bool Anulado { get; set; }
        public Nullable<int> IdVendedor { get; set; }
        public string Notas { get; set; }
        public Nullable<int> ListaPrecio { get; set; }
        public Nullable<bool> BloqueaFactura { get; set; }
        public Nullable<int> BloqueaCupo { get; set; }
    }
}
