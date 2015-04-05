using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrigorificosBle.Almacen.Core.Domain.Enum
{
    public enum TipoOrdenEnum
    {
        ORDEN_COMPRA,
        ORDEN_SERVICIO,
        REQUISICION,
        REQUISICION_SERVICIO

    }

    public enum OrdenEstadoEnum {

        ABIERTA,
        EN_PROCESO,
        CERRADA    
    }

    public static class SecuenciasOrdenEnumExtender
    {
        public static String AsSecuencia(this TipoOrdenEnum tipoOrden)
        {
            switch (tipoOrden)
            {
                case TipoOrdenEnum.ORDEN_COMPRA: return "SECUENCIA_ORDEN_COMPRA";
                case TipoOrdenEnum.ORDEN_SERVICIO: return "SECUENCIA_ORDEN_SERVICIO";
                case TipoOrdenEnum.REQUISICION: return "SECUENCIA_REQUISICION";
                case TipoOrdenEnum.REQUISICION_SERVICIO: return "SECUENCIA_REQUISICION_SERVICIO";



            }
            return String.Empty;
        }

        public static String AsText(this TipoOrdenEnum tipoOrden)
        {
            switch (tipoOrden)
            {
                case TipoOrdenEnum.ORDEN_COMPRA: return "ORDEN_COMPRA";
                case TipoOrdenEnum.ORDEN_SERVICIO: return "ORDEN_SERVICIO";
                case TipoOrdenEnum.REQUISICION: return "REQUISICION";
                case TipoOrdenEnum.REQUISICION_SERVICIO: return "REQUISICION_SERVICIO";

            }
            return String.Empty;
        }
    }

    public static class OrdenEstadoEnumExtender {

        public static String AsText(this OrdenEstadoEnum OrdenEstado)
        {
            switch (OrdenEstado)
            {
                case OrdenEstadoEnum.ABIERTA: return "ABIERTA";
                case OrdenEstadoEnum.EN_PROCESO: return "EN_PROCESO";
                case OrdenEstadoEnum.CERRADA: return "CERRADA";

            }
            return String.Empty;
        }
    
    }
}
