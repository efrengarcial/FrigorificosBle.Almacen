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
    
    public partial class Producto
    {
        public int Id { get; set; }
        public int Codigo { get; set; }
        public string Referencia { get; set; }
        public string Nombre { get; set; }
        public string Ubicacion { get; set; }
        public int IdSubLinea { get; set; }
        public int Maximo { get; set; }
        public int Minimo { get; set; }
        public int IdMedida { get; set; }
        public double Iva { get; set; }
        public decimal Precio { get; set; }
        public bool Promocion { get; set; }
        public bool Activo { get; set; }
        public string Descripcion { get; set; }
    
        public virtual Medida Medida { get; set; }
        public virtual SubLinea SubLinea { get; set; }
    }
}
