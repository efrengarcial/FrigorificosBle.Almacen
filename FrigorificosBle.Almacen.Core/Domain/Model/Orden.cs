using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrigorificosBle.Almacen.Core.Domain
{
    public partial class Orden
    {
        public long FechaCreacionLong { 
            get 
            { 
                return this.FechaCreacion.Ticks; 
            } 
        }
    }
}
