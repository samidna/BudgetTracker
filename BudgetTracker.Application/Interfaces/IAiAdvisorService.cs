using BudgetTracker.Application.DTOs.AiAdvisor;

namespace BudgetTracker.Application.Interfaces;
public interface IAiAdvisorService
{
    Task<string> GetAdviceAsync(string userId, AIAdviceRequestDto request);
}
