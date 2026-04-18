using BudgetTracker.Application.DTOs.User;
using BudgetTracker.Domain.Entities;

namespace BudgetTracker.Application.Interfaces;
public interface IAuthService
{
    Task<AuthResponseDto> RegisterAsync(RegisterDto dto);
    Task<AuthResponseDto> LoginAsync(LoginDto dto);
    string GenerateJwtToken(AppUser user);
}
