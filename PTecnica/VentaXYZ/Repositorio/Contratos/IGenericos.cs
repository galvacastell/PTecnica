using System.Linq.Expressions;
using VentaXYZ.Modelo;

namespace VentaXYZ.Repositorio.Contratos
{
    public interface IGenericos<T> where T : class
    {
        IQueryable<T> Consultar(Expression<Func<T,bool>> ? filtro = null);
        Task<T> Crear(T modelo);
        Task<bool> Editar(T modelo);
        //Task<bool> Editar(Pedido pedid);
        Task<bool> Eliminar(T modelo);
    }
}
