using FinaControl.Models;
using FinaControl.Repositories.Abstractions;
using FinaControl.ViewModels.Response;
using FinaControl.ViewModels.Role;
using FinaControl.ViewModels.User;

namespace FinaControl.Services;

public class UserService(
    IUserRepository userRepository,
    IRoleRepository roleRepository,
    IUnitOfWork unitOfWork) : IUserService
{
    public async Task<Response<List<User>>> GetUsersWithRolesEndTransactions(int skip = 0, int take = 25)
    {
        try
        {
            var users = await userRepository.GetUsersWithRolesEndTransactions(skip, take);
            if (users is null)
                return new Response<List<User>>("User not found");
            return new Response<List<User>>(users);
        }
        catch
        {
            return new Response<List<User>>("erro interno no servidor");
        }
    }

    public async Task<Response<User>> GetUserByEmail(string email)
    {
        try
        {
            var user = await userRepository.GetUserByEmail(email);
            if (user is null)
                return new Response<User>("User not found");
            return new Response<User>(user);
        }
        catch
        {
            return new Response<User>("erro interno no servidor");

        }
    }

    public async Task<Response<User>> GetAsync(long id)
    {
        try
        {
            var user = await userRepository.GetAsync(id);
            if (user is null)
                return new Response<User>("User not found");
            return new Response<User>(user);
        }
        catch
        {
            return new Response<User>("erro interno no servidor");
        }
    }

    public async Task<Response<List<User>>> GetAsync(int skip = 0, int take = 25)
    {
        try
        {
            var users = await userRepository.GetAsync(skip, take);
            if (users is null)
                return new Response<List<User>>("User not found");
            return new Response<List<User>>(users);
        }
        catch
        {
            return new Response<List<User>>("erro interno no servidor");
        }
    }

    public async Task<Response<User>> Update(EditorUserViewModel model, long id)
    {
        try
        {
            var user = await userRepository.GetAsync(id);
            if (user is null)
                return new Response<User>("User not found");
            user.Name = model.name;
            user.Email = model.email;

            userRepository.Update(user);
            await unitOfWork.CommitAsync();
            return new Response<User>(user);
        }
        catch
        {
            return new Response<User>("erro interno no servidor");
        }

    }

    public async Task<Response<User>> CreateAsync(EditorUserViewModel model)
    {
        var user = new User
        {
            Name = model.name,
            Email = model.email,

        };
        try
        {
            await userRepository.CreateAsync(user);
            await unitOfWork.CommitAsync();
            return new Response<User>(user);
        }
        catch (Exception e)
        {
            return new Response<User>("erro interno no servidor");
        }

    }

    public async Task<Response<User>> UpdateRolesByUserAsync(AssociateRoleUserViewModel model)
    {
        var role = await roleRepository.GetAsync(model.RoleId);
        if (role == null)
            return new Response<User>("Role not found");

        var user = await userRepository.GetAsync(model.UserId);
        if (user == null)
            return new Response<User>("User not found");

        if (user.Roles.Any(r => r.Id == model.RoleId))
            return new Response<User>("Perfil já está associado ao usuário");
        

        try
        {
            user.Roles.Add(role);
            userRepository.Update(user);
            await unitOfWork.CommitAsync();
            return new Response<User>(user);
        }
        catch
        {
            return new Response<User>("Erro Interno no Servidor");
        }
    }
}