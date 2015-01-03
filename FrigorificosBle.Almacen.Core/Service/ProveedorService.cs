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


namespace FrigorificosBle.Almacen.Core.Service
{
    public class ProveedorService: IProveedorService
    {
        private readonly IRepository<Proveedor> _proveedorRepository;

        private readonly ILog _logger;
        private readonly DbContext _context;

        public ProveedorService(IRepository<Proveedor> proveedorRepository, 
            ILog logger, DbContext context)
        {
            _proveedorRepository = proveedorRepository;
            _logger = logger;
            _context = context;
        }

        public void Save(Proveedor proveedor)
        {
            _proveedorRepository.Insert(proveedor);
        }

        public IEnumerable<Proveedor> Query(ProveedorQueryDto dto)
        {
            IEnumerable<Proveedor> result = _context.Set<Proveedor>().Where(p => p.Nombre.Contains(dto.Nombre) || 
                p.Nit == dto.Nit).OrderBy(p => p.Nit).ToList();
            return result;
        }
    }
}