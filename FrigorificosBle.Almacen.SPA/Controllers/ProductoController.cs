﻿using FrigorificosBle.Almacen.Core.Domain;
using FrigorificosBle.Almacen.Core.Domain.Dto;
using FrigorificosBle.Almacen.Core.Service;
using FrigorificosBle.Almacen.SPA.Filters;
using FrigorificosBle.Security.Infrastructure;
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
    [ClaimsAuthorization(Permission = "CONSULTAR_PRODUCTOS")]
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

        [Route("lineas")]
        [HttpGet]
        public IEnumerable<Linea> GetLineas()
        {
            return _productoService.GetLineas();
        }

        [Route("medidas")]
        [HttpGet]
        public IEnumerable<Medida> GetMedidas()
        {
            return _productoService.GetMedidas();
        }

        [Route("monedas")]
        [HttpGet]
        public IEnumerable<Moneda> GeMonedas()
        {
            return _productoService.GetMonedas();
        }

        // GET api/producto/5
        [Route("getById/{Id}")]
        [HttpGet]
        public Producto GetById(long id)
        {
            return _productoService.GetById(id);
        }


        [Route("query/{search}")]
        [HttpGet]
        public IEnumerable<Producto> Query(String search)
        {
            ProductoQueryDto dto = new ProductoQueryDto();
            int codigo;
            int.TryParse(search, out codigo);
            dto.Codigo = codigo;
            dto.Nombre = search;
            dto.Referencia = search;
            return _productoService.Query(dto);
        }

        [Route("query/{search}/{esServicio}")]
        [HttpGet]
        public IEnumerable<Producto> Query(String search, Boolean esServicio)
        {
            ProductoQueryDto dto = new ProductoQueryDto();
            int codigo;
            int.TryParse(search, out codigo);
            dto.Codigo = codigo;
            dto.Nombre = search;
            dto.Referencia = search;
            dto.EsServicio = esServicio;
            return _productoService.Query(dto);
        }



        // POST api/producto
        // POST api/<controller>
        [HttpPost]
        [Route("save")]
        [ClaimsAuthorization(Permission = "ADMIN_PRODUCTOS")]
        public HttpResponseMessage Save([FromBody]Producto producto)
        {
            _productoService.Save(producto);
            return Request.CreateResponse(HttpStatusCode.OK, producto.Id);
        }

        [HttpPost]
        [Route("inactivate")]
        [ClaimsAuthorization(Permission = "ADMIN_PRODUCTOS")]
        public HttpResponseMessage Inactivate([FromBody]Int32 id)
        {
            Producto producto = _productoService.GetById(id);
            producto.Anulado = true;
            _productoService.Save(producto);
            return Request.CreateResponse(HttpStatusCode.OK, id);
        }

        [Route("centroCostos")]
        [HttpGet]
        public IEnumerable<CentroCosto> GetCentroCostos()
        {
            return _productoService.GetCentroCostos();
        }

        [Route("GetByName/{search}")]
        [HttpGet]
        public IEnumerable<CentroCosto> GetByName(String search)
        {
            CentroCostoQueryDto dto = new CentroCostoQueryDto();
            dto.Nombre = search;
            return _productoService.GetByName(dto);
        }
    }
}
