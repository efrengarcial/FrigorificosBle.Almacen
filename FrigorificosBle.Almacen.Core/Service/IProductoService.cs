using FrigorificosBle.Almacen.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrigorificosBle.Almacen.Core.Service
{
    public interface IProductoService
    {
        IList<Linea> GetLineas();
        IList<Medida> GetMedidas();
    }
}
