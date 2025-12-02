namespace FinaControl.Repositories;

public interface IRepository<T> where T : class
{
    Task<List<T>> GetAsync(int skip = 0, int take = 25);
    Task<T> GetAsync(long id);
    Task CreateAsync(T entity);
    Task UpdateAsync(T entity);
    Task DeleteAsync(T entity);
}