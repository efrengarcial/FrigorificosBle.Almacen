using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrigorificosBle.Almacen.Core.Domain.Dto
{
    public class OrdenQueryDto
    {
        public Nullable<Int64> Numero { get; set; }
        public long Id { get; set; }
        public Nullable<Int32> IdProveedor { get; set; }
        public Nullable<DateTime> StartDate { get; set; }
        public Nullable<DateTime> EndDate { get; set; }
        public Nullable<Int32> UserId { get; set; }

    }
}
