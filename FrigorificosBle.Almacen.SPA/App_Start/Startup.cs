using FrigorificosBle.Almacen.Core.Dao;
using FrigorificosBle.Almacen.Core.Domain;
using Microsoft.Owin;
using Microsoft.Owin.Security.OAuth;
using Owin;
using Store.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FrigorificosBle.Almacen.SPA
{
    public partial class Startup
    {
        public static OAuthAuthorizationServerOptions OAuthOptions { get; private set; }

        public static string PublicClientId { get; private set; }

        public void ConfigureStoreAuthentication(IAppBuilder app)
        {
            // User a single instance of StoreContext and UserManager per request
            app.CreatePerOwinContext(StoreContext.Create);
            app.CreatePerOwinContext<AppUserManager>(AppUserManager.Create);
            app.UseCors(Microsoft.Owin.Cors.CorsOptions.AllowAll);

            // Configure the application for OAuth based flow
            PublicClientId = "self";
            OAuthOptions = new OAuthAuthorizationServerOptions
            {
                TokenEndpointPath = new PathString("/token"),
                Provider = new ApplicationOAuthProvider(PublicClientId),
                AccessTokenExpireTimeSpan = TimeSpan.FromDays(10),
                AllowInsecureHttp = true
            };

            // Enable the application to use bearer tokens to authenticate users
            app.UseOAuthBearerTokens(OAuthOptions);
        }
    }
}