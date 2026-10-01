using FinaControl.Models;

namespace FinaControl.Services.Abstractions;

public interface ITransactionService
{
    Task<List<Transaction>> GetTransactionByUserAsync(int skip, int take, User? user);
    Task<List<Transaction>> GetAsync(int skip = 0, int take = 25);
    Task<Transaction> GetAsync(long id);
    Task CreateAsync(Transaction entity);
    Task Update(Transaction entity);
    Task Delete(Transaction entity);
}