using FrigorificosBle.Security.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using Newtonsoft.Json;
using System.Collections;

namespace FrigorificosBle.Security.Infrastructure
{
    public class PermissionsFromClaims
    {
        public static IEnumerable<Claim> CreateRolesBasedOnClaims(List<ApplicationRole> roles)
        {
            List<Claim> claims = new List<Claim>();
            IDictionary<int, String> permissions = new Dictionary<int, String>();

            foreach (var role in roles)
            {
                foreach (var rolePermission in role.Permissions)
                {
                    if (!permissions.ContainsKey(rolePermission.Permission.Id))
                    {
                        permissions.Add(rolePermission.Permission.Id, rolePermission.Permission.Name);
                    }
                }
            }
            var json = JsonConvert.SerializeObject(permissions.Values);

            claims.Add(new Claim("permissions", json));
                    
            return claims;
        }
    }
}
