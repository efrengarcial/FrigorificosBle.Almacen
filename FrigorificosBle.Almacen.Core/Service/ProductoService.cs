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
using System.Data.Linq;
using System.Diagnostics;

//https://simpleinjector.codeplex.com/wikipage?title=Quick%20Start

//http://johnnycode.com/2012/04/10/serializing-circular-references-with-json-net-and-entity-framework/
//https://code.msdn.microsoft.com/Loop-Reference-handling-in-caaffaf7
//http://thecodeseeker.blogspot.com/2014/03/web-api-self-referencing-loop-detected.html

//http://blog.oneunicorn.com/2013/05/08/ef6-sql-logging-part-1-simple-logging/
//http://stackoverflow.com/questions/1412863/how-do-i-view-the-sql-generated-by-the-entity-framework

namespace FrigorificosBle.Almacen.Core.Service
{
    public class ProductoService : IProductoService
    {
        private readonly IRepository<Producto> _productRepository;
        private readonly IRepository<Linea> _lineaRepository;
        private readonly IRepository<Medida> _medidaRepository;
        private readonly IRepository<Moneda> _monedaRepository;
        private readonly ILog _logger;
        private readonly DbContext _context;

        public ProductoService(IRepository<Producto> productRepository,
            IRepository<Linea> lineaRepository, IRepository<Medida> medidaRepository, 
            IRepository<Moneda> monedaRepository,
            ILog logger, DbContext context)
        {
            _productRepository = productRepository;
            _lineaRepository = lineaRepository;
            _medidaRepository = medidaRepository;
            _monedaRepository = monedaRepository;
            _logger = logger;
            _context = context;
        }

        public IList<Linea> GetLineas()
        {
            _context.Configuration.ProxyCreationEnabled = false;
            return _context.Set<Linea>().Include(l => l.SubLineas).ToList();//_lineaRepository.GetAll();
        }

        public IList<Medida> GetMedidas()
        {
            return _medidaRepository.GetAll();
        }

        public IList<Moneda> GetMonedas()
        {
            return _monedaRepository.GetAll();
        }


        public Producto GetById(long id)
        {
            return _productRepository.GetById(id);
        }

        public void Save(Producto producto)
        {
            if (producto.Id == 0)
            {
                _productRepository.Insert(producto);
            }
            else
            {
                _productRepository.Update(producto);

            }
        }

        public IEnumerable<Producto> Query(ProductoQueryDto dto)
        {
            _context.Configuration.ProxyCreationEnabled = false;
            IEnumerable<Producto> result = null;
            if (dto.EsServicio != null)
            {
                result = _context.Set<Producto>().Where(p => (p.Nombre.Contains(dto.Nombre) ||
                  p.Referencia.Contains(dto.Referencia) || p.Codigo == dto.Codigo) && !p.Anulado && p.EsServicio == dto.EsServicio).
                  Include(p => p.Medida).OrderBy(p => p.Codigo).ToList();
            }
            else
            {
                result = _context.Set<Producto>().Where(p => (p.Nombre.Contains(dto.Nombre) ||
                    p.Referencia.Contains(dto.Referencia) || p.Codigo == dto.Codigo) && !p.Anulado).
                    Include(p => p.Medida).OrderBy(p => p.Codigo).ToList();
            }
            return result;
        }

        public IList<CentroCosto> GetCentroCostos()
        {
            _context.Configuration.ProxyCreationEnabled = false;
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
