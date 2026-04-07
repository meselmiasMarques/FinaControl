using FinaControl.Models;
using FinaControl.Repositories.Abstractions;
using FinaControl.Services;

namespace FinaControl.Repositories;

public class TransactionService(
    ITransactionRepository repository,
    IUnitOfWork unitOfWork
    ) : ITransactionService
{
    public async Task<List<Transaction>> GetTransactionByUserAsync(int skip, int take, User? user)
    {
        try
        {
            var transaction = await repository.GetTransactionByUserAsync(skip, take, user);
            return transaction;
        }
        catch 
        {
           return null;
        }
    }

    public async Task<List<Transaction>> GetAsync(int skip = 0, int take = 25)
    {
       var transactions =  await repository.GetAsync(skip, take);
       return transactions;
    }

    public async Task<Transaction> GetAsync(long id)
    {
        var transaction = await repository.GetAsync(id);
        return transaction;
    }

    public async Task CreateAsync(Transaction entity)
    {
       await repository.CreateAsync(entity);
       await unitOfWork.CommitAsync();
    }

    public void Update(Transaction entity)
    {
        throw new NotImplementedException();
    }

    public void Delete(Transaction entity)
    {
        throw new NotImplementedException();
    }
}