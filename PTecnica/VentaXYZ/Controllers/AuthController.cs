using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using VentaXYZ.DTO;
using VentaXYZ.Servicio.Contrato;
using VentaXYZ.Servicio.Implementacion;

namespace VentaXYZ.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : Controller
    {
        private readonly string _secretKey;//clave secreta 
        private readonly IUsuarioServicio _usuarioServicio;

        public AuthController( IUsuarioServicio usuarioServicio, IConfiguration config)
        {
            _usuarioServicio = usuarioServicio;
            _secretKey = config.GetSection("settings").GetSection("secretKey").ToString();
        }

        [HttpPost]
        [Route("token")]
        public async Task<IActionResult> Token(LoginDTO credentials)
        {
            //Validación de usuario:
            if (! await _usuarioServicio.AutorizarToken(credentials))
            {
                return Unauthorized();
            }

             string secretKey = _secretKey; 
             var securityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(secretKey));

             var jwt = new JwtSecurityToken(
                 claims: BuildClaims(credentials),
                 expires: DateTime.Now.AddMinutes(15),
                 signingCredentials: new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256)
                 );
             var token = new JwtSecurityTokenHandler().WriteToken(jwt); //Creacion de token
            
            return Ok(token);
        }

        private Claim[] BuildClaims(LoginDTO credentials)
        {
            return new[]
            {
               new Claim("userType",credentials.CodUsuario)
           };
        }
    }
}
