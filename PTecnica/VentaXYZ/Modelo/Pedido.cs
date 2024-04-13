using System;
using System.Collections.Generic;

namespace VentaXYZ.Modelo;

public partial class Pedido
{
    public int CodPedido { get; set; }

    public DateTime? FechaPedido { get; set; }

    public DateTime? FechaRecepcion { get; set; }

    public DateTime? FechaDespacho { get; set; }

    public DateTime? FechaEntrega { get; set; }

    public string? CodVendedor { get; set; }

    public string? CodRepartidor { get; set; }

    public int? Estado { get; set; }

    public bool? MarcaBorrado { get; set; }

    public virtual Usuario? CodRepartidorNavigation { get; set; }

    public virtual Usuario? CodVendedorNavigation { get; set; }

    public virtual ICollection<DetallePedido> DetallePedidos { get; set; } = new List<DetallePedido>();
}
