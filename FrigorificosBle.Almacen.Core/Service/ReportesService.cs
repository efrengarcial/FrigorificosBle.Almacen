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
    public class ReportesService : IReportesService
    {
        private readonly ILog _logger;
        private readonly DbContext _context;

        public ReportesService(ILog logger, DbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public InventarioFinalDto ConsultarInventarioFinal(DateTime fechaIni, DateTime fechaFin)
        {
            _context.Configuration.ProxyCreationEnabled = false;
            var consolidado = _context.Set<InventarioFinal>().Where(i => i.Fecha >= fechaIni.AddDays(-1) &&
                i.Fecha <= fechaFin).
                    GroupBy(row => new { row.IdProducto  } ).
                    Select(g => new InventarioFinalDto
                    {
                        IdProducto = g.Key.IdProducto,
                        Entradas = g.Sum( s => s.Entradas),
                        Salidas = g.Sum(s => s.Salidas
                        )
                    });
                   
            return null;
        }
    }
}
