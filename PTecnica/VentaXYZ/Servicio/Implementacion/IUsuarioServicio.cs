using VentaXYZ.DTO;
namespace VentaXYZ.Servicio.Implementacion
{
    public interface IUsuarioServicio
    {
        Task<List<UsuarioDTO>> Listar(int rol, string buscar); //Listar Usuarios

        Task<bool> AutorizarToken(LoginDTO modelo); //Autorizar token
        Task<PermisoDTO> Autorizar(LoginDTO modelo); //Autorizar cambios de estados y uso de controladores
        Task<UsuarioDTO> buscar(string id); //Buscar usuario

        Task<UsuarioDTO> crear(UsuarioDTO modelo); //Crear Usuario
        Task<bool> Editar(UsuarioDTO modelo); //Editar Usuario
        Task<bool> Eliminar(string id); //Eliminar usuario

        Task<List<PermisoDTO>> verificarPermiso(int? Rol, string Controlador, int opt, string esta); //Permitir accesos para actualiar pedidos

    }
}
