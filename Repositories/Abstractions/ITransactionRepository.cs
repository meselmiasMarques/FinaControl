using FinaControl.Models;

namespace FinaControl.Repositories.Abstractions;

public interface ITransactionRepository
{
    Task<List<Transaction>> GetTransactionByUserAsync(int skip, int take, User? user);
    Task<List<Transaction>> GetAsync(int skip = 0, int take = 25);
    Task<Transaction> GetAsync(long id);
    Task CreateAsync(Transaction entity);
    void UpdateAsync(Transaction entity);
    Task DeleteAsync(Transaction entity);
}