using BudgetTracker.Application.DTOs.Category;

namespace BudgetTracker.Application.Interfaces;
public interface ICategoryService
{
    Task<IEnumerable<CategoryDto>> GetAllCategoriesAsync();
    Task<CategoryDto> CreateCategoryAsync(CreateCategoryDto dto);
}
