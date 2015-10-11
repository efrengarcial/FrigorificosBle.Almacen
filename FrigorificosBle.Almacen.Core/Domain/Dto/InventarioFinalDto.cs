using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrigorificosBle.Almacen.Core.Domain.Dto
{
    public class InventarioFinalDto
    {
        public int IdProducto { get; set; }
        public int CantidadInicial { get; set; }
        public int CantidadFinal { get; set; }
        public int Entradas { get; set; }
        public int Salidas { get; set; }
        public String Producto { get; set; } 

    }
}
