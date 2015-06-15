using FrigorificosBle.Security.Domain.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrigorificosBle.Security.Configurations
{
    public sealed class UserClaimConfiguration : EntityTypeConfiguration<ApplicationUserClaim>
    {
        public UserClaimConfiguration()
        {
            HasKey<int>(c => c.Id);

            ToTable("IdentityUserClaims");
        }
    }
}
