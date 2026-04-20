namespace BudgetTracker.Application.DTOs.AiAdvisor;
public class AIAdviceRequestDto
{
    public int Month { get; set; } = DateTime.UtcNow.Month;
    public int Year { get; set; } = DateTime.UtcNow.Year;
}
