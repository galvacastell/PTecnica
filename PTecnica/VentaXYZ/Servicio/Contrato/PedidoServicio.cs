using AutoMapper;
using Azure;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using VentaXYZ.DTO;
using VentaXYZ.Modelo;
using VentaXYZ.Repositorio.Contratos;
using VentaXYZ.Repositorio.Implementacion;
using VentaXYZ.Servicio.Implementacion;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace VentaXYZ.Servicio.Contrato
{
    public class PedidoServicio : IPedidoServicio
    {
        private readonly IMapper _mapper; //Homologacion de clases Modelo en DTO y viceversa
        private readonly IPedidoRepositorio _repositorio; //Contrato al pedido   
        private readonly IGenericos<Pedido> _modeloGenericoPedido; //Contrato a los pedidos genericos
        private readonly IGenericos<Usuario> _modeloGenericoUsuario; //Contrato con los usuarios genericos
        private readonly IUsuarioRepositorio _usuarioRepositorio; //Contrato con usuario
        public PedidoServicio(IPedidoRepositorio repositorio, IMapper mapper, IGenericos<Pedido> modeloRepositorio, IGenericos<Usuario> modeloGenericoUsuario, IUsuarioRepositorio usuarioRepositorio)
        {
            //Inicializando Pedido 
            _mapper = mapper;
            _repositorio = repositorio;
            _modeloGenericoPedido = modeloRepositorio;
            _modeloGenericoUsuario = modeloGenericoUsuario;
            _usuarioRepositorio = usuarioRepositorio;
        }

        public async Task<ResponseDTO<List<PermisoDTO>>> validacionPedido(PedidoDTO modelo, string _Controllador, int opt)
        {
           //Validacion inicial de pedido y de permiso de usuario 
            var response = new ResponseDTO<List<PermisoDTO>>(); //Respuesta de validacion
            response.status = true; 

            //Obligar a ingresar usuario
            if (modelo.CodUsuario is null || modelo.CodUsuario.Length==0)
            {
                response.status = false;
                response.msg = "Ingresar código de usuario";
            }
            else
            {
                //Buscar existencia de usuario
                var consultaU = _modeloGenericoUsuario.Consultar(p => p.CodUsuario == modelo.CodUsuario);
                var fromDBModeloU = await consultaU.FirstOrDefaultAsync();
                if (fromDBModeloU == null)
                {
                    response.status = false;
                    response.msg = "No existe el usuario";
                    return response;
                }
                else
                {
                    //Verificar permisos de usuario
                   response.value = await _usuarioRepositorio.verificarPermiso(fromDBModeloU.CodRol, _Controllador, opt, modelo.Estado.ToString());
                   if (response.value.Count==0)
                   {
                        response.status = false;
                        response.msg = "Usuario no cuenta con autorizaciòn";
                        return response;
                    }
                }
            }

            //Obligar a ingresar estado
            if (modelo.Estado < 0 || modelo.Estado>3) //No desbordar estados de pedido
            {
                response.status = false;
                response.msg = response.msg +", "+ "Código de estado incorrecto";
            }
            else
            {
                    if(opt==1 && modelo.Estado >0) //No desbordar estado en registro nuevo
                    {
                        response.status = false;
                        response.msg = response.msg + ", " + "Código de estado incorrecto";
                    }
            }

            return response;
        }

        public async Task<ResponseDTO<bool>> actualizar(PedidoDTO modelo, string _Controllador)
        {
            //Actualizar Pedido
            var responsePermiso = new ResponseDTO<List<PermisoDTO>>(); //Respuesta de validacion
            var response = new ResponseDTO<bool>(); //Respuesta de servicio

            response.status = false;
            List<PermisoDTO> Permiso = new List<PermisoDTO>();

            try
            {
                responsePermiso = await validacionPedido(modelo,  _Controllador,2);

                if (responsePermiso.status) {

                    //Validacion de Pedido
                    var consultaP = _modeloGenericoPedido.Consultar(p => p.CodPedido == modelo.CodPedido);
                    var fromDBModeloP = await consultaP.FirstOrDefaultAsync();

                    if (fromDBModeloP == null)
                    {
                        response.status = false;
                        response.msg = response.msg + ", " + "No existe Pedido";
                    }
                    else
                    {
                        //Homologando modelo con clase DTO para acceder al contexto
                        var pedid = _mapper.Map<Pedido>(fromDBModeloP);

                        response.status = false;
                        foreach (var obj in responsePermiso.value)
                        {
                            if (obj.optEstado != null)
                            {
                                if (obj.optEstado.Trim().Split(",").Contains(pedid.Estado.ToString())) //Validacion de permiso para cambio de estado
                                    response.status = true;
                            }
                            else
                                response.status = true;
                        }

                        
                        if (!response.status) //Validacion de permiso de usuario
                        {
                            response.status = false;
                            response.msg = response.msg + ", " + "Usuario no tiene permiso para realizar la actualizaciòn";
                        }
                        else
                        { 
                            if (pedid.Estado > modelo.Estado) //Validacion para actualizacon de pedidos
                            {
                                    response.status = false;
                                    response.msg = response.msg + ", " + "Estado a modificar es incorrecto";
                            }
                            else
                                {
                                    if (pedid.Estado == 3) //Validacion para no modificar pedidos con estado recibido 
                                    {
                                        response.status = false;
                                        response.msg = response.msg + ", " + "Estado Recibido no se puede modificar";
                                    }
                                    else
                                    {
                                        pedid.Estado = modelo.Estado;

                                        switch (modelo.Estado) //Actualizacion de fechas por estado
                                        {
                                            case 1:
                                                pedid.CodVendedor = modelo.CodUsuario;
                                                pedid.FechaRecepcion = DateTime.Now;
                                                break;
                                            case 2:
                                                pedid.CodRepartidor = modelo.CodUsuario;
                                                pedid.FechaDespacho = DateTime.Now;
                                                break;
                                            default:
                                                pedid.CodRepartidor = modelo.CodUsuario;
                                                pedid.FechaEntrega = DateTime.Now;
                                                break;
                                        }

                                        response.value = await _modeloGenericoPedido.Editar(pedid); //actualizando pedido usando clase generica
                                        response.status = true;
                                        response.msg = "OK";
                                    }

                                }
                        }
                    }
                    
                }
            }
            catch (Exception e)
            {
                response.status = false;
                response.msg = e.Message;
            }

            return response;
        }

        public async Task<PedidoDTO> Registrar(PedidoDTO modelo, string _Controllador)
        {
            //Registra un nuevo pedido comunicandose con el contrado 
            var response = new ResponseDTO<List<PermisoDTO>>(); //Inicializando objeto de respuesta de validacion de permiso del usuario
            response.status = false; //Inicializando respuesta en correcto

            try
            {

                response = await validacionPedido(modelo, _Controllador, 1); //Validando permisos del usuario

                if (!response.status)
                    throw new TaskCanceledException(response.msg); //Cancelando registro por error de validacion 

                var dbmodelo = _mapper.Map<Pedido>(modelo); //Homologando Modelo con DTO
                var pedidorpt = await _repositorio.Registrar(dbmodelo); //Comunicandose con el contrado de pedido de EF 

                if (pedidorpt.CodPedido > 0) //Validando registro exitoso 
                    return _mapper.Map<PedidoDTO>(pedidorpt);  //Homologando DTO con Modelo 
                else
                    throw new TaskCanceledException("No se pudo registrar venta"); //Abortando el registro 
            }
            catch (Exception e)
            {
                response.status = false;
                response.msg = e.Message; 
                throw e;
            }

        }
    }
}
