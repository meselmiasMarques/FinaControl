using FinaControl.Data;
using FinaControl.Models;

namespace FinaControl.Repositories;

public class RoleRepository(FinaControlDbContext context) : Repository<Role>(context)
{
    
}