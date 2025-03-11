namespace Trade.Infrastructure.Repositories
{
    public interface IRepository<T>
    {
        public Task<List<T>> GetAll();

        public Task<T?> GetById(Guid id);

        public Task<T> Add(T entity);

        public Task<T?> Update(T entity);

        public Task Delete(Guid id);
    }
}