using System.Linq.Expressions;
using VentaXYZ.Modelo;
using VentaXYZ.Repositorio.Contratos;

namespace VentaXYZ.Repositorio.Implementacion
{
    public class Generico<T> : IGenericos<T> where T: class
    {
        private readonly DbventaXyzContext _dbContext;

        public Generico(DbventaXyzContext dbContext)
        {
            _dbContext = dbContext;
        }

        IQueryable<T> IGenericos<T>.Consultar(Expression<Func<T, bool>>? filtro)
        {
            IQueryable<T> consulta = (filtro == null) ? _dbContext.Set<T>() : _dbContext.Set<T>().Where(filtro);

            return consulta;
        }

        async Task<T> IGenericos<T>.Crear(T modelo)
        {
           try
           {
                _dbContext.Set<T>().Add(modelo);
                await _dbContext.SaveChangesAsync();
                return modelo;
           }
           catch 
           {
                throw;
           }

        }

         async Task<bool> IGenericos<T>.Editar(T modelo)
        {
            try
            {
                _dbContext.Set<T>().Update(modelo);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            catch
            {
                throw;
            }
        }

        async Task<bool> IGenericos<T>.Eliminar(T modelo)
        {
            try
            {
                _dbContext.Set<T>().Remove(modelo);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            catch
            {
                throw;
            }
        }
    }
}
