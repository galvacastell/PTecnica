using System;
using System.Collections.Generic;

namespace VentaXYZ.Modelo;

public partial class Usuario
{
    public string CodUsuario { get; set; } = null!;

    public string? Nombre { get; set; }

    public string? Correo { get; set; }

    public string? Telefono { get; set; }

    public string? Puesto { get; set; }

    public int? CodRol { get; set; }

    public string? Clave { get; set; }

    public bool? MarcaBorrado { get; set; }

    public DateTime? FechaRegistro { get; set; }

    public virtual Rol? CodRolNavigation { get; set; }

    public virtual ICollection<Pedido> PedidoCodRepartidorNavigations { get; set; } = new List<Pedido>();

    public virtual ICollection<Pedido> PedidoCodVendedorNavigations { get; set; } = new List<Pedido>();
}
