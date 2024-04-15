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
        private readonly string _Controllador;
        public PedidoController(IPedidoServicio pedidoServicio)
        {
            _pedidoServicio = pedidoServicio;
            _Controllador = "PEDIDO"; //Se utiliza para validar las opciones del usuario vs el controlador
        }

        [HttpPost("Registrar")]
        public async Task<IActionResult> Registrar([FromBody] PedidoDTO modelo)
        {
            //Controlador para registrar un nuevo Pedido
            var response = new ResponseDTO<PedidoDTO>(); //Inicializando respuesta del servicio
            try
            {
                response.status = true;
                response.value = await _pedidoServicio.Registrar(modelo, _Controllador); //Llamada a la clase implementadora
                response.msg = "ok";
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
            //Controlador para Actualoizar los Pedido
            var response = new ResponseDTO<bool>(); //Inicializando respuesta del servicio

            try
            {
                response = await _pedidoServicio.actualizar(modelo, _Controllador); //Llamada a la clase implementadora
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
