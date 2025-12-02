using FinaControl.Data;
using Microsoft.EntityFrameworkCore;

namespace FinaControl.Repositories;

public abstract class Repository<T>(FinaControlDbContext context) : IRepository<T>
    where T : class
{
    private readonly FinaControlDbContext _context = context;

    public async Task<List<T>> GetAsync(int skip = 0, int take = 25)
    {
        return await _context.Set<T>().Skip(skip).Take(25).ToListAsync();
    }

    public async Task<T?> GetAsync(long id)
    {
        return await _context.Set<T>().FindAsync(id);
    }

    public async Task CreateAsync(T entity)
    {
        await _context.Set<T>().AddAsync(entity);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(T entity)
    {
         _context.Set<T>().Update(entity);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(T entity)
    {
        _context.Set<T>().Remove(entity);
        await _context.SaveChangesAsync();
    }
}