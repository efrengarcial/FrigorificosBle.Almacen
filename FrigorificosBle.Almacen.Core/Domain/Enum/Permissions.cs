using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrigorificosBle.Almacen.Core.Domain.Enum
{
    public enum Permissions
    {
        [StringValue("ADMIN_PRODUCTOS")]
        AdminProductos,
        [StringValue("ADMIN_PROVEEDORES")]
        AdminProveedores,
        [StringValue("ENTRADAS")]
        Entradas,
        [StringValue("SALIDAS")]
        Salidas,
        [StringValue("REQUISICIONES_POR_PROCESAR")]
        RequisicionesProcesar,
        [StringValue("ORDEN_COMPRA")]
        OrdenCompra,
        [StringValue("REQUISICION")]
        Requisicion,
        [StringValue("ORDEN_SERVICIO")]
        OrdenServicio,
        [StringValue("REQUISICION_SERVICIO")]
        RequisicionServicio,
        [StringValue("CONSULTAR_ORDENES")]
        ConsultarOrdenes

    }
}
