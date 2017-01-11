using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrigorificosBle.Almacen.Core.Domain.Dto
{
    public class SalidaQueryDto
    {

        public long Id { get; set; }
        public Nullable<Int32> IdRecibidor { get; set; }
        public Nullable<Int32> IdSolicitador { get; set; }
        public Nullable<DateTime> StartDate { get; set; }
        public Nullable<DateTime> EndDate { get; set; }
        public Nullable<Int32> UserId { get; set; }
        public Boolean ConsultarSalidas = false;

    }
}
