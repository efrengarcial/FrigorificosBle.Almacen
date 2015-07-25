namespace FrigorificosBle.Security.Migrations
{
    using FrigorificosBle.Security.Domain.Model;
    using FrigorificosBle.Security.Infrastructure;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    public sealed class Configuration : DbMigrationsConfiguration<FrigorificosBle.Security.Dao.SecurityDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(FrigorificosBle.Security.Dao.SecurityDbContext context)
        {
            var manager = new ApplicationUserManager(new ApplicationUserStore(context));

            var user = new ApplicationUser()
            {
                UserName = "efren.gl@gmail.com",
                Email = "efren.gl@gmail.com",
                EmailConfirmed = true,
                FirstName = "Efren",
                LastName = "Garcia",
                IdentificationNumber = 13719643,
                JoinDate = DateTime.Now.AddYears(-3)
            };

            

            var roleManager = new ApplicationRoleManager(new ApplicationRoleStore(context));

            if (roleManager.Roles.Count() == 0)
            {
                roleManager.Create(new ApplicationRole { Name = "SuperAdmin" });
                roleManager.Create(new ApplicationRole { Name = "Admin", Application = "FrigorificosBle.Almacen" });
                roleManager.Create(new ApplicationRole { Name = "Almacenista", Application = "FrigorificosBle.Almacen" });
                roleManager.Create(new ApplicationRole { Name = "Operario", Application = "FrigorificosBle.Almacen" });
            }          

            var user1 = new ApplicationUser()
            {
                UserName = "dariodeath@gmail.com",
                Email = "dariodeath@gmail.com",
                EmailConfirmed = true,
                FirstName = "German",
                LastName = "Garcia",
                IdentificationNumber = 225666363,
                JoinDate = DateTime.Now.AddYears(-3)
            };          

            var user2 = new ApplicationUser()
            {
                UserName = "monica.garcia@gmail.com",
                Email = "dariodeath@gmail.com",
                EmailConfirmed = true,
                FirstName = "Monica",
                LastName = "Garcia",
                IdentificationNumber = 455552255,
                JoinDate = DateTime.Now.AddYears(-3)
            };

            var identity = manager.Create(user, "Passw0rd");
            var identity1 = manager.Create(user1, "Passw0rd");
            var identity2 = manager.Create(user2, "Passw0rd");


            var adminUser = manager.FindByName("efren.gl@gmail.com");
            var adminUser1 = manager.FindByName("dariodeath@gmail.com");
            var adminUser2 = manager.FindByName("monica.garcia@gmail.com");

            manager.AddToRoles(adminUser.Id, new string[] { "Almacenista" });
            manager.AddToRoles(adminUser1.Id, new string[] { "Operario" });
            manager.AddToRoles(adminUser2.Id, new string[] { "Operario" });

            if (context.Permissions.Count() == 0)
            {
                var permission=context.Permissions.Add(new ApplicationPermission() { Name = "Consutar Ordenes" });
                var role = roleManager.Roles.Single(r => r.Name.Equals("Almacenista"));

                context.RolePermissions.Add(new ApplicationRolePermission() { Role = role, Permission = permission });
                
                context.SaveChanges();
            }
        }
    }
}
