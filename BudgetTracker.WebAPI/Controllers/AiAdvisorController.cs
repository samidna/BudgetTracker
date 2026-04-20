using BudgetTracker.Application.DTOs.AiAdvisor;
using BudgetTracker.Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BudgetTracker.WebAPI.Controllers;
[Route("api/[controller]")]
[ApiController]
public class AiAdvisorController : ControllerBase
{
    private readonly IAiAdvisorService _aiAdvisorService;

    public AiAdvisorController(IAiAdvisorService aiAdvisorService)
    {
        _aiAdvisorService = aiAdvisorService;
    }

    [HttpGet("analyze")]
    public async Task<IActionResult> Analyze([FromQuery] AIAdviceRequestDto request)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var result = await _aiAdvisorService.GetAdviceAsync(userId, request);
        return Ok(result);
    }
}
