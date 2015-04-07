using FrigorificosBle.Almacen.Core.Domain;
using FrigorificosBle.Almacen.Core.Domain.Dto;
using FrigorificosBle.Almacen.Core.Service;
using FrigorificosBle.Almacen.SPA.Filters;
using FrigorificosBle.Almacen.Util;
using log4net;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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

        [Route("query")]
        [HttpGet]
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
        public Orden Get(long id)
        {
            return _ordenService.GetById(id);
        }
      
        [HttpPost]
        [Route("save")]
        public HttpResponseMessage Save([FromBody]Orden orden)
        {
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

        //http://blog.hexacta.com/typeahead-for-angularjs-with-ajax/
        [Route("getInboxOrden")]
        [HttpGet]
        public IEnumerable<Orden> GetInboxOrden()
        {
            return _ordenService.GetInboxOrden();
        }
    }
}
