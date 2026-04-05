using FinaControl.Data;
using FinaControl.Repositories.Abstractions;

namespace FinaControl.Repositories;

public class UnitOfWork(FinaControlDbContext context) : IUnitOfWork
{
    public async Task CommitAsync()
        => await context.SaveChangesAsync();
    
}