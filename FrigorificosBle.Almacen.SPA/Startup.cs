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
using Microsoft.Owin.Security.OAuth;
using Owin;
using System.Web.Http;
using SimpleInjector.Integration.WebApi;
using FrigorificosBle.Almacen.SPA.Filters;
using FrigorificosBle.Security.Dao;
using FrigorificosBle.Security.Infrastructure;
using Microsoft.Owin;
using System.Configuration;
using Microsoft.Owin.Security.DataHandler.Encoder;
using Microsoft.Owin.Security.Jwt;
using Microsoft.Owin.Security;
using System.Net.Http.Formatting;
using System.Web.Mvc;
using SimpleInjector.Integration.Web.Mvc;
using System.Web.Http.Dispatcher;
using System.Web.Routing;



//[assembly: OwinStartup(typeof(FrigorificosBle.Almacen.SPA.Startup))]
namespace FrigorificosBle.Almacen.SPA
{
    public class Startup
    {

        public static OAuthAuthorizationServerOptions OAuthOptions { get; private set; }

        public static string PublicClientId { get; private set; }

        public void Configuration(IAppBuilder app)
        {
            HttpConfiguration httpConfig = new HttpConfiguration();

            ConfigureStoreAuthentication(app);
            ConfigureOAuthTokenConsumption(app);

            //AreaRegistration.RegisterAllAreas();
            //WebApiConfig.Register(GlobalConfiguration.Configuration);
            //http://stackoverflow.com/questions/19969228/ensure-that-httpconfiguration-ensureinitialized
            //http://www.asp.net/web-api/overview/web-api-routing-and-actions/attribute-routing-in-web-api-2

            GlobalConfiguration.Configure(WebApiConfig.Register);
            //FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            //RouteConfig.RegisterRoutes(RouteTable.Routes);
            LoggerConfig.Setup();

            BootstrapConfig.Register();

            app.UseWebApi(httpConfig);
        }
      
        private void ConfigureStoreAuthentication(IAppBuilder app)
        {
            // User a single instance of StoreContext and UserManager per request
            app.CreatePerOwinContext(SecurityDbContext.Create);
            app.CreatePerOwinContext<ApplicationUserManager>(ApplicationUserManager.Create);
            app.CreatePerOwinContext<ApplicationRoleManager>(ApplicationRoleManager.Create);
            app.UseCors(Microsoft.Owin.Cors.CorsOptions.AllowAll);

            // Configure the application for OAuth based flow
            PublicClientId = "self";
            OAuthOptions = new OAuthAuthorizationServerOptions
            {
                TokenEndpointPath = new PathString("/token"),
                Provider = new ApplicationOAuthProvider(PublicClientId),
                AccessTokenExpireTimeSpan = TimeSpan.FromDays(1),
                AllowInsecureHttp = true,
                AccessTokenFormat = new CustomJwtFormat(ConfigurationManager.AppSettings["as:Issuer"])
            };

            // Enable the application to use bearer tokens to authenticate users
            app.UseOAuthBearerTokens(OAuthOptions);
        }

        private void ConfigureOAuthTokenConsumption(IAppBuilder app)
        {

            var issuer = ConfigurationManager.AppSettings["as:Issuer"];
            string audienceId = ConfigurationManager.AppSettings["as:AudienceId"];
            byte[] audienceSecret = TextEncodings.Base64Url.Decode(ConfigurationManager.AppSettings["as:AudienceSecret"]);

            // Api controllers with an [Authorize] attribute will be validated with JWT
            app.UseJwtBearerAuthentication(
                new JwtBearerAuthenticationOptions
                {
                    AuthenticationMode = AuthenticationMode.Active,
                    AllowedAudiences = new[] { audienceId },
                    IssuerSecurityTokenProviders = new IIssuerSecurityTokenProvider[]
                    {
                        new SymmetricKeyIssuerSecurityTokenProvider(issuer, audienceSecret)
                    }
                });
        }

    }
          
}