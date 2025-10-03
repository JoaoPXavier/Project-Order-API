namespace OrdersApi.Infrastructure.Repositories
{
    // Interface genérica  - facilita testes e separação de infra
    public interface IGenericRepository<T> where T : class
    {
        Task<T?> GetByIdAsync(int id);
        Task AddAsync(T entity);
        void Update(T entity);
        void Remove(T entity);
        Task SaveChangesAsync();
    }
}
