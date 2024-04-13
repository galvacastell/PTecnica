using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VentaXYZ.DTO;
using VentaXYZ.Servicio.Contrato;
using VentaXYZ.Servicio.Implementacion;

namespace VentaXYZ.Controllers
{
    [Authorize]
    [Route("api/[Controller]")]
    [ApiController]
    public class PedidoController : Controller
    {
        private readonly IPedidoServicio _pedidoServicio;
        public PedidoController(IPedidoServicio pedidoServicio)
        {
            _pedidoServicio = pedidoServicio;
        }

        [HttpPost("Registrar")]
        public async Task<IActionResult> Registrar([FromBody] PedidoDTO modelo)
        {
            var response = new ResponseDTO<PedidoDTO>();
            try
            {
                response.status = true;
                response.value = await _pedidoServicio.Registrar(modelo);

            }
            catch (Exception e)
            {
                response.status = false;
                response.msg = e.Message;
            }
            return Ok(response);
        }

        [HttpPut("Actualizar")]
        public async Task<IActionResult> Actualizar([FromBody] PedidoDTO modelo)
        {
            var response = new ResponseDTO<bool>();
            try
            {
                response = await _pedidoServicio.actualizar(modelo);
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
