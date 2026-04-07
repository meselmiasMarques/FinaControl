using FinaControl.Data;
using FinaControl.Models;
using FinaControl.Repositories.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace FinaControl.Repositories;

public class CategoryRepository(FinaControlDbContext context) : ICategoryRepository
{
    public async Task<List<Category>> GetAsync(int skip = 0, int take = 25)
        => await context.Categories.AsNoTracking().Skip(skip).Take(take).ToListAsync();

    public async Task<Category?> GetAsync(long id)
        => await context.Categories.FirstOrDefaultAsync(x => x.Id == id);

    public async Task CreateAsync(Category entity)
        => await context.Categories.AddAsync(entity);

    public void Update(Category entity)
        => context.Categories.Update(entity);
    
    public void Delete(Category entity)
        => context.Categories.Remove(entity);
    
    public async Task<List<Category>> GetCategoriesByUserAsync(User user)
         => await context
            .Categories
            .AsNoTracking()
            .Where(c => c.UserId == user.Id)
            .ToListAsync();
       
}