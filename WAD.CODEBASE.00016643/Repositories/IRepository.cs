namespace WAD.CODEBASE._00016643.Repositories
{
    public interface IRepository<T>
    {
        Task CreateAsync(T entity);
        Task DeleteAsync(T entity);
        Task UpdateAsync(T entity);
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> GetByIdAsync(int id);
    }
}
