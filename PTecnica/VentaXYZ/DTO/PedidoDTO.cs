using System;
using System.Collections.Generic;
using VentaXYZ.Modelo;

namespace VentaXYZ.DTO;

public partial class PedidoDTO
{
    public int CodPedido { get; set; }

    public string? CodUsuario { get; set; }

    public int? Estado { get; set; }

    public virtual ICollection<DetallePedidoDTO> DetallePedidos { get; set; } = new List<DetallePedidoDTO>();
}
