namespace BudgetTracker.Application.DTOs.AiAdvisor;
public class AIAdviceResponseDto
{
    public string Advice { get; set; }
    public decimal TotalIncome { get; set; }
    public decimal TotalExpense { get; set; }
    public decimal NetBalance { get; set; }
    public List<CategorySummaryDto> CategorySummaries { get; set; }
}
