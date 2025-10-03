using Microsoft.EntityFrameworkCore;
using OrdersApi.Domain.Entities;
using OrdersApi.Infrastructure.Data;

namespace OrdersApi.Infrastructure.Repositories
{
    public class PedidoRepository : GenericRepository<Pedido>, IPedidoRepository
    {
        public PedidoRepository(OrdersDbContext ctx) : base(ctx) { }

        public async Task<Pedido?> GetWithOcorrenciasAsync(int id)
        {
            
            return await _ctx.Pedidos
                .Include(p => p.Ocorrencias)
                .FirstOrDefaultAsync(p => p.IdPedido == id);
        }

        public async Task<IEnumerable<Pedido>> ListAllAsync()
        {
            
            return await _ctx.Pedidos
                .Include(p => p.Ocorrencias)
                .ToListAsync();
        }

        public async Task<bool> ExisteNumeroPedidoAsync(int numeroPedido)
        {
            return await _ctx.Pedidos
                .AnyAsync(p => p.NumeroPedido.Numero == numeroPedido);
        }
    }
}