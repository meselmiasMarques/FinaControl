using FinaControl.Data;
using FinaControl.Models;
using FinaControl.Repositories.Abstractions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace FinaControl.Repositories;

public class TransactionRepository(FinaControlDbContext context) : ITransactionRepository
{

    public async Task<List<Transaction>> GetTransactionByUserAsync(int skip, int take, User? user)
    {
        var transactions = await context
            .Transactions
            .AsNoTracking()
            .Include(c => c.Category)
            .Include(u => u.User)
            .Where(t => t.UserId == user.Id)
            .Skip(skip)
            .Take(take)
            .ToListAsync();
        
        return transactions;
    }

    public async Task<List<Transaction>> GetAsync(int skip = 0, int take = 25)
        => await context.Transactions.AsNoTracking().Skip(skip).Take(take).ToListAsync();
    

    public async Task<Transaction> GetAsync(long id)
        => await context.Transactions.AsNoTracking().FirstOrDefaultAsync(t => t.Id == id); 

    public async Task CreateAsync(Transaction entity)
        => await context.Transactions.AddAsync(entity);

    public void Update(Transaction entity)
        => context.Transactions.Update(entity);

    public void Delete(Transaction entity)
        => context.Transactions.Remove(entity);
}