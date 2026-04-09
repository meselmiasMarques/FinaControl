using FinaControl.Data;
using FinaControl.Models;
using FinaControl.Repositories.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace FinaControl.Repositories;

public class UserRepository(FinaControlDbContext context) : IUserRepository
{
    
    public async Task<List<User>> GetUsersWithRolesEndTransactions(int skip = 0, int take = 25)
    {

        var users = await context.Users
            .AsNoTracking()
            .Include(r => r.Roles)
            .Include(t => t.Transactions)
            .Skip(skip)
            .Take(take)
            .ToListAsync();
        return users;
    }


    public async Task<User> GetUserByEmail(string email)
    {
        var user = await context
            .Users
            .AsNoTracking()
            .Include(r => r.Roles)
            .FirstOrDefaultAsync(x => x.Email == email);
        return user;
    }

    public async Task<User?> GetAsync(long id)
        => await context.Users.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
    
    public async Task<List<User>> GetAsync(int skip = 0, int take = 25)
        => await  context.Users.AsNoTracking().Skip(skip).Take(take).ToListAsync();

    public void Update(User user)
    {
         context.Users.Update(user);
    }

    public async Task CreateAsync(User user)
        => await context.Users.AddAsync(user);
    
}