using BudgetTracker.Application.DTOs.Category;
using BudgetTracker.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BudgetTracker.WebAPI.Controllers;
[Route("api/[controller]")]
[ApiController]
public class CategoriesController : ControllerBase
{
    private readonly ICategoryService _categoryService;

    public CategoriesController(ICategoryService categoryService)
    {
        _categoryService = categoryService;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var categories = await _categoryService.GetAllCategoriesAsync();
        return Ok(categories);
    }

    [HttpPost]
    [Authorize]
    public async Task<IActionResult> Post(CreateCategoryDto dto)
    {
        var result = await _categoryService.CreateCategoryAsync(dto);
        return Ok(result);
    }
}
