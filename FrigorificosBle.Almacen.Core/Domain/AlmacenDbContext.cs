using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrigorificosBle.Almacen.Core.Domain
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
