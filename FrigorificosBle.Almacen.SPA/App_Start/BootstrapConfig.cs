using FrigorificosBle.Almacen.Core.Dao;
using FrigorificosBle.Almacen.Core.Domain;
using FrigorificosBle.Almacen.Core.Service;
using log4net;
using SimpleInjector;
using SimpleInjector.Integration.Web;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Reflection;
using System.Web;
using SimpleInjector.Extensions;
using System.Web.Http;
using System.Web.Mvc;
using SimpleInjector.Integration.Web.Mvc;
using SimpleInjector.Integration.WebApi;

namespace FrigorificosBle.Almacen.SPA
{
    //http://codeinthedesert.azurewebsites.net/using-simple-injector-with-asp-net-mvc-and-web-api/
    public static class BootstrapConfig
    {
        public static void Register()
        {
            var container = new Container();

            //http://stackoverflow.com/questions/20594391/how-to-use-simple-injector-repository-and-context-code-first
            // which is a shortcut for:
            container.RegisterPerWebRequest<DbContext, AlmacenDbContext>();

            // 2. Configure the container (register)
            //container.Register<, SqlUserRepository>();
            container.RegisterOpenGeneric(typeof(IRepository<>), typeof(EfRepository<>),
                new WebRequestLifestyle());
            container.RegisterWebApiRequest<IProductoService, ProductoService>();
            container.RegisterWebApiRequest<IProveedorService, ProveedorService>();
            container.RegisterWebApiRequest<IOrdenService, OrdenService>();
            container.RegisterWebApiRequest<ISalidaService, SalidaService>();

            var logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
            container.RegisterSingle(logger);

            //RegisterApiControllers(container);
            // This is an extension method from the integration package.
            container.RegisterWebApiControllers(GlobalConfiguration.Configuration);


            // 3. Optionally verify the container's configuration.
            container.Verify();

            //Required to set resolvers differently for WebApi and MVC
            //DependencyResolver.SetResolver(new SimpleInjectorDependencyResolver(container));
            GlobalConfiguration.Configuration.DependencyResolver = new SimpleInjectorWebApiDependencyResolver(container);
        }
       
    }
}