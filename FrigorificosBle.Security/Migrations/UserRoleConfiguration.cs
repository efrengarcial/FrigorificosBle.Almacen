using FrigorificosBle.Security.Domain.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrigorificosBle.Security.Configurations
{
    public sealed class UserRoleConfiguration : EntityTypeConfiguration<ApplicationUserRole>
    {
        public UserRoleConfiguration()
        {
            HasKey(r => new { r.RoleId, r.UserId });

            ToTable("IdentityUserRoles");

            //relationship  
            HasRequired(t => t.Role).WithMany(c => c.Users).HasForeignKey
                    (t => t.RoleId).WillCascadeOnDelete(false);

            //relationship  
            HasRequired(t => t.User).WithMany(c => c.Roles).HasForeignKey
                    (t => t.UserId).WillCascadeOnDelete(false); 

        }
    }   
        
}
