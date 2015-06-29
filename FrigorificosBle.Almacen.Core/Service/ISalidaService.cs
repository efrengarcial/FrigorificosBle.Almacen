using FrigorificosBle.Almacen.Core.Domain;
using FrigorificosBle.Almacen.Core.Domain.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace FrigorificosBle.Almacen.Core.Service
{
    public interface ISalidaService
    {
        void Save(Salida salida);
        IList<CentroCosto> GetCentroCostos();
        IEnumerable<CentroCosto> GetByName(CentroCostoQueryDto dto);
    }
}
