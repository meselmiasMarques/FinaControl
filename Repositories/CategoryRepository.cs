using FinaControl.Data;
using FinaControl.Models;
using Microsoft.EntityFrameworkCore;

namespace FinaControl.Repositories;

public class CategoryRepository(FinaControlDbContext context) : Repository<Category>(context)
{
    public async Task<List<Category>> GetCategoriesByUserAsync(User user)
    {
        var categories = await context
            .Categories
            .AsNoTracking()
            .Where(c => c.UserId == user.Id)
            .ToListAsync();
        
        return categories;
    }
}