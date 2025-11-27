using FinaControl.Data;
using FinaControl.Models;
using Microsoft.EntityFrameworkCore;

namespace FinaControl.Repositories;

public class UserRepository(FinaControlDbContext context) : IRepository<User>
{
    private readonly FinaControlDbContext _context = context;
    
    public async Task<List<User>> Get(int skip = 0, int take = 25)
    {
        var users = await _context.Users.AsNoTracking().Skip(skip).Take(take).ToListAsync();
        return users;
    }
    
    public async Task<User> Get(long id)
    {
        var user = await _context.Users.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
            return user;
    }

    public async Task Create(User entity)
    {
        var user =  await _context.Users.AddAsync(entity);
        await _context.SaveChangesAsync();
    }

    public async Task Update(User entity)
    {
        _context.Users.Update(entity);
        await _context.SaveChangesAsync();
    }

    public async Task Delete(User entity)
    {
        _context.Users.Remove(entity);
        await _context.SaveChangesAsync();
    }
}