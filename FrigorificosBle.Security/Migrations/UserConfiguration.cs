using FrigorificosBle.Security.Domain.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrigorificosBle.Security.Configurations
{
    public sealed class UserConfiguration : EntityTypeConfiguration<ApplicationUser>
    {
        public UserConfiguration()
        {
            HasKey<int>(t => t.Id);
            Property(t => t.UserName)
                .HasMaxLength(100)
                .HasColumnType("nvarchar")
                .IsRequired()
                .HasUniqueIndexAnnotation("UX_IdentityUsers_UserName", 0);  

            ToTable("IdentityUsers");

        }
    }  
}
