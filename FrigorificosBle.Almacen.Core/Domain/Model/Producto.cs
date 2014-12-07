using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrigorificosBle.Almacen.Core.Domain
{
    partial class Producto
    {
        public int IdLinea
        {
            get
            {
                if (this.SubLinea != null)
                {
                    return this.SubLinea.IdLinea;
                }
                else
                {
                    return -1;
                }
            }

        }
    }
}
