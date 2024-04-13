using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace VentaXYZ.DTO;

public partial class LoginDTO
{
    [Required(ErrorMessage = "Ingrese Usuario")]
    public string CodUsuario { get; set; } = null!;

    [Required(ErrorMessage = "Ingrese Clave")]
    public string? Clave { get; set; }
}
