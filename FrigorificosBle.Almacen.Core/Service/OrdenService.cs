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
    public class OrdenService : IOrdenService
    {
        private readonly IRepository<Orden> _ordenRepository;

        private readonly ILog _logger;
        private readonly DbContext _context;

        public readonly string REQUISICION = TipoOrdenEnum.REQUISICION.AsText();
        public readonly string REQUISICION_SERVICIO = TipoOrdenEnum.REQUISICION_SERVICIO.AsText();
        public readonly string ORDEN_ABIERTA = OrdenEstadoEnum.ABIERTA.AsText();
        public readonly string ORDEN_EN_CURSO = OrdenEstadoEnum.EN_CURSO.AsText();
        public readonly string ORDEN_COMPRA = TipoOrdenEnum.ORDEN_COMPRA.AsText();
        public readonly string ORDEN_SERVICIO = TipoOrdenEnum.ORDEN_SERVICIO.AsText();
        public readonly string ESTADO = OrdenEstadoEnum.CERRADA.AsText();


        public OrdenService(IRepository<Orden> ordenRepository,
            ILog logger, DbContext context)
        {
            _ordenRepository = ordenRepository;
            _logger = logger;
            _context = context;
        }

        public Orden GetById(long id)
        {

            //_context.Set<Orden>().Single(o => o.Id == id)
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

                    if (orden.IdOrdenBase != null)
                    {
                        var requisicion = _ordenRepository.GetById(orden.IdOrdenBase);
                        requisicion.Estado = ESTADO;
                        _ordenRepository.Update(requisicion);
                    }

                    var numeroOrden = ((AlmacenDbContext)_context).CrearNumeroOrden(secuencia);
                    orden.Numero = numeroOrden.SingleOrDefault().Value;
                    _ordenRepository.Insert(orden);
                    t.Complete();
                }
            }
            else
            {
                using (TransactionScope t = new TransactionScope())
                {
                    var ordenEntity = _context.Set<Orden>();
                    ordenEntity.Attach(orden);
                    _context.Entry(orden).State = EntityState.Modified;

                    foreach (OrdenItem item in orden.OrdenItems)
                    {
                        var ordenItemEntity = _context.Set<OrdenItem>();
                        ordenItemEntity.Attach(item);
                        _context.Entry(item).State = EntityState.Modified;
                    }

                    _context.SaveChanges();
                    t.Complete();
                }
            }
        }

        public void SaveEntrada(EntradaOrden entradaOrden)
        {
            using (TransactionScope t = new TransactionScope())
            {
                Orden orden = GetById(entradaOrden.IdOrden);
                bool ordenEnCurso = false;

                foreach (OrdenItem item in orden.OrdenItems)
                {
                    EntradaOrdenItem entradaOrdenItem = entradaOrden.EntadaOrdenItems.Single(e => e.IdOrdenItem == item.Id);

                    if (entradaOrdenItem.Aprovisionado > 0)
                    {
                        item.Aprovisionado = item.Aprovisionado + entradaOrdenItem.Aprovisionado;
                        Entrada entrada = new Entrada();
                        entrada.OrdenItem = item;
                        entrada.IdProducto = item.IdProducto;
                        entrada.Cantidad = entradaOrdenItem.Aprovisionado;
                        entrada.Precio = item.Precio;
                        item.Entradas.Add(entrada);

                        Producto producto = _context.Set<Producto>().Find(item.IdProducto);
                        producto.CantidadInventario = producto.CantidadInventario + entradaOrdenItem.Aprovisionado;

                        HistoricoProducto historicoProducto = new HistoricoProducto();
                        historicoProducto.Cantidad = entradaOrdenItem.Aprovisionado;
                        historicoProducto.IdProducto = item.IdProducto;
                        historicoProducto.IdOrden = orden.Id;
                        _context.Set<HistoricoProducto>().Add(historicoProducto);

                    }
                    if ((item.Cantidad - item.Aprovisionado) > 0)
                    {
                        ordenEnCurso = true;
                    }
                }

                if (ordenEnCurso)
                {
                    orden.Estado = OrdenEstadoEnum.EN_CURSO.AsText();
                }
                else
                {
                    orden.Estado = OrdenEstadoEnum.CERRADA.AsText();
                }

                _context.SaveChanges();
                t.Complete();
            }
        }


        public IEnumerable<Orden> Query(OrdenQueryDto dto)
        {
            _context.Configuration.ProxyCreationEnabled = false;
            //_context.Configuration.LazyLoadingEnabled = false;
            var query = _context.Set<Orden>();
            IQueryable<Orden> result = null;

            //Search by Number
            if (dto.Numero != null)
            {
                result = query.Where(p => p.Numero == dto.Numero).Include(p => p.Proveedor);
            }

            //Search by Date
            else if (dto.StartDate != null && dto.EndDate != null && dto.IdProveedor == null && dto.UserId == null)
            {
                result = query.Where(p => DbFunctions.TruncateTime(p.FechaCreacion) >= ((DateTime)dto.StartDate).Date &&
                     DbFunctions.TruncateTime((DateTime)p.FechaCreacion) <= ((DateTime)dto.EndDate).Date).Include(p => p.Proveedor);
            }
            //Search by Date and Prveedor
            else if (dto.StartDate != null && dto.EndDate != null && dto.IdProveedor != null && dto.UserId == null)
            {
                result = query.Where(p => DbFunctions.TruncateTime(p.FechaCreacion) >= ((DateTime)dto.StartDate).Date &&
                     DbFunctions.TruncateTime((DateTime)p.FechaCreacion) <= ((DateTime)dto.EndDate).Date && (p.IdProveedor == dto.IdProveedor)).Include(p => p.Proveedor);
            }
            //Search by Date and User
            else if (dto.StartDate != null && dto.EndDate != null && dto.UserId != null && dto.IdProveedor == null)
            {
                result = query.Where(p => DbFunctions.TruncateTime(p.FechaCreacion) >= ((DateTime)dto.StartDate).Date &&
                    DbFunctions.TruncateTime((DateTime)p.FechaCreacion) <= ((DateTime)dto.EndDate).Date && (p.UserId == dto.UserId)).Include(p => p.Proveedor);

            }
            //Search by Date and Proveedor and User
            else if (dto.StartDate != null && dto.EndDate != null && dto.UserId != null && dto.IdProveedor != null)
            {
                result = query.Where(p => DbFunctions.TruncateTime(p.FechaCreacion) >= ((DateTime)dto.StartDate).Date &&
                    DbFunctions.TruncateTime((DateTime)p.FechaCreacion) <= ((DateTime)dto.EndDate).Date && (p.IdProveedor == dto.IdProveedor) && (p.UserId == dto.UserId)).Include(p => p.Proveedor);
            }
            return result.ToList();
        }

        public IEnumerable<Orden> GetInboxOrden()
        {
            _context.Configuration.ProxyCreationEnabled = false;
            return _context.Set<Orden>().Where(orden => (ORDEN_ABIERTA.Equals(orden.Estado)) && (REQUISICION.Equals(orden.Tipo) ||
                REQUISICION_SERVICIO.Equals(orden.Tipo)) && orden.Anulada == false).Include(p => p.Proveedor).OrderBy(orden => orden.FechaCreacion).ToList();
        }

        public IEnumerable<Orden> GetOrdenesCompraAbiertas()
        {
            _context.Configuration.ProxyCreationEnabled = false;
            return _context.Set<Orden>().Where(orden => (ORDEN_ABIERTA.Equals(orden.Estado) || ORDEN_EN_CURSO.Equals(orden.Estado))
                && (ORDEN_COMPRA.Equals(orden.Tipo) || ORDEN_SERVICIO.Equals(orden.Tipo)) && orden.Anulada == false).Include(p => p.Proveedor).
                OrderBy(orden => orden.FechaCreacion).ToList();
        }


        public IEnumerable<Orden> GetOrdenByNum(long ordenNum)
        {
            _context.Configuration.ProxyCreationEnabled = false;
            IEnumerable<Orden> result = null;
            result = _context.Set<Orden>().Where(orden => (orden.Numero == ordenNum) && (orden.Anulada == false) && (ORDEN_ABIERTA.Equals(orden.Estado)) && (ORDEN_COMPRA.Equals(orden.Tipo))).Include(ordenItem => ordenItem.OrdenItems).ToList();
            return result;
        }

    }
}
