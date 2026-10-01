using FinaControl.Models;
using FinaControl.Repositories.Abstractions;
using FinaControl.Services.Abstractions;

namespace FinaControl.Services;

public class TransactionService(
    ITransactionRepository repository,
    IUnitOfWork unitOfWork) : ITransactionService
{
    public async Task<List<Transaction>> GetTransactionByUserAsync(int skip, int take, User? user)
    {
        throw new NotImplementedException();
    }

    public async Task<List<Transaction>> GetAsync(int skip = 0, int take = 25)
    {
        throw new NotImplementedException();
    }

    public async Task<Transaction> GetAsync(long id)
    {
        throw new NotImplementedException();
    }

    public async Task CreateAsync(Transaction entity)
    {
        throw new NotImplementedException();
    }

    public async Task Update(Transaction entity)
    {
        throw new NotImplementedException();
    }

    public async Task Delete(Transaction entity)
    {
        throw new NotImplementedException();
    }
}   
