using FrigorificosBle.Almacen.Core.Domain;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
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

        public virtual long CrearNumeroOrden(string tipoOrden)
        {
            SqlParameter[] sqlParams = new SqlParameter[1];
            sqlParams[0] = new SqlParameter("@tipoOrden", tipoOrden);

            var result = this.Database.SqlQuery<Nullable<long>>("exec [dbo].sp_CrearNumeroOrden @tipoOrden", sqlParams).Single();

           return (long)result;

        }
    }
}
