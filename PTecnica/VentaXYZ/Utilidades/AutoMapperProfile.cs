using AutoMapper;
using VentaXYZ.DTO;
using VentaXYZ.Modelo;

namespace VentaXYZ.Utilidades
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile() 
        {
            //Mapas para homologacion de modelos y DTO
            CreateMap<Usuario,UsuarioDTO>();
            CreateMap<Usuario, PermisoDTO>();
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
