using FrigorificosBle.Almacen.Core.Domain;
using FrigorificosBle.Almacen.Core.Service;
using FrigorificosBle.Almacen.SPA.Filters;
using log4net;
using log4net.Core;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace FrigorificosBle.Almacen.SPA.Controllers
{
    [ExceptionHandlingAttribute]
    public class ProductoController : ApiController
    {
        private readonly IProductoService _productoService;
        private readonly ILog _logger;

        public ProductoController(IProductoService productoService, ILog logger)
        {
            _productoService = productoService;
            _logger = logger;
        }

        // GET api/lineas
        [ActionName("lineas")]
        [HttpGet]
        public IEnumerable<Linea> GetLineas()
        {
            return _productoService.GetLineas();
        }

        // GET api/medidas
        [ActionName("medidas")]
        [HttpGet]
        public IEnumerable<Medida> GetMedidas()
        {
            return _productoService.GetMedidas();
        }

        // GET api/producto/5
        public string Get(int id)
        {
            return "xxx";
        }

        // POST api/producto
        // POST api/<controller>
        public void Post([FromBody]Producto producto)
        {
            try
            {
                AlmacenContext context = new AlmacenContext();
                context.Productos.Add(producto);
                context.SaveChanges();
            } catch (DbEntityValidationException dbEx)
            {
                /*foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        Trace.TraceInformation("Property: {0} Error: {1}", validationError.PropertyName, validationError.ErrorMessage);
                    }
                }*/
                // Retrieve the error messages as a list of strings.
                var errorMessages = dbEx.EntityValidationErrors
                        .SelectMany(x => x.ValidationErrors)
                        .Select(x => x.ErrorMessage);

                // Join the list to a single string.
                var fullErrorMessage = string.Join("; ", errorMessages);

                // Combine the original exception message with the new one.
                var exceptionMessage = string.Concat(dbEx.Message, " The validation errors are: ", fullErrorMessage);
                _logger.Error(exceptionMessage);

                // Throw a new DbEntityValidationException with the improved exception message.
                throw new DbEntityValidationException(exceptionMessage, dbEx.EntityValidationErrors);

            }
        }

        // PUT api/producto/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/producto/5
        public void Delete(int id)
        {
        }
    }
}
