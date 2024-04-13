using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace VentaXYZ.DTO;

public partial class UsuarioDTO
{
    public string CodUsuario { get; set; } = null!;

    [Required(ErrorMessage="Ingrese nombre Completo")]
    public string? Nombre { get; set; }

    public string? Correo { get; set; }

    public string? Telefono { get; set; }


    [Required(ErrorMessage = "Ingrese Clave")]
    public string? Clave { get; set; }

    
}
