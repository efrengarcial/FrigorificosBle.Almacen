using FrigorificosBle.Security.Domain.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrigorificosBle.Security.Configurations
{
   
    public sealed class RoleConfiguration : EntityTypeConfiguration<ApplicationRole>
    {
        public RoleConfiguration()
        {
            HasKey<int>(r => r.Id);
            Property(t => t.Name)
              .HasMaxLength(100)
              .HasColumnType("nvarchar")
              .IsRequired();

            ToTable("IdentityRoles");


            //relationship  
           // HasMany(t => t.Users).WithRequired(c => c.Role).HasForeignKey(t => t.RoleId);

        }
    }    
}
