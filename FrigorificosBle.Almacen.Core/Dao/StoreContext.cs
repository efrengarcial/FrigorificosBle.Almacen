using FrigorificosBle.Almacen.Core.Dao.Configurations;
using FrigorificosBle.Almacen.Core.Domain.Model;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace FrigorificosBle.Almacen.Core.Dao
{
    public class StoreContext : IdentityDbContext<AppStoreUser>
    {
        public StoreContext()
            : base("StoreContext", throwIfV1Schema: false)
        {
            this.Database.Log = s => System.Diagnostics.Debug.WriteLine(s);
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<IdentityUserLogin>().HasKey<string>(l => l.UserId);
            modelBuilder.Entity<IdentityRole>().HasKey<string>(r => r.Id);
            modelBuilder.Entity<IdentityUserRole>().HasKey(r => new { r.RoleId, r.UserId });

            modelBuilder.Configurations.Add(new AppStoreUserConfiguration());
        }
       
        public static StoreContext Create()
        {
            return new StoreContext();
        }
    }
}
