using System;
using System.Collections.Generic;

namespace VentaXYZ.Modelo;

public partial class DetallePedido
{
    public int CodDetalle { get; set; }

    public int? CodPedido { get; set; }

    public string? CodSku { get; set; }

    public int? Item { get; set; }

    public decimal? Cantidad { get; set; }

    public DateTime? FechaRegistro { get; set; }

    public bool? MarcaBorrado { get; set; }

    public virtual Pedido? CodPedidoNavigation { get; set; }

    public virtual Producto? CodSkuNavigation { get; set; }
}
