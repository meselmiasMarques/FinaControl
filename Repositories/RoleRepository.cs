using FinaControl.Data;
using FinaControl.Models;
using FinaControl.Repositories.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace FinaControl.Repositories;

public class RoleRepository(FinaControlDbContext context) : IRoleRepository
{
    public async Task<List<Role>> GetAsync(int skip = 0, int take = 25)
     => await context.Roles.AsNoTracking().Skip(skip).Take(take).ToListAsync();

    public async Task<Role> GetAsync(long id)
    =>  await context.Roles.AsNoTracking().FirstOrDefaultAsync(role => role.Id == id);

    public async Task CreateAsync(Role entity)
    => await context.Roles.AddAsync(entity);

    public void UpdateAsync(Role entity)
    => context.Roles.Update(entity);

    public async Task DeleteAsync(Role entity)
    =>  context.Roles.Remove(entity);
}