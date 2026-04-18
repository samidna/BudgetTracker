using BudgetTracker.Application.DTOs.Transaction;
using BudgetTracker.Application.Interfaces;
using BudgetTracker.Domain.Entities;

namespace BudgetTracker.Application.Services;
public class TransactionService : ITransactionService
{
    private readonly IUnitOfWork _unitOfWork;

    public TransactionService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<bool> CreateTransactionAsync(CreateTransactionDto dto, string userId)
    {
        var transaction = new Transaction
        {
            Title = dto.Title,
            Amount = dto.Amount,
            CategoryId = dto.CategoryId,
            Type = dto.Type,
            AppUserId = userId,
            CreatedAt = DateTime.UtcNow
        };

        await _unitOfWork.Transactions.AddAsync(transaction);
        var result = await _unitOfWork.CompleteAsync();

        return result > 0;
    }

    public Task<IEnumerable<TransactionDto>> GetAllTransactionsAsync(string userId)
    {
        throw new NotImplementedException();
    }
}
