using FrigorificosBle.Security.Domain.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrigorificosBle.Security.Configurations
{
    public sealed class PermissionConfiguration : EntityTypeConfiguration<ApplicationPermission>
    {
        public PermissionConfiguration()
        {
            HasKey<int>(t => t.Id);


            ToTable("IdentityPermissions");

        }
    }  
}
