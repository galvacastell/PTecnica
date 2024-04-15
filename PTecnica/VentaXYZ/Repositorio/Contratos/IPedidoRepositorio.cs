using System.Linq.Expressions;
using VentaXYZ.Modelo;

namespace VentaXYZ.Repositorio.Contratos
{
    public interface IPedidoRepositorio : IGenericos<Pedido>
    {
        Task<Pedido> Registrar(Pedido modelo); //Registrar Pedido

    }
}
