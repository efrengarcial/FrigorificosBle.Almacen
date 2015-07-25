using FrigorificosBle.Security.Configurations;
using FrigorificosBle.Security.Domain.Model;
using FrigorificosBle.Security.Migrations;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace FrigorificosBle.Security.Dao
{
    public class SecurityDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, int,
        ApplicationUserLogin, ApplicationUserRole, ApplicationUserClaim>
    {
        public SecurityDbContext()
            : base("SecurityDbContext")
        {
            this.Database.Log = s => System.Diagnostics.Debug.WriteLine(s);
            Configuration.ProxyCreationEnabled = false;
            Configuration.LazyLoadingEnabled = false;

            Database.SetInitializer(
             new MigrateDatabaseToLatestVersion<SecurityDbContext, Configuration>());
        }

        public DbSet<ApplicationPermission> Permissions { get; set; }
        public DbSet<ApplicationRolePermission> RolePermissions { get; set; }
        

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
           modelBuilder.Configurations.Add(new UserConfiguration());
           modelBuilder.Configurations.Add(new RoleConfiguration());
           modelBuilder.Configurations.Add(new UserRoleConfiguration());
           modelBuilder.Configurations.Add(new UserLoginConfiguration());
           modelBuilder.Configurations.Add(new UserClaimConfiguration());
           modelBuilder.Configurations.Add(new PermissionConfiguration());
           modelBuilder.Configurations.Add(new RolePermissionConfiguration());
        }
       
        public static SecurityDbContext Create()
        {
            return new SecurityDbContext();
        }
    }
}
