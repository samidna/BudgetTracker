using BudgetTracker.Domain.Enums;

namespace BudgetTracker.Domain.Entities;
public class Transaction : BaseEntity
{
    public string Title { get; set; }
    public decimal Amount { get; set; }
    public string Description { get; set; }
    public TransactionType Type { get; set; }
    public Category Category { get; set; }
    public int CategoryId { get; set; }
    public string AppUserId { get; set; }
    public AppUser AppUser { get; set; }
}
