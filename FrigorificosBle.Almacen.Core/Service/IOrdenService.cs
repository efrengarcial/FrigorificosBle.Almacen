using FrigorificosBle.Almacen.Core.Domain;
using FrigorificosBle.Almacen.Core.Domain.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrigorificosBle.Almacen.Core.Service
{
    public interface IOrdenService
    {
        Orden GetById(long id);
        void Save(Orden orden);
        IEnumerable<Orden> Query(OrdenQueryDto dto);
        IEnumerable<Orden> GetInboxOrden();
        IEnumerable<Orden> GetOrdenesCompraAbiertas();
        IEnumerable<Orden> GetOrdenByNum(OrdenQueryDto dto);

    }
}
