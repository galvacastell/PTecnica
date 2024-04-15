using System.Linq.Expressions;
using VentaXYZ.Modelo;

namespace VentaXYZ.Repositorio.Contratos
{
    public interface IGenericos<T> where T : class
    {
        IQueryable<T> Consultar(Expression<Func<T,bool>> ? filtro = null); //Consultar Modelos 
        Task<T> Crear(T modelo); //Insertar modelos
        Task<bool> Editar(T modelo); //Editar modelos
        Task<bool> Eliminar(T modelo); //Eliminar modelos
    }
}
