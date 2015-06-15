using FrigorificosBle.Security.Dao;
using FrigorificosBle.Security.Domain.Model;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;

namespace FrigorificosBle.Security.Infrastructure
{
    public class ApplicationRoleManager : RoleManager<ApplicationRole,int>
    {
        public ApplicationRoleManager(IRoleStore<ApplicationRole, int> roleStore)
            : base(roleStore)
        {
        }

        public static ApplicationRoleManager Create(IdentityFactoryOptions<ApplicationRoleManager> options, IOwinContext context)
        {
            var appRoleManager = new ApplicationRoleManager(new ApplicationRoleStore(context.Get<SecurityDbContext>()));

            return appRoleManager;
        }
    }
}