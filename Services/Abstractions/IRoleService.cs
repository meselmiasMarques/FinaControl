using FinaControl.Models;
using FinaControl.ViewModels.Response;
using FinaControl.ViewModels.Role;

namespace FinaControl.Services;

public interface IRoleService
{
    
    Task<Response<List<Role>>> GetAsync(int skip = 0, int take = 25);
    Task<Response<Role>> GetAsync(long id);
    Task CreateAsync(EditorRoleViewModel entity);
    Task<Response<Role>> UpdateAsync(EditorRoleViewModel entity, long id);
    Task<Response<Role>>  DeleteAsync(long id);
}