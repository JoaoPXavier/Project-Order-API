using Microsoft.EntityFrameworkCore;
using OrdersApi.Infrastructure.Data;

namespace OrdersApi.Infrastructure.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected readonly OrdersDbContext _ctx;
        public GenericRepository(OrdersDbContext ctx) => _ctx = ctx;

        public async Task AddAsync(T entity) => await _ctx.Set<T>().AddAsync(entity);
        public async Task<T?> GetByIdAsync(int id) => await _ctx.Set<T>().FindAsync(id) as T;
        public void Update(T entity) => _ctx.Set<T>().Update(entity);
        public void Remove(T entity) => _ctx.Set<T>().Remove(entity);
        public async Task SaveChangesAsync() => await _ctx.SaveChangesAsync();
    }
}
