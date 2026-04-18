namespace BudgetTracker.Application.DTOs.User;
public class AuthResponseDto
{
    public bool IsSuccess { get; set; }
    public string Message { get; set; }
    public string? Token { get; set; }

    public AuthResponseDto(bool isSuccess, string message, string? token = null)
    {
        IsSuccess = isSuccess;
        Message = message;
        Token = token;
    }
}
