using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace FrigorificosBle.Security.Domain.Model
{
    public class ApplicationUser : IdentityUser<int, ApplicationUserLogin,
        ApplicationUserRole, ApplicationUserClaim>,  IUser<int>
    {
        // Add any custom properties you wish here
        [Required]
        [MaxLength(100)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(100)]
        public string LastName { get; set; }

        [Required]
        public long IdentificationNumber { get; set; }

        [Required]
        public DateTime JoinDate { get; set; }
      

        //Rest of code is removed for brevity
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser,int> manager, string authenticationType)
        {
            var userIdentity = await manager.CreateIdentityAsync(this, authenticationType);
            // Add custom user claims here
            return userIdentity;
        }

    }

    public class ApplicationRole : IdentityRole<int, ApplicationUserRole>, IRole<int>
    {
        [MaxLength(100)]
        public string Application { get; set; }

        [MaxLength(100)]
        public string Description { get; set; }

        public virtual ICollection<ApplicationRolePermission> Permissions { get; set; }
    }

    public class ApplicationUserRole : IdentityUserRole<int> {

        public virtual ApplicationUser User { get; set; }

        public virtual ApplicationRole Role { get; set; }  
    }
    public class ApplicationUserClaim : IdentityUserClaim<int> { }
    public class ApplicationUserLogin : IdentityUserLogin<int> { }

    public class ApplicationPermission
    {
        public int Id { get; set; }

        [MaxLength(100)]
        public string Name { get; set; }

        public virtual ICollection<ApplicationRolePermission> Roles { get; set; }
    }

    public class ApplicationRolePermission
    {
        [Required]
        public int RoleId { get; set; }

        [Required]
        public int PermissionId { get; set; }


        public virtual ApplicationPermission Permission { get; set; }

       
        public virtual ApplicationRole Role { get; set; }  
    }

    public class ApplicationUserStore
    : UserStore<ApplicationUser, ApplicationRole, int,
        ApplicationUserLogin, ApplicationUserRole,
        ApplicationUserClaim>, IUserStore<ApplicationUser, int>,
        IDisposable
    {
        public ApplicationUserStore()
            : this(new IdentityDbContext())
        {
            base.DisposeContext = true;
        }


        public ApplicationUserStore(DbContext context)
            : base(context)
        {

        }
    }


    public class ApplicationRoleStore
        : RoleStore<ApplicationRole, int, ApplicationUserRole>,
        IQueryableRoleStore<ApplicationRole, int>,
        IRoleStore<ApplicationRole, int>, IDisposable
    {
        public ApplicationRoleStore()
            : base(new IdentityDbContext())
        {
            base.DisposeContext = true;
        }


        public ApplicationRoleStore(DbContext context)
            : base(context)
        {

        }
    }

}
