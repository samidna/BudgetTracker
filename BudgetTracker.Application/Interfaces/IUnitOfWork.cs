using BudgetTracker.Domain.Entities;

namespace BudgetTracker.Application.Interfaces;
public interface IUnitOfWork : IDisposable
{
    ITransactionRepository Transactions { get; }
    IGenericRepository<Category> Categories { get; }
    Task<int> CompleteAsync();
}
