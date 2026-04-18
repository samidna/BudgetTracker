using AutoMapper;
using BudgetTracker.Application.DTOs.Category;
using BudgetTracker.Application.Interfaces;
using BudgetTracker.Domain.Entities;

namespace BudgetTracker.Application.Services;
public class CategoryService : ICategoryService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public CategoryService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<IEnumerable<CategoryDto>> GetAllCategoriesAsync()
    {
        var categories = await _unitOfWork.Categories.GetAllAsync();
        return _mapper.Map<IEnumerable<CategoryDto>>(categories);
    }

    public async Task<CategoryDto> CreateCategoryAsync(CreateCategoryDto dto)
    {
        var category = _mapper.Map<Category>(dto);
        await _unitOfWork.Categories.AddAsync(category);
        await _unitOfWork.CompleteAsync();

        return _mapper.Map<CategoryDto>(category);
    }
}
