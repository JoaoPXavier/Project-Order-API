namespace OrdersApi.Infrastructure.Repositories
{
    // Generic interface - facilitates testing and infrastructure separation
    public interface IGenericRepository<T> where T : class
    {
        Task<T?> GetByIdAsync(int id);
        Task AddAsync(T entity);
        void Update(T entity);
        void Remove(T entity);
        Task SaveChangesAsync();
    }
}
