using OrdersApi.Domain.Entities;

namespace OrdersApi.Infrastructure.Repositories
{
    public interface IPedidoRepository : IGenericRepository<Pedido>
    {
        Task<Pedido?> GetWithOcorrenciasAsync(int id);
        Task<IEnumerable<Pedido>> ListAllAsync();
        Task<bool> ExisteNumeroPedidoAsync(int numeroPedido); 
    }
}