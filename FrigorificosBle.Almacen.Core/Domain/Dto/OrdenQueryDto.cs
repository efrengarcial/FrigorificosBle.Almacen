using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrigorificosBle.Almacen.Core.Domain.Dto
{
    public class OrdenQueryDto
    {
        public long Numero { get; set; }
        public System.DateTime FechaCreacion { get; set; }
        public int IdProveedor { get; set; }
    }
}
