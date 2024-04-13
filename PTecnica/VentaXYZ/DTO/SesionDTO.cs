using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace VentaXYZ.DTO;

public partial class SesionDTO
{
    public string CodUsuario { get; set; } = null!;
    public string? Nombre { get; set; }
    public string? Correo { get; set; }
    public int? CodRol { get; set; }
}
