using FrigorificosBle.Almacen.Core.Dao;
using FrigorificosBle.Almacen.Core.Domain;
using log4net;
using log4net.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//https://simpleinjector.codeplex.com/wikipage?title=Quick%20Start
namespace FrigorificosBle.Almacen.Core.Service
{
    public class ProductoService : IProductoService
    {
        private readonly IRepository<Producto> _productRepository;
        private readonly IRepository<Linea> _lineaRepository;
        private readonly ILog _logger;

        public ProductoService(IRepository<Producto> productRepository,
            IRepository<Linea> lineaRepository, ILog logger)
        {
            _productRepository = productRepository;
            _lineaRepository = lineaRepository;
            _logger = logger;
        }

        public IList<Linea> GetLineas()
        {
            return _lineaRepository.GetAll();
        }
    }
}
