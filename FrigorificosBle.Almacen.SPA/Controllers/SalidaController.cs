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
    }
}
