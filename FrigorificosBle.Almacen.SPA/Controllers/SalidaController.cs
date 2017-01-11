using FrigorificosBle.Almacen.Core.Domain;
using FrigorificosBle.Almacen.Core.Domain.Dto;
using FrigorificosBle.Almacen.Core.Service;
using FrigorificosBle.Almacen.SPA.Filters;
using FrigorificosBle.Almacen.Util;
using log4net;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using FrigorificosBle.Almacen.Core.Util;
using FrigorificosBle.Security.Infrastructure;


namespace FrigorificosBle.Almacen.SPA.Controllers
{
    [ExceptionHandlingAttribute]
    [RoutePrefix("api/salida")]
    public class SalidaController : BaseApiController
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

        [HttpGet]
        [Route("query")]
        [ClaimsAuthorization(Permission = "CONSULTAR_SALIDAS")]
        public IEnumerable<Salida> Query()
        {
            var queryString = this.Request.GetQueryStrings();
            SalidaQueryDto dto = new SalidaQueryDto();

            List<String> permissions = this.Permissions();
            string value = "CONSULTAR_SALIDAS";
            _logger.Info(permissions);

            int index = permissions.IndexOf(value);

            if (index != -1)
            {
                dto.ConsultarSalidas = true;
                _logger.Info("ALMACENISTA");
            }
            else
            {
                dto.ConsultarSalidas = false;
                _logger.Info("OPERARIO");
            }

            foreach (KeyValuePair<string, string> element in queryString)
            {
                switch (element.Key)
                {
                    case "StartDate":
                        dto.StartDate = CommonsTools.StringToDateTime(element.Value);
                        break;
                    case "EndDate":
                        dto.EndDate = CommonsTools.StringToDateTime(element.Value);
                        break;
                    case "IdRecibidor":
                        dto.IdRecibidor = Int32.Parse(element.Value);
                        break;
                    case "IdSolicitador":
                        dto.IdSolicitador = Int32.Parse(element.Value);
                        break;
                    case "UserId":
                        dto.UserId = Int32.Parse(element.Value);
                        break;
                    default:
                        break;
                }
            }
            return _salidaService.Query(dto);
        }
    }
}
