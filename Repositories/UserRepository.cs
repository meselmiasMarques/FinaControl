using FinaControl.Data;
using FinaControl.Models;
using Microsoft.EntityFrameworkCore;

namespace FinaControl.Repositories;

public class UserRepository(FinaControlDbContext context) : Repository<User>(context)
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
}