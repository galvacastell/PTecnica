using System.Transactions;
using VentaXYZ.Modelo;
using VentaXYZ.Repositorio.Contratos;

namespace VentaXYZ.Repositorio.Implementacion
{
    public class PedidoRepositorio : Generico<Pedido>,IPedidoRepositorio
    {
        private readonly DbventaXyzContext _dbContext; //Conexion con BD por medio del contexto

        public PedidoRepositorio(DbventaXyzContext dbContext):base(dbContext)
        {
            //Iniciando contexto
            _dbContext = dbContext;
        }

        public async Task<Pedido> Registrar(Pedido modelo)
        {
            //Registrar Pedido
            Pedido PedidoGenerado = new Pedido();

            using(var transaction = _dbContext.Database.BeginTransaction() ) //Abriendo transaccion
            {
                try
                {
                    foreach(DetallePedido dp in modelo.DetallePedidos)
                    {
                        Producto producto_pedido = _dbContext.Productos.Where(p => p.CodSku == dp.CodSku).First();

                        producto_pedido.Stock = producto_pedido.Stock - dp.Cantidad;

                        if(producto_pedido.Stock<0) //Validacion de stock
                        {
                            transaction.Rollback(); //Abortando transaccion
                            throw new TaskCanceledException("Stock Insuficiente");
                        }
                    }

                    await _dbContext.SaveChangesAsync(); //guardando cambio en sku

                    await _dbContext.Pedidos.AddAsync(modelo); 
                    await _dbContext.SaveChangesAsync(); //Guardando pedido

                    PedidoGenerado = modelo;
                    transaction.Commit(); //Confirmando registro
                }
                catch (Exception ex)
                {
                    transaction.Rollback(); //Abortando transaccion
                    throw ex;
                }
            }

            return PedidoGenerado;
        }

    }
}
