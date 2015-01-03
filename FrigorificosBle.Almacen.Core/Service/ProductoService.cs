﻿using FrigorificosBle.Almacen.Core.Dao;
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
        private readonly ILog _logger;
        private readonly DbContext _context;

        public ProductoService(IRepository<Producto> productRepository,
            IRepository<Linea> lineaRepository,IRepository<Medida> medidaRepository,
            ILog logger, DbContext context)
        {
            _productRepository = productRepository;
            _lineaRepository = lineaRepository;
            _medidaRepository = medidaRepository;
            _logger = logger;
            _context = context;
        }

        public IList<Linea> GetLineas()
        {
            _context.Configuration.ProxyCreationEnabled = false;
            _context.Configuration.LazyLoadingEnabled = false;             
            return _context.Set<Linea>().Include(l=> l.SubLineas).ToList();//_lineaRepository.GetAll();
        }

        public IList<Medida> GetMedidas()
        {
            return _medidaRepository.GetAll();
        }

        public Producto GetById(Int32 id)
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
            _context.Configuration.LazyLoadingEnabled = false;
            IEnumerable<Producto> result= _context.Set<Producto>().Where(p => (p.Nombre.Contains(dto.Nombre) || 
                p.Referencia.Contains(dto.Referencia) || p.Codigo == dto.Codigo)  && p.Activo ).OrderBy(p=> p.Codigo).ToList();
            return result;
        }
    }
}
