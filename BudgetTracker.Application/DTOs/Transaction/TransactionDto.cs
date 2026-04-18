namespace BudgetTracker.Application.DTOs.Transaction;
public class TransactionDto
{
    public int Id { get; set; }
    public string Title { get; set; }
    public decimal Amount { get; set; }
    public string? Description { get; set; }
    public string TransactionType { get; set; }
    public string CategoryName { get; set; }
    public DateTime CreatedAt { get; set; }
}
