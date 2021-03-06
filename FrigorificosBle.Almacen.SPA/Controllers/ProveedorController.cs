﻿using FrigorificosBle.Almacen.Core.Domain;
using FrigorificosBle.Almacen.Core.Domain.Dto;
using FrigorificosBle.Almacen.Core.Service;
using FrigorificosBle.Almacen.SPA.Filters;
using FrigorificosBle.Security.Infrastructure;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace FrigorificosBle.Almacen.SPA.Controllers

{
    [ExceptionHandlingAttribute]
    [RoutePrefix("api/proveedor")]
    [ClaimsAuthorization(Permission = "CONSULTAR_PROVEEDORES")]
    public class ProveedorController : ApiController 
    {

        private readonly IProveedorService _proveedorService;
        private readonly ILog _logger;

        public ProveedorController(IProveedorService proveedorService, ILog logger)
        {
            _proveedorService = proveedorService;
            _logger = logger; 
        }


        //public IEnumerable<Proveedor> Query([FromUri]ProveedorQueryDto dto)
        [Route("query/{search}")]
        [HttpGet]
        public IEnumerable<Proveedor> Query(String search)
        {
            ProveedorQueryDto dto = new ProveedorQueryDto();
            int nit;
            int.TryParse(search, out nit);
            dto.Nit = nit;
            dto.Nombre = search;
            return _proveedorService.Query(dto);
        }

  
        // POST api/proveedor
        // POST api/<controller>
        [HttpPost]
        [Route("save")]
        [ClaimsAuthorization(Permission = "ADMIN_PROVEEDORES")]
        public HttpResponseMessage Save([FromBody]Proveedor proveedor)
        {
            _proveedorService.Save(proveedor);
            return Request.CreateResponse(HttpStatusCode.OK, proveedor.Id);
        }


        [HttpPost]
        [Route("inactivate")]
        [ClaimsAuthorization(Permission = "ADMIN_PROVEEDORES")]
        public HttpResponseMessage Inactivate([FromBody]Int32 id)
        {
            Proveedor proveedor = _proveedorService.GetById(id);
            proveedor.Anulado = true;
            _proveedorService.Save(proveedor);
            return Request.CreateResponse(HttpStatusCode.OK, id);
        }

        //http://blog.hexacta.com/typeahead-for-angularjs-with-ajax/
        [Route("getAll")]
        [HttpGet]
        public IEnumerable<Proveedor> GetAll()
        {
            return _proveedorService.GetALl();
        }


    }
}
