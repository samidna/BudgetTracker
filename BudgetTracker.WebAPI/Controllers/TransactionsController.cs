using BudgetTracker.Application.DTOs.Transaction;
using BudgetTracker.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

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
        var userId = "test-user-123";

        var result = await _transactionService.CreateTransactionAsync(dto, userId);

        if (result) return Ok("Tranzaksiya uğurla yaradıldı");
        return BadRequest("Xəta baş verdi");
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var userId = "test-user-123";
        var transactions = await _transactionService.GetAllTransactionsAsync(userId);
        return Ok(transactions);
    }
}
