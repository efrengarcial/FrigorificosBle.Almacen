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
using FrigorificosBle.Almacen.Core.Domain.Enum;
using FrigorificosBle.Security.Infrastructure;
using FrigorificosBle.Almacen.Core.Util;

namespace FrigorificosBle.Almacen.SPA.Controllers
{
    [ExceptionHandlingAttribute]
    [RoutePrefix("api/orden")]
    //http://www.asp.net/web-api/overview/web-api-routing-and-actions/attribute-routing-in-web-api-2
    public class OrdenController : BaseApiController
    {
        private readonly IOrdenService _ordenService;
        private readonly ILog _logger;

        public OrdenController(IOrdenService ordenService, ILog logger)
        {
            _ordenService = ordenService;
            _logger = logger;
        }
      

        [HttpGet]
        [Route("query")]
        [ClaimsAuthorization(Permission = Permissions.ConsultarOrdenes)]
        public IEnumerable<Orden> Query()
        {
            var queryString = this.Request.GetQueryStrings();
            OrdenQueryDto dto = new OrdenQueryDto();

            foreach (KeyValuePair<string, string> element in queryString)
            {
                Debug.WriteLine("Value: " + element.Value + " Key: " + element.Key);

                switch (element.Key)
                {
                    case "Numero":
                        dto.Numero = Int64.Parse(element.Value);
                        break;
                    case "StartDate":
                        dto.StartDate = CommonsTools.StringToDateTime(element.Value);
                        break;
                    case "EndDate":
                        dto.EndDate = CommonsTools.StringToDateTime(element.Value);
                        break;
                    case "IdProveedor":
                        dto.IdProveedor = Int32.Parse(element.Value);
                        break;
                    default:
                        break;
                }                
            }
            return _ordenService.Query(dto);
        }

        // GET api/orden/5
        [HttpGet]
        [Route("getById/{Id}")]
        [ClaimsAuthorization(Permission = StringEnum.GetStringValue(Permissions.ConsultarOrdenes))]
        public Orden GetById(long id)
        {
            return _ordenService.GetById(id);
        }
      
        [HttpPost]
        [Route("save")]
        public HttpResponseMessage Save([FromBody]Orden orden)
        {
            if (orden.Id == 0)
            {
                orden.UserId = 1; //User.Identity.GetUserId();
                orden.UserName = User.Identity.GetUserName();
                orden.Estado = OrdenEstadoEnum.ABIERTA.AsText();
            }
            else
            {
                orden.Estado = OrdenEstadoEnum.EN_CURSO.AsText();
            }
            _ordenService.Save(orden);
            return Request.CreateResponse(HttpStatusCode.OK, orden.Id);
        }

        [HttpPost]
        [Route("inactivate")]
        public HttpResponseMessage Inactivate([FromBody]Int32 id)
        {
            Orden orden = _ordenService.GetById(id);
            orden.Anulada = true;
            _ordenService.Save(orden);
            return Request.CreateResponse(HttpStatusCode.OK, id);
        }

        [HttpGet]
        [Route("getInboxOrden")]
        [ClaimsAuthorization(Permission = StringEnum.GetStringValue(Permissions.RequisicionesProcesar))]
        public IEnumerable<Orden> GetInboxOrden()
        {
            return _ordenService.GetInboxOrden(); 

        }

        [Route("getOrdenesCompraAbiertas")]
        [HttpGet]
        [ClaimsAuthorization(Permission = StringEnum.GetStringValue(Permissions.Entradas))]
        public IEnumerable<Orden> GetOrdenesCompraAbiertas()
        {
            return _ordenService.GetOrdenesCompraAbiertas();
        }

        [Route("getOrdenByNum/{ordenNum}")]
        [HttpGet]
        [ClaimsAuthorization(Permission = StringEnum.GetStringValue(Permissions.ConsultarOrdenes))]
        public IEnumerable<Orden> GetOrdenByNum(long ordenNum)
        {
            return _ordenService.GetOrdenByNum(ordenNum);
        }
    }
}
