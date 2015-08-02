using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrigorificosBle.Almacen.Core.Domain.Enum
{
    public enum Permissions
    {
        AdminProductos,
        AdminProveedores,
        Entradas,
        Salidas,
        RequisicionesProcesar,
        OrdenCompra,
        Requisicion,
        OrdenServicio,
        RequisicionServicio,
        ConsultarOrdenes

    }

    public static class PermissionsExtender
    {

        public static String AsText(this Permissions permission)
        {
            switch (permission)
            {
                case Permissions.AdminProductos: return "ADMIN_PRODUCTOS";
                case Permissions.AdminProveedores: return "ADMIN_PROVEEDORES";
                case Permissions.Entradas: return "ENTRADAS";
                case Permissions.Salidas: return "SALIDAS";
                case Permissions.RequisicionesProcesar: return "REQUISICIONES_POR_PROCESAR";
                case Permissions.OrdenCompra: return "ORDEN_COMPRA";
                case Permissions.Requisicion: return "REQUISICION";
                case Permissions.OrdenServicio: return "ORDEN_SERVICIO";
                case Permissions.RequisicionServicio: return "REQUISICION_SERVICIO";
                case Permissions.ConsultarOrdenes: return "CONSULTAR_ORDENES";

            }
            return String.Empty;
        }

    }
}
