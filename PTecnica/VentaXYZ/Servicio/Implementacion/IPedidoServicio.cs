using System.Threading.Tasks;
using VentaXYZ.DTO;
using VentaXYZ.Modelo;

namespace VentaXYZ.Servicio.Implementacion
{
    public interface IPedidoServicio
    {
        public Task<PedidoDTO> Registrar(PedidoDTO modelo);
        public Task<ResponseDTO<bool>> actualizar(PedidoDTO modelo);
    }
}
