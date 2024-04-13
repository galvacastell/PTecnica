using System.Transactions;
using VentaXYZ.Modelo;
using VentaXYZ.Repositorio.Contratos;

namespace VentaXYZ.Repositorio.Implementacion
{
    public class PedidoRepositorio : Generico<Pedido>,IPedidoRepositorio
    {
        private readonly DbventaXyzContext _dbContext;
       // private readonly IGenericos<Pedido> _generico;

        public PedidoRepositorio(DbventaXyzContext dbContext):base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Pedido> Registrar(Pedido modelo)
        {
            Pedido PedidoGenerado = new Pedido();

            using(var transaction = _dbContext.Database.BeginTransaction() )
            {
                try
                {
                    foreach(DetallePedido dp in modelo.DetallePedidos)
                    {
                        Producto producto_pedido = _dbContext.Productos.Where(p => p.CodSku == dp.CodSku).First();

                        producto_pedido.Stock = producto_pedido.Stock - dp.Cantidad;
                    }

                    await _dbContext.SaveChangesAsync();

                    await _dbContext.Pedidos.AddAsync(modelo);
                    await _dbContext.SaveChangesAsync();

                    PedidoGenerado = modelo;
                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw ex;
                }
            }

            return PedidoGenerado;
        }

    }
}
