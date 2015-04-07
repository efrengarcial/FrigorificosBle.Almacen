using Microsoft.Owin;
using Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;

using SimpleInjector.Extensions;
using SimpleInjector.Integration.Web;
using SimpleInjector.Integration.WebApi;
using SimpleInjector;
using SimpleInjector.Integration.Web.Mvc;


[assembly: OwinStartup(typeof(FrigorificosBle.Almacen.SPA.Startup))]
namespace FrigorificosBle.Almacen.SPA
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureStoreAuthentication(app);
        }
    }
          
}