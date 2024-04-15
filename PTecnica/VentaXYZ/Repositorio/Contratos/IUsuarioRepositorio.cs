using VentaXYZ.DTO;
using VentaXYZ.Modelo;

namespace VentaXYZ.Repositorio.Contratos
{
    public interface IUsuarioRepositorio : IGenericos<Usuario>
    {
        Task<List<PermisoDTO>> verificarPermiso(int? Rol, string Controlador, int opt, string esta);
    }
}
