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
using System.Data.Linq;
using FrigorificosBle.Almacen.Core.Util;


namespace FrigorificosBle.Almacen.Core.Service
{
    public class SalidaService : ISalidaService
    {
        private readonly IRepository<Salida> _salidaRepository;
        private readonly ILog _logger;
        private readonly DbContext _context;

        public SalidaService(IRepository<Salida> salidaRepository, ILog logger, DbContext context)
        {
            _salidaRepository = salidaRepository;
            _logger = logger;
            _context = context;
        }

        public void Save(Salida salida)
        {
            if (salida.Id == 0)
            {

                using (TransactionScope t = new TransactionScope())
                {
                    foreach (SalidaItem salidaItem in salida.SalidaItems)
                    {
                        Producto producto = _context.Set<Producto>().Find(salidaItem.IdProducto);

                        if (salidaItem.Cantidad > producto.CantidadInventario)
                        {
                            throw new NotProductsInStockException("No existe la cantidad suficiente de productos en inventario");
                        }

                        if (salidaItem.Cantidad > 0)
                        {
                            producto.CantidadInventario = (producto.CantidadInventario - salidaItem.Cantidad);

                            HistoricoProducto historicoProducto = new HistoricoProducto();
                            historicoProducto.Salidas = salidaItem.Cantidad ;
                            historicoProducto.IdProducto = salidaItem.IdProducto;
                            historicoProducto.IdOrden = salidaItem.IdOrden;
                            historicoProducto.Movimiento = "SALIDA";
                            _context.Set<HistoricoProducto>().Add(historicoProducto);
                        }
                    }
                    _salidaRepository.Insert(salida);
                    t.Complete();
                }
            }
        }

        public IEnumerable<Salida> Query(SalidaQueryDto dto)
        {
            _context.Configuration.ProxyCreationEnabled = false;
            var query = _context.Set<Salida>();
            IQueryable<Salida> result = null;

            if (dto.ConsultarSalidas == true) {
                //Search by Date
                if (dto.StartDate != null && dto.EndDate != null && dto.UserId != null) {
                    result = query.Where(s => DbFunctions.TruncateTime(s.FechaEntrega) >= ((DateTime)dto.StartDate).Date && DbFunctions.TruncateTime((DateTime)s.FechaEntrega) <= ((DateTime)dto.EndDate).Date).Include(s => s.SalidaItems);
                }

                //Search by Solicitador and recibidor
                if (dto.IdSolicitador != null && dto.IdRecibidor != null) {
                    result = query.Where(s => s.IdSolicitador == dto.IdSolicitador && s.IdRecibidor == dto.IdRecibidor).Include(s => s.SalidaItems);
                }

            }
            return result.ToList();
        }
    }
}
