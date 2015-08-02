using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FrigorificosBle.Almacen.Core.Domain
{
    public class EntradaOrden
    {
        public long IdOrden { get; set; }

        public ICollection<EntradaOrdenItem> EntadaOrdenItems { get; set; } 
    }
}