using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrigorificosBle.Almacen.Core.Domain.Dto
{
    public class ProductoQueryDto
    {
        public int Codigo { get; set; }
        public string Referencia { get; set; }
        public string Nombre { get; set; }
    }
}
