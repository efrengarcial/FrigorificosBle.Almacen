using FrigorificosBle.Almacen.Core.Dao;
using FrigorificosBle.Almacen.Core.Domain;
using Microsoft.VisualBasic;
using log4net;
using log4net.Core;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Linq.SqlClient;
using FrigorificosBle.Almacen.Core.Domain.Dto;
using System.Transactions;
using System.Data.Entity.Core.Objects;
using FrigorificosBle.Almacen.Core.Domain.Enum;
using System.Diagnostics;


namespace FrigorificosBle.Almacen.Core.Service
{
    public class OrdenService: IOrdenService
    {
        private readonly IRepository<Orden> _ordenRepository;

        private readonly ILog _logger;
        private readonly DbContext _context;

        public readonly string REQUISICION = TipoOrdenEnum.REQUISICION.AsText();
        public readonly string REQUISICION_SERVICIO = TipoOrdenEnum.REQUISICION_SERVICIO.AsText();
        public readonly string ORDEN_ABIERTA = OrdenEstadoEnum.ABIERTA.AsText();

        public OrdenService(IRepository<Orden> ordenRepository, 
            ILog logger, DbContext context)
        {
            _ordenRepository = ordenRepository;
            _logger = logger;
            _context = context;
        }

        public Orden GetById(Int32 id)
        {

            return _ordenRepository.GetById(id);
        }

        public void Save(Orden orden)
        {
            if (orden.Id == 0)
            {
                using (TransactionScope t = new TransactionScope())
                {
                    String secuencia = TipoOrdenEnum.REQUISICION.AsSecuencia();
                    if (TipoOrdenEnum.ORDEN_COMPRA.AsText().Equals(orden.Tipo))
                    {
                        secuencia = TipoOrdenEnum.ORDEN_COMPRA.AsSecuencia();
                    }
                    else if (TipoOrdenEnum.ORDEN_SERVICIO.AsText().Equals(orden.Tipo))
                    {
                        secuencia = TipoOrdenEnum.ORDEN_SERVICIO.AsSecuencia();
                    }
                    else if (TipoOrdenEnum.REQUISICION_SERVICIO.AsText().Equals(orden.Tipo))
                    {
                        secuencia = TipoOrdenEnum.REQUISICION_SERVICIO.AsSecuencia();
                    }
 
                    var numeroOrden = ((AlmacenDbContext)_context).CrearNumeroOrden(secuencia);
                    orden.Numero = numeroOrden.SingleOrDefault().Value;
                    _ordenRepository.Insert(orden);
                    t.Complete();
                }
            }
            else
            {
                _ordenRepository.Update(orden);
            }
        }

        public IEnumerable<Orden> Query(OrdenQueryDto dto)
        {
            _context.Configuration.ProxyCreationEnabled = false;
            _context.Configuration.LazyLoadingEnabled = false;
            var query = _context.Set<Orden>();
            IQueryable<Orden> result = null;

            if (dto.Numero != null)
            {
                result = query.Where(p => p.Numero == dto.Numero && p.Anulada == false).Include(p => p.Proveedor);
            }
            else if (dto.StartDate != null && dto.EndDate != null && dto.IdProveedor == null)
            {
                result = query.Where(p => DbFunctions.TruncateTime(p.FechaCreacion) >= ((DateTime)dto.StartDate).Date &&
                     DbFunctions.TruncateTime((DateTime)p.FechaCreacion) <= ((DateTime)dto.EndDate).Date && p.Anulada == false).Include(p => p.Proveedor);
            }

            else if (dto.StartDate != null && dto.EndDate != null && dto.IdProveedor != null)
            {
                result = query.Where(p => DbFunctions.TruncateTime(p.FechaCreacion) >= ((DateTime)dto.StartDate).Date &&
                     DbFunctions.TruncateTime((DateTime)p.FechaCreacion) <= ((DateTime)dto.EndDate).Date && (p.IdProveedor == dto.IdProveedor) && p.Anulada == false).Include(p => p.Proveedor);
            }
            return result.ToList();
        }

        public IEnumerable<Orden> GetInboxOrden()
        {
            return _context.Set<Orden>().Where(orden => (ORDEN_ABIERTA.Equals(orden.Estado)) && (REQUISICION.Equals(orden.Tipo) ||
                REQUISICION_SERVICIO.Equals(orden.Tipo))).Include(p => p.Proveedor).OrderBy(orden => orden.FechaCreacion).ToList();
        }
    }
}
