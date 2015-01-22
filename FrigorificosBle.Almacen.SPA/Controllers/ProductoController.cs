using FrigorificosBle.Almacen.Core.Domain;
using FrigorificosBle.Almacen.Core.Domain.Dto;
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
    [RoutePrefix("api/producto")]
    //http://www.asp.net/web-api/overview/web-api-routing-and-actions/attribute-routing-in-web-api-2
    public class ProductoController : ApiController
    {
        private readonly IProductoService _productoService;
        private readonly ILog _logger;

        public ProductoController(IProductoService productoService, ILog logger)
        {
            _productoService = productoService;
            _logger = logger;
        }

        [ActionName("lineas")]
        [HttpGet]
        public IEnumerable<Linea> GetLineas()
        {
            return _productoService.GetLineas();
        }

        [ActionName("medidas")]
        [HttpGet]
        public IEnumerable<Medida> GetMedidas()
        {
            return _productoService.GetMedidas();
        }

        
        //public IEnumerable<Producto> Query([FromUri]ProductoQueryDto dto)
        [Route("query/{search}")]
        [HttpGet]
        public IEnumerable<Producto> Query(String search)
        {
            ProductoQueryDto dto = new ProductoQueryDto();
            int codigo;
            int.TryParse(search,out codigo);
            dto.Codigo = codigo;
            dto.Nombre = search;
            dto.Referencia = search;
            return _productoService.Query(dto);
        }

        // GET api/producto/5
        public string Get(int id)
        {
            return "xxx";
        }

        // POST api/producto
        // POST api/<controller>
        [HttpPost]
        [Route("save")]
        public HttpResponseMessage Save([FromBody]Producto producto)
        {
            _productoService.Save(producto);
            return Request.CreateResponse(HttpStatusCode.OK, producto.Id);
        }

        [HttpPost]
        [Route("inactivate")]
        public HttpResponseMessage Inactivate([FromBody]Int32 id)
        {
            Producto producto = _productoService.GetById(id);
            producto.Anulado = true;
            _productoService.Save(producto);
            return Request.CreateResponse(HttpStatusCode.OK, id);
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
