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
    [RoutePrefix("api/orden")]
    //http://www.asp.net/web-api/overview/web-api-routing-and-actions/attribute-routing-in-web-api-2
    public class OrdenController : ApiController
    {
        private readonly IOrdenService _ordenService;
        private readonly ILog _logger;

        public OrdenController(IOrdenService ordenService, ILog logger)
        {
            _ordenService = ordenService;
            _logger = logger;
        }
       
        [HttpPost]
        [Route("save")]
        public HttpResponseMessage Save([FromBody]Orden orden)
        {
            _ordenService.Save(orden);
            return Request.CreateResponse(HttpStatusCode.OK, orden.Id);
        }
    }
}
