using FinaControl.Models;

namespace FinaControl.Repositories.Abstractions;

public interface IRoleRepository
{
    Task<List<Role>> GetAsync(int skip = 0, int take = 25);
    Task<Role> GetAsync(long id);
    Task CreateAsync(Role entity);
    void UpdateAsync(Role entity);
    Task DeleteAsync(Role entity);
}