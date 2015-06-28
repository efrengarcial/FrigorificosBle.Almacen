using FrigorificosBle.Almacen.Core.Domain;
using FrigorificosBle.Almacen.Core.Domain.Dto;
using FrigorificosBle.Almacen.Core.Service;
using FrigorificosBle.Almacen.SPA.Filters;
using FrigorificosBle.Almacen.Util;
using log4net;
using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Web.Http;
using Microsoft.AspNet.Identity;


namespace FrigorificosBle.Almacen.SPA.Controllers
{
    //[Authorize]
    [ExceptionHandlingAttribute]
    [RoutePrefix("api/salida")]
    public class SalidaController : ApiController
    {

        private readonly ISalidaService _salidaService;
        private readonly ILog _logger;

        public SalidaController(ISalidaService salidaService, ILog logger)
        {
            _salidaService = salidaService;
            _logger = logger; 
        }

        [Route("centroCostos")]
        [HttpGet]
        public IEnumerable<CentroCosto> GetCentroCostos()
        {
            return _salidaService.GetCentroCostos();
        }

        [Route("GetByName/{search}")]
        [HttpGet]
        public IEnumerable<CentroCosto> GetByName(String search)
        {
            CentroCostoQueryDto dto = new CentroCostoQueryDto();
            dto.Nombre = search;
            return _salidaService.GetByName(dto);
        }

        [HttpPost]
        [Route("save")]
        public HttpResponseMessage Save([FromBody]Salida salida)
        {
            if (salida.Id == 0)
            {
                _salidaService.Save(salida);

            }

           return Request.CreateResponse(HttpStatusCode.OK, salida.Id);
        }
    }
}
