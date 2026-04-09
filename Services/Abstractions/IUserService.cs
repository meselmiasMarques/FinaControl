using FinaControl.Models;
using FinaControl.ViewModels.Response;
using FinaControl.ViewModels.Role;
using FinaControl.ViewModels.User;

namespace FinaControl.Services;

public interface IUserService
{
    Task<Response<List<User>>> GetUsersWithRolesEndTransactions(int skip = 0, int take = 25);
    Task<Response<User>> GetUserByEmail(string email);
    Task<Response<User>> GetAsync(long id);
    Task<Response<List<User>>> GetAsync(int skip = 0, int take = 25);

    Task<Response<User>>  Update(EditorUserViewModel user, long id);
    Task<Response<User>> CreateAsync(EditorUserViewModel user);
    Task<Response<User>> UpdateRolesByUserAsync(AssociateRoleUserViewModel model);
}