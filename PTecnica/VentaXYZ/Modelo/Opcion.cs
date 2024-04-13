using System;
using System.Collections.Generic;

namespace VentaXYZ.Modelo;

public partial class Opcion
{
    public int CodOpcion { get; set; }

    public string? Descripcion { get; set; }

    public int? Padre { get; set; }

    public string? CodControlador { get; set; }

    public string? Rutapagina { get; set; }

    public bool? MarcaBorrado { get; set; }

    public DateTime? FechaRegistro { get; set; }

    public virtual ICollection<OpcionRol> OpcionRols { get; set; } = new List<OpcionRol>();
}
