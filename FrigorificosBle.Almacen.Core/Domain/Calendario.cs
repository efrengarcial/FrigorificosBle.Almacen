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
    
    public partial class Calendario
    {
        public int Id { get; set; }
        public System.DateTime Fecha { get; set; }
        public int Ano { get; set; }
        public int Mes { get; set; }
        public int Dia { get; set; }
        public string MesNombre { get; set; }
        public string DiaNombre { get; set; }
        public int DiaAno { get; set; }
        public int SemanaAno { get; set; }
        public int Semestre { get; set; }
        public int Trimestre { get; set; }
        public int Cuatrimestre { get; set; }
        public bool DiaHabil { get; set; }
    }
}
