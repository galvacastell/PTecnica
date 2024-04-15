using System.Threading.Tasks;
using VentaXYZ.DTO;
using VentaXYZ.Modelo;

namespace VentaXYZ.Servicio.Implementacion
{
    public interface IPedidoServicio
    {
        public Task<PedidoDTO> Registrar(PedidoDTO modelo, string _Controllador); //Registrar pedido
        public Task<ResponseDTO<bool>> actualizar(PedidoDTO modelo, string _Controllador); //Actualizar pedido
    }
}
