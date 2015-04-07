using FrigorificosBle.Almacen.Core.Domain.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrigorificosBle.Almacen.Core.Dao.Configurations
{
    public class AppStoreUserConfiguration : EntityTypeConfiguration<AppStoreUser>
    {
        public AppStoreUserConfiguration()
        {
            ToTable("Users");
        }
    }
}
