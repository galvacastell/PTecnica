using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using VentaXYZ.Modelo;
using VentaXYZ.Repositorio.Contratos;

namespace VentaXYZ.Repositorio.Implementacion
{
    public class Generico<T> : IGenericos<T> where T: class //Indicando con el dato "T" es una clase
    {
        private readonly DbventaXyzContext _dbContext; //Conexion a la BD

        public Generico(DbventaXyzContext dbContext)
        {
            //Iniciando generico
            _dbContext = dbContext;
        }

        
        IQueryable<T> IGenericos<T>.Consultar(Expression<Func<T, bool>>? filtro)
        {
            //Crear consulta a la base de datos teniendo where opcional
            IQueryable<T> consulta = (filtro == null) ? _dbContext.Set<T>() : _dbContext.Set<T>().Where(filtro);

            return consulta;
        }

        async Task<T> IGenericos<T>.Crear(T modelo)
        {
            //Insertar modelos en BD
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
            //Editando modelos en BD
            try
            {
                foreach (var property in _dbContext.Entry(modelo).Properties)
                {
                    if (property.CurrentValue == null)
                    {
                        // Si la propiedad actual es nula se excluye de la actualización
                        property.IsModified = false;
                    }
                }

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
            //Eliinado modelo
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
