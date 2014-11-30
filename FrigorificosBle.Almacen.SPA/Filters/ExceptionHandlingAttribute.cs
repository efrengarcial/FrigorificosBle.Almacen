using log4net;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Http.Filters;

namespace FrigorificosBle.Almacen.SPA.Filters
{
    public class ExceptionHandlingAttribute : ExceptionFilterAttribute
    {
        protected static readonly log4net.ILog _logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);  

        public override void OnException(HttpActionExecutedContext context)
        {
            if (context.Exception is EntityException)
            {
                //Log Critical errors
                _logger.Error(context.Exception);
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.InternalServerError)
                {
                    //Content = new StringContent(context.Exception.Message),
                    ReasonPhrase = context.Exception.InnerException.Message
                });
            }
            else
            {

                //Log Critical errors
                _logger.Error(context.Exception);

                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.InternalServerError)
                {
                    Content = new StringContent("An error occurred, please try again or contact the administrator."),
                    ReasonPhrase = "Critical Exception"
                });
            }
        }
    }
}