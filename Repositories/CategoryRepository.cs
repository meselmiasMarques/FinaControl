using FinaControl.Data;
using FinaControl.Models;

namespace FinaControl.Repositories;

public class CategoryRepository(FinaControlDbContext context) : Repository<Category>(context)
{
    
}