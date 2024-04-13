using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.Eventing.Reader;
using VentaXYZ.DTO;
using VentaXYZ.Modelo;
using VentaXYZ.Repositorio.Contratos;
using VentaXYZ.Repositorio.Implementacion;
using VentaXYZ.Servicio.Implementacion;

namespace VentaXYZ.Servicio.Contrato
{
    public class PedidoServicio : IPedidoServicio
    {
        private readonly IMapper _mapper;
        private readonly IPedidoRepositorio _repositorio;
        private readonly IGenericos<Pedido> _modeloGenericoPedido;
        private readonly IGenericos<Usuario> _modeloGenericoUsuario;
        public PedidoServicio(IPedidoRepositorio repositorio, IMapper mapper, IGenericos<Pedido> modeloRepositorio, IGenericos<Usuario> modeloGenericoUsuario)
        {
            _mapper = mapper;
            _repositorio = repositorio;
            _modeloGenericoPedido = modeloRepositorio;
            _modeloGenericoUsuario = modeloGenericoUsuario;
        }

        public async Task<ResponseDTO<bool>> validacionPedido(PedidoDTO modelo)
        {
            var response = new ResponseDTO<bool>();
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
            }

            //Obligar a ingresar estado
            if (modelo.Estado <= 0 || modelo.Estado>=5)
            {
                response.status = false;
                response.msg = response.msg +", "+ "Código de estado incorrecto";
            }
            else
            {
                if(modelo.Estado==4)
                {
                    response.status = false;
                    response.msg = response.msg + ", " + "Estado Recibido no se puede modificar";
                }
                else
                { 
                    //Buscar Existencia de Pedido
                    var consultaP = _modeloGenericoPedido.Consultar(p => p.CodPedido == modelo.CodPedido);
                    var fromDBModeloP = await consultaP.FirstOrDefaultAsync();

                    if (fromDBModeloP == null)
                    {
                        response.status = false;
                        response.msg = response.msg + ", " + "No existe Pedido";
                    }
                }
            }

            return response;
        }

        public async Task<ResponseDTO<bool>> actualizar(PedidoDTO modelo)
        {

            var response = new ResponseDTO<bool>();
            response.status = false;

            try
            {

                response = await validacionPedido(modelo);

                if (response.status) { 
                    var pedid = _mapper.Map<Pedido>(modelo);
                    response.value = await _modeloGenericoPedido.Editar(pedid);
                    response.status = true;
                }
            }
            catch (Exception e)
            {
                response.status = false;
                response.msg = e.Message;
            }

            return response;
        }

        public async Task<PedidoDTO> Registrar(PedidoDTO modelo)
        {

            try
            {

                var dbmodelo = _mapper.Map<Pedido>(modelo);
                var pedidorpt = await _repositorio.Registrar(dbmodelo);

                if (pedidorpt.CodPedido > 0)
                    return _mapper.Map<PedidoDTO>(pedidorpt);
                else
                    throw new TaskCanceledException("No se pudo registrar venta");
            }
            catch (Exception e)
            {
                throw e;
            }

        }
    }
}
