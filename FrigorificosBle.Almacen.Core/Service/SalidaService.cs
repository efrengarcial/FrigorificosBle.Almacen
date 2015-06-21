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
    public class SalidaService: ISalidaService
    {
        private readonly IRepository<Salida> _salidaRepository;

        private readonly ILog _logger;
        private readonly DbContext _context;

        public SalidaService(IRepository<Salida> salidaRepository, 
            ILog logger, DbContext context)
        {
            _salidaRepository = salidaRepository;
            _logger = logger;
            _context = context;
        }
             
    }
}
