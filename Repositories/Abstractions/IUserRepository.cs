using FinaControl.Models;

namespace FinaControl.Repositories.Abstractions;

public interface IUserRepository
{
    Task<List<User>> GetUsersWithRolesEndTransactions(int skip = 0, int take = 25);
    Task<User> GetUserByEmail(string email);
    Task<User> GetAsync(long id);
    Task<List<User>> GetAsync(int skip = 0, int take = 25);

    void Update(User user);
    Task CreateAsync(User user);
}