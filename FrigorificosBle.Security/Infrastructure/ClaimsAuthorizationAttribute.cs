﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using System.Net.Http;
using Newtonsoft.Json;

namespace FrigorificosBle.Security.Infrastructure
{
    public class ClaimsAuthorizationAttribute : AuthorizationFilterAttribute
    {
        public string Permission { get; set; }

        public override Task OnAuthorizationAsync(HttpActionContext actionContext, System.Threading.CancellationToken cancellationToken)
        {

            var principal = actionContext.RequestContext.Principal as ClaimsPrincipal;

            if (!principal.Identity.IsAuthenticated)
            {
                actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Unauthorized);
                return Task.FromResult<object>(null);
            }

            Claim  permission = principal.Claims.Single(c => c.Type.Equals("permissions"));
            string permissionsString = permission.Value ;

            var permissions =JsonConvert.DeserializeObject<List<String>>(permissionsString );

            string[] permissionsAEvaluar = Permission.Split(',');
            bool tienePermisos = false;
            foreach (string persmissionAEvaluar in permissionsAEvaluar)
            {
                if (permissions.Contains(persmissionAEvaluar))
                {
                    tienePermisos = true;
                    break;
                }
            }

            if (!tienePermisos)
            {
                  actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Unauthorized);
                return Task.FromResult<object>(null);
            }

            //User is Authorized, complete execution
            return Task.FromResult<object>(null);

        }
    }
}
