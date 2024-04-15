using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.Internal;
using Microsoft.Identity.Client;
using System.Linq;
using VentaXYZ.DTO;
using VentaXYZ.Modelo;
using VentaXYZ.Repositorio.Contratos;

namespace VentaXYZ.Repositorio.Implementacion
{
    public class UsuarioRepositorio: Generico<Usuario>, IUsuarioRepositorio
    {
        private readonly DbventaXyzContext _context; //Referencia a la BD 
        public UsuarioRepositorio(DbventaXyzContext context) : base(context)
        {
            _context = context;
        }

        public async Task<List<PermisoDTO>> verificarPermiso(int? Rol, string Controlador, int opt,string esta)
        {
            //Verificando permisos de usuario
            bool rpta = false;
            List<PermisoDTO> _permisoDTO = new List<PermisoDTO>();

            try
            {
                var codRolParam = Rol; //Rol de usuario a consultar
                var codControladorParam = Controlador; //Controldor al que se necesita acceder
                var tipoParam = opt; //Opcion a realizar Eliminar/leer/actualizar/registrar

                var query =  from op in _context.OpcionRols
                            join o in _context.Opcions on op.CodOpcion equals o.CodOpcion
                            join r in _context.Rols on op.CodRol equals r.CodRol
                            where op.CodRol == codRolParam
                               && o.CodControlador == codControladorParam
                               && op.Tipo == tipoParam
                            select new PermisoDTO
                            {
                               CodRol= op.CodRol,
                               Controlador= o.CodControlador,
                               Tipo=  op.Tipo,
                               optEstado= op.optEstado //Capturando estados de pedido
                            };

                var result = await query.ToListAsync();

                if (result!=null && result.Count>0) //Verificando Permisos
                {
                    _permisoDTO = result;
                }

            }
            catch (Exception)
            {

                throw;
            }
            return _permisoDTO;
        }
        

    }
}
