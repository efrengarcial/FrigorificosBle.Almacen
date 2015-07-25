using FrigorificosBle.Security.Domain.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrigorificosBle.Security.Configurations
{
    public sealed class RolePermissionConfiguration : EntityTypeConfiguration<ApplicationRolePermission>
    {
        public RolePermissionConfiguration()
        {
            HasKey(r => new { r.RoleId, r.PermissionId });

            ToTable("IdentityRolePermissions");

            //relationship  
            HasRequired(t => t.Role).WithMany(c => c.Permissions).HasForeignKey
                    (t => t.RoleId).WillCascadeOnDelete(false);

            //relationship  
            HasRequired(t => t.Permission).WithMany(c => c.Roles).HasForeignKey
                    (t => t.PermissionId).WillCascadeOnDelete(false); 
         }
    }   
        
}
