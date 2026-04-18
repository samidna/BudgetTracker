using BudgetTracker.Domain.Entities;

namespace BudgetTracker.Application.Interfaces;
public interface ITransactionRepository : IGenericRepository<Transaction>
{
    Task<IEnumerable<Transaction>> GetRecentTransactionsAsync(string userId, int count);
}
