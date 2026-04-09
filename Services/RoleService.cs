using FinaControl.Models;
using FinaControl.Repositories.Abstractions;
using FinaControl.ViewModels.Response;
using FinaControl.ViewModels.Role;

namespace FinaControl.Services;

public class RoleService (
    IRoleRepository roleRepository,
    IUnitOfWork unitOfWork
    ) : IRoleService
{
    public async Task<Response<List<Role>>> GetAsync(int skip = 0, int take = 25)
    {
        try
        {
            var roles = await roleRepository.GetAsync(skip, take);
            return new Response<List<Role>>(roles);
        }
        catch 
        {
            return  new Response<List<Role>>("erro interno no servidor");
        }
    }

    public async Task<Response<Role>> GetAsync(long id)
    {
        var  role = await roleRepository.GetAsync(id);
        return new Response<Role> (role);
    }

    public async Task CreateAsync(EditorRoleViewModel entity)
    {
        var role =  new Role
        {
            Name = entity.Name,
            CreatedAt =  DateTime.UtcNow
        };
        await roleRepository.CreateAsync(role);
        await unitOfWork.CommitAsync();
        
    }

    public async Task<Response<Role>> UpdateAsync(EditorRoleViewModel entity, long id)
    {
        var role = await roleRepository.GetAsync(id);
        if (role != null)
            return new Response<Role>("role in not found");
        
        role.Name = entity.Name;
        
        roleRepository.UpdateAsync(role);
        await unitOfWork.CommitAsync();
        return new Response<Role>(role);
        
    }

    public async Task<Response<Role>> DeleteAsync(long id)
    {
        var role = await roleRepository.GetAsync(id);
        if (role != null)
            return new Response<Role>("role in not found"); 
        await roleRepository.DeleteAsync(role);
        await unitOfWork.CommitAsync();
        return new Response<Role>(role);
    }
}