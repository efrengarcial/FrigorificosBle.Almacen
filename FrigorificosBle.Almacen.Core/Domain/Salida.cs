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
    
    public partial class Salida
    {
        public Salida()
        {
            this.SalidaItems = new HashSet<SalidaItem>();
        }
    
        public long Id { get; set; }
        public System.DateTime FechaEntrega { get; set; }
        public int IdSolicitador { get; set; }
        public int IdRecibidor { get; set; }
    
        public virtual ICollection<SalidaItem> SalidaItems { get; set; }
    }
}