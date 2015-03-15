using FrigorificosBle.Almacen.Core.Dao;
using FrigorificosBle.Almacen.Core.Domain;
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


namespace FrigorificosBle.Almacen.Core.Service
{
    public class OrdenService: IOrdenService
    {
        private readonly IRepository<Orden> _ordenRepository;

        private readonly ILog _logger;
        private readonly DbContext _context;

        public OrdenService(IRepository<Orden> ordenRepository, 
            ILog logger, DbContext context)
        {
            _ordenRepository = ordenRepository;
            _logger = logger;
            _context = context;
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
                result = query.Where(p => p.Numero == dto.Numero);
            }
            else if (dto.StartDate != null && dto.EndDate !=null)
            {
                result = query.Where(p => DbFunctions.TruncateTime(p.FechaCreacion) >= ((DateTime)dto.StartDate).Date &&
                     DbFunctions.TruncateTime((DateTime)p.FechaCreacion) <= ((DateTime)dto.EndDate).Date);
            }
            return result.ToList();
        }


    }
}
