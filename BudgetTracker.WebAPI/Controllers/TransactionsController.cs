using BudgetTracker.Application.DTOs.Transaction;
using BudgetTracker.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BudgetTracker.WebAPI.Controllers;
[Route("api/[controller]")]
[ApiController]
public class TransactionsController : ControllerBase
{
    private readonly ITransactionService _transactionService;

    public TransactionsController(ITransactionService transactionService)
    {
        _transactionService = transactionService;
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateTransactionDto dto)
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        if (string.IsNullOrEmpty(userId))
        {
            return Unauthorized("User not found");
        }

        var result = await _transactionService.CreateTransactionAsync(dto, userId);

        if (result) return Ok("Transaction successfullt created");
        return BadRequest("Error");
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        var transactions = await _transactionService.GetAllTransactionsAsync(userId);
        return Ok(transactions);
    }
}
