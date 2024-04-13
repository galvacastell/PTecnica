using System;
using System.Collections.Generic;

namespace VentaXYZ.Modelo;

public partial class OpcionRol
{
    public int CodOpcionRol { get; set; }

    public int? CodOpcion { get; set; }

    public int? CodRol { get; set; }

    public int? Tipo { get; set; }

    public bool? MarcaBorrado { get; set; }

    public DateTime? FechaRegistro { get; set; }

    public virtual Opcion? CodOpcionNavigation { get; set; }

    public virtual Rol? CodRolNavigation { get; set; }
}
