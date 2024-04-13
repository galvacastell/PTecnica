using System;
using System.Collections.Generic;

namespace VentaXYZ.Modelo;

public partial class Rol
{
    public int CodRol { get; set; }

    public string? Descripcion { get; set; }

    public bool? MarcaBorrado { get; set; }

    public DateTime? FechaRegistro { get; set; }

    public virtual ICollection<OpcionRol> OpcionRols { get; set; } = new List<OpcionRol>();

    public virtual ICollection<Usuario> Usuarios { get; set; } = new List<Usuario>();
}
