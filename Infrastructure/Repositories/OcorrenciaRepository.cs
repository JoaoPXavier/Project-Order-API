using OrdersApi.Infrastructure.Data;
using OrdersApi.Domain.Entities;

namespace OrdersApi.Infrastructure.Repositories
{
    public class OcorrenciaRepository : GenericRepository<Ocorrencia>, IOcorrenciaRepository
    {
        public OcorrenciaRepository(OrdersDbContext ctx) : base(ctx) { }
    }
}
