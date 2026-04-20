namespace BudgetTracker.Application.DTOs.AiAdvisor;
public class CategorySummaryDto
{
    public string CategoryName { get; set; }
    public string Icon { get; set; }
    public decimal TotalAmount { get; set; }
    public int TransactionCount { get; set; }
}
