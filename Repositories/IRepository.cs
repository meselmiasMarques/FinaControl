namespace FinaControl.Repositories;

public interface IRepository<T> where T : class
{
    Task<List<T>> Get(int skip = 0, int take = 25);
    Task<T> Get(long id);
    Task Create(T entity);
    Task Update(T entity);
    Task Delete(T entity);
}