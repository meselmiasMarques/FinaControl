using FinaControl.Data;
using FinaControl.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace FinaControl.Repositories;

public class TransactionRepository(FinaControlDbContext context) : Repository<Transaction>(context)
{

    public async Task<List<Transaction>> GetTransactionWithCategoriesAsync(int skip, int take)
    {
        var transactions = await context
            .Transactions
            .AsNoTracking()
            .Include(c => c.Category)
            .Include(u => u.User)
            .Skip(skip)
            .Take(take)
            .ToListAsync();
        
        return transactions;
    }
}