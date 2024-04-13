using VentaXYZ.DTO;
namespace VentaXYZ.Servicio.Implementacion
{
    public interface IUsuarioServicio
    {
        Task<List<UsuarioDTO>> Listar(int rol, string buscar);

        bool AutorizarToken(LoginDTO modelo);
        Task<SesionDTO> Autorizar(LoginDTO modelo);
        Task<UsuarioDTO> buscar(string id);

        Task<UsuarioDTO> crear(UsuarioDTO modelo);
        Task<bool> Editar(UsuarioDTO modelo);
        Task<bool> Eliminar(string id);

    }
}
