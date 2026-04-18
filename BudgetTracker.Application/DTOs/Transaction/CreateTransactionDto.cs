using BudgetTracker.Domain.Enums;

namespace BudgetTracker.Application.DTOs.Transaction;
public class CreateTransactionDto
{
    public string Title { get; set; }
    public decimal Amount { get; set; }
    public string? Description { get; set; }
    public TransactionType Type { get; set; }
    public int CategoryId { get; set; }
}
