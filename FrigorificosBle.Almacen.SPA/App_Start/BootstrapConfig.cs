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

namespace FrigorificosBle.Almacen.SPA
{
    //http://codeinthedesert.azurewebsites.net/using-simple-injector-with-asp-net-mvc-and-web-api/
    public static class BootstrapConfig
    {
        public static void Register(Container container)
        {
            //http://stackoverflow.com/questions/20594391/how-to-use-simple-injector-repository-and-context-code-first
            // which is a shortcut for:
            container.RegisterPerWebRequest<DbContext, AlmacenContext>();

            // 2. Configure the container (register)
            //container.Register<, SqlUserRepository>();
            container.RegisterOpenGeneric(typeof(IRepository<>), typeof(EfRepository<>),
                new WebRequestLifestyle());
            container.Register<IProductoService, ProductoService>();
            container.Register<IProveedorService, ProveedorService>();

            var logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
            container.RegisterSingle(logger);

            // 3. Optionally verify the container's configuration.
            container.Verify();
        }
    }
}