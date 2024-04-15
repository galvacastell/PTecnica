using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Security.AccessControl;
using System.Security.Cryptography;
using System.Security.Cryptography.Xml;
using VentaXYZ.DTO;
using VentaXYZ.Modelo;
using VentaXYZ.Repositorio.Contratos;
using VentaXYZ.Servicio.Implementacion;

namespace VentaXYZ.Servicio.Contrato
{
    public class UsuarioServicio : IUsuarioServicio
    {
        private readonly IGenericos<Usuario> _modeloRepositorio;
        private readonly IMapper _mapper;
        private readonly IUsuarioRepositorio _modeloUsuario;

        public UsuarioServicio(IGenericos<Usuario> modeloRepositorio, IMapper mapper, IUsuarioRepositorio modeloUsuario)
        {
            _modeloRepositorio = modeloRepositorio;
            _modeloUsuario = modeloUsuario;
            _mapper = mapper;
        }

        public async Task<PermisoDTO> Autorizar(LoginDTO modelo)
        {
            try
            {
                var consulta = _modeloRepositorio.Consultar(q => q.CodUsuario == modelo.CodUsuario && q.Clave == modelo.Clave);
                var fromDBModelo = await consulta.FirstOrDefaultAsync();

                if (fromDBModelo != null)
                {
                    return _mapper.Map<PermisoDTO>(fromDBModelo);
                }
                else
                    throw new TaskCanceledException("No se encontraron coincidencias");

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<List<PermisoDTO>> verificarPermiso(int? Rol, string Controlador, int opt, string esta)
        {
            List<PermisoDTO> rspa = new List<PermisoDTO>();
            try
            {
                rspa = await _modeloUsuario.verificarPermiso(Rol, Controlador, opt, esta);
            }
            catch (Exception)
            {
                throw;
            }
            return rspa;
        }

        public async Task<bool> AutorizarToken(LoginDTO modelo)
        {
            try
            {
                var consulta = _modeloRepositorio.Consultar(q => q.CodUsuario == modelo.CodUsuario && q.Clave == modelo.Clave);
                var fromDBModelo = await consulta.FirstOrDefaultAsync();

                if (fromDBModelo != null)
                {
                    return true;
                }
                else
                    return false;

            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<UsuarioDTO> crear(UsuarioDTO modelo)
        {

            try
            {
                var dbModelo = _mapper.Map<Usuario>(modelo);
                var rspModelo = await _modeloRepositorio.Crear(dbModelo);

                if (rspModelo.CodUsuario.Length > 0)
                    return _mapper.Map<UsuarioDTO>(rspModelo);
                else
                    throw new TaskCanceledException("No se puede crear");

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<bool> Editar(UsuarioDTO modelo)
        {
            try
            {
                var consulta = _modeloRepositorio.Consultar(q => q.CodUsuario == modelo.CodUsuario);
                var fromDBModelo = await consulta.FirstOrDefaultAsync();
                if (fromDBModelo != null)
                {
                    fromDBModelo.Nombre = modelo.Nombre;
                    fromDBModelo.Correo = modelo.Correo;
                    fromDBModelo.Clave = modelo.Clave;

                    var respuesta = await _modeloRepositorio.Editar(fromDBModelo);
                    if (!respuesta)
                        throw new TaskCanceledException("No se pudo editar");

                    return respuesta;

                }
                else
                    throw new TaskCanceledException("No se encontro el usuario");

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<bool> Eliminar(string id)
        {
            try
            {
                var consulta = _modeloRepositorio.Consultar(q => q.CodUsuario == id);
                var fromDBModelo = await consulta.FirstOrDefaultAsync();
                if (fromDBModelo != null)
                {
                    var respuesta = await _modeloRepositorio.Eliminar(fromDBModelo);
                    if (!respuesta)
                        throw new TaskCanceledException("No se pudo Eliminar Usuario");

                    return respuesta;
                }
                else
                    throw new TaskCanceledException("No se encontro el usuario");

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<UsuarioDTO>> Listar(int rol, string buscar)
        {
            try
            {
                var consulta = _modeloRepositorio.Consultar(q => q.CodRol == rol
                && string.Concat(q.Nombre.ToLower(), q.Correo.ToLower()).Contains(buscar.ToLower()));

                List<UsuarioDTO> lista = _mapper.Map<List<UsuarioDTO>>(await consulta.ToListAsync());
                return lista;

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<UsuarioDTO> buscar(string id)
        {
            try
            {
                var consulta = _modeloRepositorio.Consultar(q => q.CodUsuario == id);
                var fromDbModelo = await consulta.FirstOrDefaultAsync();
                if (fromDbModelo != null)
                {
                    return _mapper.Map<UsuarioDTO>(fromDbModelo);
                }
                else
                    throw new TaskCanceledException("No se encontro el usuario");

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

    }
}
