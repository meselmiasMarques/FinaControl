namespace FinaControl.Repositories.Abstractions;

public interface IUnitOfWork
{
    Task CommitAsync();
    
}