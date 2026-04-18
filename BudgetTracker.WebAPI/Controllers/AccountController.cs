using BudgetTracker.Application.DTOs.User;
using BudgetTracker.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BudgetTracker.WebAPI.Controllers;
[Route("api/[controller]")]
[ApiController]
public class AccountController : ControllerBase
{
    private readonly IAuthService _authService;

    public AccountController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterDto dto)
    {
        var result = await _authService.RegisterAsync(dto);
        if (!result.IsSuccess)
        {
            return BadRequest(result);
        }
        return Ok(result);
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginDto dto)
    {
        var result = await _authService.LoginAsync(dto);
        if (!result.IsSuccess)
        {
            return Unauthorized(result);
        }
        return Ok(result);
    }
}
