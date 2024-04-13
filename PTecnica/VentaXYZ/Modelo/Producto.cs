using System;
using System.Collections.Generic;

namespace VentaXYZ.Modelo;

public partial class Producto
{
    public string CodSku { get; set; } = null!;

    public string? Nombre { get; set; }

    public string? Tipo { get; set; }

    public string? Etiqueta { get; set; }

    public decimal? Precio { get; set; }

    public string? UndMedida { get; set; }

    public decimal? Stock { get; set; }

    public DateTime? FechaRegistro { get; set; }

    public bool? MarcaBorrado { get; set; }

    public virtual ICollection<DetallePedido> DetallePedidos { get; set; } = new List<DetallePedido>();
}
