using FrigorificosBle.Almacen.Core.Domain;
using FrigorificosBle.Almacen.Core.Domain.Dto;
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
        Producto GetById(Int32 id);
        void Save(Producto producto);
        IEnumerable<Producto> Query(ProductoQueryDto dto);
    }
}
