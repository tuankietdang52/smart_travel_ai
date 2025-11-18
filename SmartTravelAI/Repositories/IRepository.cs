namespace SmartTravelAI.Repositories
{
    public interface IRepository<T> where T : class
    {
        public Task<T?> GetByIdAsync(long id);
        public Task<IEnumerable<T>> GetAllAsync();
        public Task<T> AddAsync(T entity);
        public Task<T> UpdateAsync(long id, T entity);
        public Task DeleteAsync(long id);
    }
}