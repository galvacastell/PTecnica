using Azure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VentaXYZ.DTO;
using VentaXYZ.Servicio.Implementacion;

namespace VentaXYZ.Controllers
{
    [Authorize]
    [Route("api/[Controller]")]
    [ApiController]
    public class UsuarioController : Controller
    {
        private readonly IUsuarioServicio _usuarioServicio;

        public UsuarioController(IUsuarioServicio usuarioServicio)
        {
            _usuarioServicio = usuarioServicio;
        }


        [HttpGet("Lista/{rol:int}/{buscar:alpha?}")]
        public async Task<IActionResult> Lista(int rol, string buscar="NA")
        {
            var response = new ResponseDTO<List<UsuarioDTO>>();
            try
            {
                if (buscar == "NA") buscar = "";

                response.status = true;
                response.value = await _usuarioServicio.Listar(rol, buscar);

            }
            catch (Exception e)
            {
                response.status = false;
                response.msg = e.Message;
            }
            return Ok(response);
        }

        [HttpGet("Obtener/{coduser:alpha}")]
        public async Task<IActionResult> Obtener(string coduser)
        {
            var response = new ResponseDTO<UsuarioDTO>();
            try
            {
                response.status = true;
                response.value = await _usuarioServicio.buscar(coduser);

            }
            catch (Exception e)
            {
                response.status = false;
                response.msg = e.Message;
            }
            return Ok(response);
        }

        [HttpPost("Crear")]
        public async Task<IActionResult> Crear([FromBody]UsuarioDTO modelo)
        {
            var response = new ResponseDTO<UsuarioDTO>();
            try
            {
                response.status = true;
                response.value = await _usuarioServicio.crear(modelo);

            }
            catch (Exception e)
            {
                response.status = false;
                response.msg = e.Message;
            }
            return Ok(response);
        }

        [HttpPost("Autorizacion")]
        public async Task<IActionResult> Autorizacion([FromBody] LoginDTO modelo)
        {
            var response = new ResponseDTO<PermisoDTO>();
            try
            {
                response.status = true;
                response.value = await _usuarioServicio.Autorizar(modelo);

            }
            catch (Exception e)
            {
                response.status = false;
                response.msg = e.Message;
            }
            return Ok(response);
        }

        [HttpPut("Editar")]
        public async Task<IActionResult> Editar([FromBody] UsuarioDTO modelo)
        {
            var response = new ResponseDTO<bool>();
            try
            {
                response.status = true;
                response.value = await _usuarioServicio.Editar(modelo);

            }
            catch (Exception e)
            {
                response.status = false;
                response.msg = e.Message;
            }
            return Ok(response);
        }

        [HttpDelete("Eliminar")]
        public async Task<IActionResult> Eliminar(string id)
        {
            var response = new ResponseDTO<bool>();
            try
            {
                response.status = true;
                response.value = await _usuarioServicio.Eliminar(id);

            }
            catch (Exception e)
            {
                response.status = false;
                response.msg = e.Message;
            }
            return Ok(response);
        }

    }
}
