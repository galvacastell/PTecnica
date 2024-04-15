using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace VentaXYZ.DTO;

public partial class PermisoDTO
{
    public string? Controlador { get; set; } 
    public int? CodRol { get; set; }
    public int? Tipo { get; set; }
    public string? optEstado { get; set; }

}
