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
        private readonly IRepository<CentroCosto> _centroCostoRepository;
        private readonly IRepository<Producto> _productoRepository;
        private readonly ILog _logger;
        private readonly DbContext _context;

        public SalidaService(IRepository<Salida> salidaRepository, IRepository<CentroCosto> centroCostoRepository, IRepository<Producto> productoRepository,
            ILog logger, DbContext context)
        {
            _salidaRepository = salidaRepository;
            _centroCostoRepository = centroCostoRepository;
            _productoRepository = productoRepository;
            _logger = logger;
            _context = context;
        }

        /*public void Save(Salida salida) {
 
            if(salida.Id == 0){

                using (TransactionScope t = new TransactionScope())
                {
                    _salidaRepository.Insert(salida);
                    t.Complete();
                }            
            }        
        }*/

        public void Save(Salida salida)
        {


            if (salida.Id == 0)
            {

                using (TransactionScope t = new TransactionScope())
                {
                    //bool ordenEnCurso = false;

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
                            historicoProducto.Cantidad = (salidaItem.Cantidad * -1);
                            historicoProducto.IdProducto = salidaItem.IdProducto;
                            historicoProducto.IdOrden = salidaItem.IdOrden;
                            _context.Set<HistoricoProducto>().Add(historicoProducto);
                        }

                    }
                    _salidaRepository.Insert(salida);
                    t.Complete();
                }

            }
        }

        public IList<CentroCosto> GetCentroCostos()
        {
            _context.Configuration.ProxyCreationEnabled = false;
            //_context.Configuration.LazyLoadingEnabled = false;
            return _context.Set<CentroCosto>().ToList();
        }


        public IEnumerable<CentroCosto> GetByName(CentroCostoQueryDto dto)
        {
            _context.Configuration.ProxyCreationEnabled = false;
            IEnumerable<CentroCosto> result = null;
            result = _context.Set<CentroCosto>().Where(p => (p.Nombre.Contains(dto.Nombre)));
            return result;
        }
    }
}
