namespace BudgetTracker.Domain.Entities;
public class Category : BaseEntity
{
    public string Name { get; set; }
    public string Icon { get; set; }
    public ICollection<Transaction> Transactions { get; set; }
}
