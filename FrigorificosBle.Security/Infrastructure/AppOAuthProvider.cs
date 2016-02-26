using Microsoft.Owin.Security;
using Microsoft.Owin.Security.OAuth;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Security.Claims;
using System.Threading.Tasks;
using FrigorificosBle.Security.Domain.Model;
using System.Collections.Generic;
using FrigorificosBle.Security.Dao;
using System.Linq;
using System.Data.Entity;
using log4net;
using System.Reflection;

namespace FrigorificosBle.Security.Infrastructure
{
    public class ApplicationOAuthProvider : OAuthAuthorizationServerProvider
    {
        private readonly ILog logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        private readonly string _publicClientId;

        public ApplicationOAuthProvider(string publicClientId)
        {
            if (publicClientId == null)
            {
                throw new ArgumentNullException("publicClientId");
            }

            _publicClientId = publicClientId;
        }

        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            var userManager = context.OwinContext.GetUserManager<ApplicationUserManager>();
            var rolManager = context.OwinContext.GetUserManager<ApplicationRoleManager>();
            ApplicationUser user = null;
            try { 
                user = await userManager.FindAsync(context.UserName, context.Password);
            }
            catch (Exception ex)
            {
                logger.Error(ex);
                context.SetError("error", "Error de Sistema.");
                return;
            }
   

            if (user == null)
            {
                context.SetError("invalid_grant", "Invalid username or password.");
                return;
            }

            if (!user.EmailConfirmed)
            {
                context.SetError("invalid_grant", "User did not confirm email.");
                return;
            }
            List<int> roles = new List<int>();
            foreach (var item in user.Roles)
            {
                roles.Add(item.RoleId);
            }
            List<ApplicationRole> rolesList = rolManager.Roles.Where(r => roles.Contains(r.Id)).
                Include(r => r.Permissions.Select(p => p.Permission)).ToList();

            ClaimsIdentity oAuthIdentity = await userManager.GenerateUserIdentityAsync(user, "JWT");
            oAuthIdentity.AddClaims(PermissionsFromClaims.CreateRolesBasedOnClaims(rolesList));
            AuthenticationProperties properties = new AuthenticationProperties(); 
            AuthenticationTicket ticket = new AuthenticationTicket(oAuthIdentity, properties);
            context.Validated(ticket);
        }

        public override Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            if (context.ClientId == null)
            {
                context.Validated();
            }

            return Task.FromResult<object>(null);
        }
    }
}