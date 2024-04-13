using AutoMapper;
using VentaXYZ.DTO;
using VentaXYZ.Modelo;

namespace VentaXYZ.Utilidades
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile() 
        {
            CreateMap<Usuario,UsuarioDTO>();
            CreateMap<Usuario, SesionDTO>();
            CreateMap<UsuarioDTO, Usuario>();
            CreateMap<Pedido, PedidoDTO>();
            CreateMap<PedidoDTO, Pedido>();
            CreateMap<DetallePedido, DetallePedidoDTO>();
            CreateMap<DetallePedidoDTO, DetallePedido>();
            //CreateMap<PedidoDTO, Pedido>().ForMember(dest=> 
            // dest.CodRepartidorNavigation,opy => opy.Ignore());
        }
    }
}
