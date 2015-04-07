using FrigorificosBle.Almacen.Core.Dao.Configurations;
using FrigorificosBle.Almacen.Core.Domain;
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
    public class AlmacenDbContext : AlmacenContext
    {
        public AlmacenDbContext()
            : base()
        {
            this.Database.Log = s => System.Diagnostics.Debug.WriteLine(s);
        }
    }
}
