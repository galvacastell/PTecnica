using VentaXYZ.Modelo;

namespace VentaXYZ.DTO
{
    public class DetallePedidoDTO
    {
        public int? CodPedido { get; set; }

        public string? CodSku { get; set; }

        public int? Item { get; set; }

        public decimal? Cantidad { get; set; }

    }
}
