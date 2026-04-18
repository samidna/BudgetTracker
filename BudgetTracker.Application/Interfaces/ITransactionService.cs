using BudgetTracker.Application.DTOs.Transaction;

namespace BudgetTracker.Application.Interfaces;
public interface ITransactionService
{
    Task<IEnumerable<TransactionDto>> GetAllTransactionsAsync(string userId);
    Task<bool> CreateTransactionAsync(CreateTransactionDto dto, string userId);
}
