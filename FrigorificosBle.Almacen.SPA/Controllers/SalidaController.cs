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
using FrigorificosBle.Almacen.Core.Util;


namespace FrigorificosBle.Almacen.SPA.Controllers
{
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

        [HttpPost]
        [Route("save")]
        public HttpResponseMessage Save([FromBody]Salida salida)
        {
            if (salida.Id == 0)
            {
                try
                {
                    _salidaService.Save(salida);
                }
                catch (NotProductsInStockException ex)
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound, ex);
                }

            }
            return Request.CreateResponse(HttpStatusCode.OK, salida.Id);
        }
    }
}
