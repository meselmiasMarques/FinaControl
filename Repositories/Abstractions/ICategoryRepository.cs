using FinaControl.Models;

namespace FinaControl.Repositories.Abstractions;

public interface ICategoryRepository
{
    Task<List<Category>> GetAsync(int skip = 0, int take = 25);
    Task<Category> GetAsync(long id);
    Task CreateAsync(Category entity);
    void UpdateAsync(Category entity);
    Task DeleteAsync(Category entity);
    Task<List<Category>> GetCategoriesByUserAsync(User user);
}