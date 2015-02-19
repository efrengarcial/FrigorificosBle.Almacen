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
                _ordenRepository.Insert(orden);
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
            IEnumerable<Orden> result = _context.Set<Orden>().Where(p => ((p.Numero == dto.Numero) || (p.IdProveedor == dto.IdProveedor))).OrderBy(p => p.Numero).ToList();
            return result;
        }
    }
}
